using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Akka.Tdd.TestKit
{
    public abstract class MockActorBase : TestActorBase<MockActorState>, IWithUnboundedStash
    {
        protected Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors> InjectedActors { get; set; }

        public object GetMockState()
        {
            var myState = GetState();
            return myState.MockState;
        }

        public void SetMockState(object mockState)
        {
            var myState = GetState();
            SetState(new MockActorState() { MockState = mockState, MockSetUpMessages = myState.MockSetUpMessages });
        }

        protected MockActorBase(MockActorState state)
        {
            SetState(state as MockActorState);
            var st = GetState();
            ReceiveAny(message =>
            {
                var myState = GetState();
                var results = myState.MockSetUpMessages.Where(s => s.WhenInComing == message.GetType() && s.Owner == GetType()).ToList();
                ProcessMockSetUpMessage(results);
            });
        }

        private void Initialize()
        {
            var result = GetState().MockSetUpMessages.Where(s => s.WhenInComing == typeof(MockActorInitializationMessage) && s.Owner == GetType()).ToList();

            ProcessMockSetUpMessage(result);
        }

        private void ProcessMockSetUpMessage(List<MockSetUpMessage> result)
        {
            if (!result.Any()) return;

            foreach (var response in result)
            {
                if (response == null) return;
                Execute(response.RespondWith, response);
            }
        }

        private void Execute(object response, object message)
        {
            var lambda = response as ItShouldExecuteLambda;
            lambda?.Operation(new ActorAccess(Context, InjectedActors, this, Stash, Become));
        }

        protected override void PreStart()
        {
            Logger.Debug(" PRESTARTING " + GetType().FullName);
            Initialize();
        }

        protected override void PostRestart(Exception reason)
        {
            Logger.Debug(" RESTARTED " + GetType().FullName + " : " + reason.Message);
            var state = GetState();
            Logger.Debug(" restarted INITIALIZING WITH STATE " + (state?.MockSetUpMessages?.Count.ToString() ?? "") + " items For ACTOR : " + GetType().FullName);
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            //  Restart the child, if 100 exceptions occur in 5 minutes or less, then stop the actor
            return new OneForOneStrategy(100, TimeSpan.FromMinutes(5), Decider.From(x => Directive.Restart));
        }
    }
}