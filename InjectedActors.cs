using Akka.Actor;
using System;

namespace Akka.Tdd.TestKit
{
    public class InjectedActors
    {
        public Type ActorType { get; set; }
        public IActorRef ActorRef { get; set; }
    }
}