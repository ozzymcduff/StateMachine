using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachines;
namespace Tests
{
    class AutoShop
    {
        [Flags]
        public enum States
        {
            None = 0,
            Available = 1,
            Busy = 2,
            Any = Available | Busy
        }

        [Flags]
        public enum Events
        {
            None = 0,
            TowVehicle = 1,
            FixVehicle = 2,
            Any = TowVehicle | FixVehicle
        }

        public int NumCustomers { get; private set; }
        private Machine<AutoShop, States, Events> StateMachine;

        public AutoShop()
        {
            NumCustomers = 0;

            this.StateMachine = new Machine<AutoShop, States, Events>(this)
                   .Initial(States.Available)
                   .AfterTransition(States.Available, States.Any, a => a.IncrementCustomers())
                   .AfterTransition(States.Busy, States.Any, a => a.DecrementCustomers())
                   .Event(Events.TowVehicle, e => e.Transition(States.Available, States.Busy))
                   .Event(Events.FixVehicle, e => e.Transition(States.Busy, States.Available))
                   ;
        }
        /// <summary>
        /// Increments the number of customers in service
        /// </summary>
        public void IncrementCustomers() { this.NumCustomers += 1; }
        /// <summary>
        /// Decrements the number of customers in service
        /// </summary>
        public void DecrementCustomers() { this.NumCustomers -= 1; }

        public bool IsAvailable()
        {
            return this.StateMachine.Is(States.Available);
        }

        public bool IsBusy()
        {
            return this.StateMachine.Is(States.Busy);
        }
    }
}
