using System;

namespace Akka.Tdd.TestKit
{
    public class ItShouldExecuteLambda
    {
        public ItShouldExecuteLambda(Action<ActorAccess> operation)
        {
            Operation = operation;
        }

        public Action<ActorAccess> Operation { private set; get; }
    }
}