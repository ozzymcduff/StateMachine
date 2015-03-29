using StateMachines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class Vehicle : ModelBase
    {
        public AutoShop AutoShop { get; private set; }
        public bool SeatbeltOn { get; private set; }
        public int InsurancePremium { get; private set; }
        public bool ForceIdle { get; private set; }
        public Action[] Callbacks { get; private set; }
        public bool TimeElapsed { get; private set; }
        public bool LastTransitionArgs { get; private set; }
        [Flags]
        public enum States
        {
            None = 0,
            Idling = 1,
            Parked = 2,
            Stalled = 4,
            FirstGear = 8,
            SecondGear = 16,
            ThirdGear = 32,
            Any = Idling | Parked | Stalled | FirstGear | SecondGear | ThirdGear,
        }

        public enum Events
        {
            Crash,
            Tow,
            Repair,
            Fix,
            Park,
            Ignite,
            ShiftUp,
            Idle,
            ShiftDown
        }

        private Machine<Vehicle, States, Events> StateMachine;
        public enum InsuranceStates
        {
            Inactive,
            Active
        }
        public enum InsuranceEvents
        {
            Buy,
            Cancel
        }

        private Machine<Vehicle, InsuranceStates, InsuranceEvents> InsuranceState;

        public Vehicle(
            AutoShop autoshop = null,
            bool seatbeltOn = false,
            int insurancePremium = 50,
            bool forceIdle = false,
            Action[] callbacks = null,
            bool saved = false)
        {
            this.AutoShop = autoshop ?? new AutoShop();
            this.SeatbeltOn = seatbeltOn;
            this.InsurancePremium = insurancePremium;
            this.ForceIdle = forceIdle;
            this.Callbacks = callbacks ?? new Action[0];
            this.Saved = saved;
            StateMachine = new Machine<Vehicle, States, Events>(this)
                    .Initial(vehicle => vehicle.ForceIdle ? States.Idling : States.Parked)
                    .Action(m => m.Save())
                    .BeforeTransition((vehicle, transition) => vehicle.LastTransitionArgs = transition.Args)
                    .BeforeTransition(States.Parked, States.Any, v => v.PutOnSeatBelt())
                    .BeforeTransition(States.Any, States.Stalled, v => v.IncreaseInsurancePremium())
                    .AfterTransition(States.Any, States.Parked, v => v.SeatbeltOn = false)
                    .AfterTransition(Events.Crash, Events.Tow)
                    .AfterTransition(Events.Repair, Events.Fix)
                    .Event(Events.Park, e => e
                            .Transition(States.Idling | States.FirstGear, States.Parked))
                    .Event(Events.Ignite, e => e
                             .Transition(States.Stalled, States.Stalled)
                             .Transition(States.Parked, States.Idling)
                        )
                    .Event(Events.Idle, e => e
                            .Transition(States.FirstGear, States.Idling))
                    .Event(Events.ShiftUp, e => e
                            .Transition(States.Idling, States.FirstGear)
                            .Transition(States.FirstGear, States.SecondGear)
                            .Transition(States.SecondGear, States.ThirdGear)
                            )
                    .Event(Events.ShiftDown, e => e
                            .Transition(States.ThirdGear, States.SecondGear)
                            .Transition(States.SecondGear, States.FirstGear)
                            )
                    .Event(Events.Crash, e => e
                            .Transition(States.FirstGear | States.SecondGear | States.ThirdGear, States.Stalled).If(v => v.AutoShop.IsAvailable()))
                    .Event(Events.Repair, e => e
                            .Transition(States.Stalled, States.Parked).If(v => v.AutoShop.IsBusy()))
                    ;

            InsuranceState = new Machine<Vehicle, InsuranceStates, InsuranceEvents>(this)
                    .Initial(InsuranceStates.Inactive)
                    .Event(InsuranceEvents.Buy, e => e
                        .Transition(InsuranceStates.Inactive, InsuranceStates.Active))
                    .Event(InsuranceEvents.Cancel, e => e
                        .Transition(InsuranceStates.Active, InsuranceStates.Inactive))

                    ;
        }

        /*  


def save
super
end

def new_record?
@saved == false
end

def park
super
end

# Tows the vehicle to the auto shop
def tow
auto_shop.tow_vehicle
end

# Fixes the vehicle; it will no longer be in the auto shop
def fix
auto_shop.fix_vehicle
end

end*/
        private void PutOnSeatBelt()
        {
            // Safety first! Puts on our seatbelt
            SeatbeltOn = true;
        }
        private void IncreaseInsurancePremium()
        {
            // We crashed! Increase the insurance premium on the vehicle
            InsurancePremium += 100;
        }
    }
}
