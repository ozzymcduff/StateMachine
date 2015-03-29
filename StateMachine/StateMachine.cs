using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines
{
    public class Machine<Model, TState,TEvents> 
    {
        private Model model;

        public Machine(Model model)
        {
            this.model= model;
        }
        //public Machine<Model, TState, TEvents> Initial(TState initial)
        //{
        //    throw new NotImplementedException();
        //}

        public Machine<Model, TState, TEvents> Initial(Func<Model, TState> initial)
        {
            throw new NotImplementedException();
        }

        public Machine<Model, TState, TEvents> Initial(TState initial)
        {
            throw new NotImplementedException();
        }
        public Machine<Model, TState, TEvents> AfterTransition(TState state, TState toexpression, Action<Model> on) 
        {
            throw new NotImplementedException();
        }
        public Machine<Model, TState, TEvents> AfterTransition(TEvents eventa, TEvents eventb)
        {
            throw new NotImplementedException();
        }
        public Machine<Model, TState, TEvents> BeforeTransition(Action<Model, Transition> before)
        {
            throw new NotImplementedException();
        }

        public Machine<Model, TState, TEvents> BeforeTransition(TState state, TState toexpression, Action<Model> on)
        {
            throw new NotImplementedException();
        }

        public Machine<Model, TState, TEvents> Event(TEvents evnt, TState from, TState to) 
        {
            throw new NotImplementedException();
        }
        public Machine<Model, TState, TEvents> Event(TEvents evnt, Action<Event<Model,TEvents, TState>> events)
        {
            throw new NotImplementedException();
        }

        public Machine<Model, TState, TEvents> Action(Expression<Action<Model>> actionToRun)
        {
            throw new NotImplementedException();
        }
        public bool Is(TState isIn)
        {
            throw new NotImplementedException();
        }
    }
}
