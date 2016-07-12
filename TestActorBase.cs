using Akka.Actor;
using Akka.Event;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Akka.Tdd.TestKit
{
    public abstract class TestActorBase<T> : TestActorBase<T, NoSupervisor> where T : new()
    {
    }

    public abstract class TestActorBase : TestActorBase<object>
    {
    }

    public abstract class TestActorBase<T, TSupervisor> : ReceiveActor, IWithUnboundedStash where T : new() where TSupervisor : ReceiveActor
    {
        private Dictionary<Guid, Tuple<T, DateTime>> StateHistory { get; set; }

        private bool RecordActorStateHistory { get; set; }

        protected new void Become(Action configure)
        {
            base.Become(() =>
            {
                EngageState();
                configure();
            });
        }

        protected TestActorBase()
        {
            RecordActorStateHistory = false;
            SetState(new T());
            EngageState();
        }

        protected void EngageState()
        {
        }

        public bool HasStateChange()
        {
            return JsonConvert.SerializeObject(CurrentState) == JsonConvert.SerializeObject(PreviousState);
        }

        public void SetState(T newState)
        {
            PreviousState = CurrentState;
            CurrentState = newState;
        }

        public T GetState()
        {
            return CurrentState;
        }

        private T PreviousState { get; set; }
        private T CurrentState { get; set; }

        public IStash Stash { get; set; }

        public readonly ILoggingAdapter Logger = Context.GetLogger();
        /*
         is only called once directly during the initialization of the first instance,
         that is, at creation of its ActorRef. In the case of restarts, PreStart()
         is called from PostRestart(), therefore if not overridden, PreStart() is called
         on every incarnation. However, overriding PostRestart() one can disable this behavior,
         and ensure that there is only one call to PreStart().
        */
        //protected override void PreStart()
        //{
        //}

        // Overriding postRestart to disable the call to preStart() after restarts

        protected override void Unhandled(object message)
        {
            Logger.Debug(" UNHANDLED MESSAGE FOUND " + GetType().FullName + " : " + message);
        }

        /*
         depend on supervisory strategy to take care of children, and not be affected by restart of parent
        */

        // The default implementation of PreRestart() stops all the children
        // of the actor. To opt-out from stopping the children, we
        // have to override PreRestart()
        //protected override void PreRestart(Exception reason, object message)
        //{
        //    // Keep the call to PostStop(), but no stopping of children
        //    PostStop();
        //}
    }
}