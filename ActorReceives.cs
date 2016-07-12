using Akka.Actor;
using Akka.DI.Core;
using Akka.Tdd.Core;
using Autofac;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Akka.Tdd.TestKit
{
    public class ActorReceives<T>
    {
        private ConcurrentDictionary<Tuple<Guid, Type>, object> Mocks { get; set; }
        private IContainer Container { get; set; }
        private ActorSystem ActorSystem { get; set; }
        private T ReceivedMessage { get; set; }
        private ConcurrentDictionary<Guid, MockMessages> MessagesReceived { get; }

        public ActorReceives(ConcurrentDictionary<Guid, MockMessages> messagesReceived, IContainer container, ActorSystem actorSystem, ConcurrentDictionary<Tuple<Guid, Type>, object> mocks = null, T receivedMessage = default(T))
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (actorSystem == null) throw new ArgumentNullException(nameof(actorSystem));
            MessagesReceived = messagesReceived;
            ActorSystem = actorSystem;
            Container = container;
            Mocks = mocks ?? new ConcurrentDictionary<Tuple<Guid, Type>, object>();
            ReceivedMessage = receivedMessage;
        }

        public ActorReceives<TT> WhenActorReceives<TT>()
        {
            return new ActorReceives<TT>(MessagesReceived, Container, ActorSystem, Mocks);
        }

        public ActorReceives<MockActorInitializationMessage> WhenActorStarts()
        {
            return new ActorReceives<MockActorInitializationMessage>(MessagesReceived, Container, ActorSystem, Mocks);
        }

        public ActorReceives<T> ItShouldDoNothing()
        {
            return this;
        }

        public ActorReceives<T> ItShouldForwardItTo(Type actorType, object message, ActorSelection parent = null)
        {
            return ItShouldDo((actorAccess) =>
            {
                var destActor = actorAccess.Context.System.LocateActor(actorType, parent);
                destActor.Tell(message, actorAccess.Context.Sender);
            });
        }

        public ActorReceives<T> ItShouldTellItToChildActor<TTC>(object message)
        {
            return ItShouldTellItToChildActor(typeof(TTC), message);
        }

        public ActorReceives<T> ItShouldTellItToChildActor(Type actorType, object message)
        {
            return ItShouldDo((actorAccess) =>
            {
                HandleChildActorType(actorType, actorAccess.ActorChildrenOrDependencies, (actor) =>
                {
                    actor.ActorRef.Tell(message);
                });
            });
        }

        private IActorRef CreateChildActor(IActorContext context, Type actorType, ActorSetUpOptions options)
        {
            var props = context.DI().Props(actorType);

            props = new SelectableActor().PrepareProps(options, props);

            var actorRef = context.ActorOf(props, new SelectableActor().GetActorNameByType(null, actorType));
            return actorRef;
        }

        private void HandleChildActorType(Type childActorType, Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors> injectedActors, Action<InjectedActors> operation)
        {
            if (injectedActors == null) return;
            if (injectedActors.Item1 != null && injectedActors.Item1.ActorType == childActorType)
            {
                operation(injectedActors.Item1);
            }
            if (injectedActors.Item2 != null && injectedActors.Item2.ActorType == childActorType)
            {
                operation(injectedActors.Item2);
            }
            if (injectedActors.Item3 != null && injectedActors.Item3.ActorType == childActorType)
            {
                operation(injectedActors.Item3);
            }
            if (injectedActors.Item4 != null && injectedActors.Item4.ActorType == childActorType)
            {
                operation(injectedActors.Item4);
            }
        }

        public IActorRef CreateMockActorRef<TActor>() where TActor : ActorBase
        {
            SetUpMockActor<TActor>();
            var actorref = ActorSystem.CreateActor<TActor>();
            return actorref;
        }

        public Type SetUpMockActor<TActor>() where TActor : ActorBase
        {
            var actor = CreateMockActor<TActor>(Mocks);
            return actor;
        }

        public Type CreateMockActor<TActor>() where TActor : ActorBase
        {
            var actor = SetUpMockActor<TActor>();
            ActorSystem.CreateActor<TActor>();
            return actor;
        }

        public ActorSelection CreateMockActorSelection<TActor>() where TActor : ActorBase
        {
            CreateMockActor<TActor>(Mocks);
            ActorSystem.CreateActor<TActor>();
            var actorSelection = ActorSystem.LocateActor<TActor>();
            return actorSelection;
        }

        protected Type CreateMockActor<TMockActor>(ConcurrentDictionary<Tuple<Guid, Type>, object> mmocks) where TMockActor : ActorBase
        {
            ItShouldDo((actorAccess) => MessagesReceived.GetOrAdd(Guid.NewGuid(), new MockMessages(actorAccess.Context.Self.ToActorMetaData().Path, typeof(T))));

            var mocks = new ConcurrentDictionary<Tuple<Guid, Type>, object>();

            mocks.GetOrAdd(new Tuple<Guid, Type>(Guid.NewGuid(), typeof(T)), new ItShouldExecuteLambda((actorAccess) => MessagesReceived.GetOrAdd(Guid.NewGuid(), new MockMessages(actorAccess.Context.Self.ToActorMetaData().Path, typeof(T)))));
            foreach (var mock in mmocks)
            {
                mocks.GetOrAdd(mock.Key, mock.Value);
            }

            foreach (var mock in mocks)
            {
                var builder = new ContainerBuilder();
                builder.RegisterType<TMockActor>();
                var mockActorState = new MockActorState();
                if (Container.IsRegistered<MockActorState>())
                {
                    var state = (MockActorState)Container.Resolve<MockActorState>();
                    mockActorState = state ?? mockActorState;
                }
                mockActorState.MockSetUpMessages = mockActorState.MockSetUpMessages ?? new List<MockSetUpMessage>();
                mockActorState.MockSetUpMessages.Add(new MockSetUpMessage(typeof(TMockActor), mock.Key.Item2, mock.Value));

                builder.RegisterType<MockActorState>();
                builder.Register<MockActorState>(c => mockActorState);
                builder.Update(Container);
            }

            return typeof(TMockActor);
        }

        public ActorReceives<T> ItShouldDo(Action operation)
        {
            Action<ActorAccess> op = (a) => { operation(); };

            Mocks.GetOrAdd(new Tuple<Guid, Type>(Guid.NewGuid(), typeof(T)), new ItShouldExecuteLambda(op));
            return this;
        }

        public ActorReceives<T> ItShouldDo(Action<ActorAccess> operation)
        {
            Mocks.GetOrAdd(new Tuple<Guid, Type>(Guid.NewGuid(), typeof(T)), new ItShouldExecuteLambda(operation));
            return this;
        }
    }

    public class ActorReceives : ActorReceives<object>
    {
        public ActorReceives(ConcurrentDictionary<Guid, MockMessages> messagesReceived, IContainer container, ActorSystem actorSystem, ConcurrentDictionary<Tuple<Guid, Type>, object> mocks = null) : base(messagesReceived, container, actorSystem, mocks)
        {
        }
    }
}