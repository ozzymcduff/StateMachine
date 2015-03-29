using System;

namespace StateMachines
{
    public class Event<TModel,TEvents, TState>
    {
        public Event<TModel, TEvents, TState> Transition(TState from, TState to)
        {
            throw new NotImplementedException();
        }
        public Event<TModel, TEvents, TState> If(Func<TModel,bool> condition)
        {
            throw new NotImplementedException();
        }


    }
}