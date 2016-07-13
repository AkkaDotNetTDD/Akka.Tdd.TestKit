using Akka.Actor;
using System;

namespace Akka.Tdd.TestKit
{
    public class ActorAccess
    {
        public ActorAccess(IUntypedActorContext context, Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors> actorChildrenOrDependencies, MockActorBase actorThis, IStash stash, Action<Receive> become, object message)
        {
            Context = context;
            ActorThis = actorThis;
            ActorChildrenOrDependencies = actorChildrenOrDependencies;
            Stash = stash;
            Become = become;
            MessageReceived = message;
        }
        public object MessageReceived { private set; get; }
        public IUntypedActorContext Context { private set; get; }
        public Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors> ActorChildrenOrDependencies { private set; get; }
        public MockActorBase ActorThis { private set; get; }
        public IStash Stash { private set; get; }
        public Action<Receive> Become { private set; get; }
    }
}