using Akka.Actor;
using Akka.Tdd.Core;
using System;

namespace Akka.Tdd.TestKit
{
    public static class ActorFactoryExtensions
    {
        public static IActorRef CreateActor(this ActorSystem actorSystem, Type actorType, ActorSetUpOptions option = null, ActorMetaData parentActorMetaData = null)
        {
            return new SelectableActor().SetUp(actorType, actorSystem, null, parentActorMetaData).Create(actorSystem, option);
        }
    }
}