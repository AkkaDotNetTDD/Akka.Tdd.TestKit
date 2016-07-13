using System;

namespace Akka.Tdd.TestKit
{
    public class MockActor : MockActorBase
    {
        public MockActor(MockActorState state) : base(state)
        {
        }
    }

    public class MockActor<TMockActor, TMockActor2, TMockActor3, TMockActor4> : MockActorBase
    {
        public MockActor(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, new InjectedActors() { ActorType = typeof(TMockActor2) }, new InjectedActors() { ActorType = typeof(TMockActor3) }, new InjectedActors() { ActorType = typeof(TMockActor4) });
        }
    }

    public class MockActor<TMockActor, TMockActor2, TMockActor3> : MockActorBase
    {
        public MockActor(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, new InjectedActors() { ActorType = typeof(TMockActor2) }, new InjectedActors() { ActorType = typeof(TMockActor3) }, null);
        }
    }

    public class MockActor<TMockActor, TMockActor2> : MockActorBase
    {
        public MockActor(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, new InjectedActors() { ActorType = typeof(TMockActor2) }, null, null);
        }
    }

    public class MockActor<TMockActor> : MockActorBase
    {
        public MockActor(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, null, null, null);
        }
    }

    public class MockActor4 : MockActorBase
    {
        public MockActor4(MockActorState state) : base(state)
        {
        }
    }

    public class MockActor3<TMockActor, TMockActor2, TMockActor3, TMockActor4> : MockActorBase
    {
        public MockActor3(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, new InjectedActors() { ActorType = typeof(TMockActor2) }, new InjectedActors() { ActorType = typeof(TMockActor3) }, new InjectedActors() { ActorType = typeof(TMockActor4) });
        }
    }

    public class MockActor3<TMockActor, TMockActor2, TMockActor3> : MockActorBase
    {
        public MockActor3(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, new InjectedActors() { ActorType = typeof(TMockActor2) }, new InjectedActors() { ActorType = typeof(TMockActor3) }, null);
        }
    }

    public class MockActor3<TMockActor, TMockActor2> : MockActorBase
    {
        public MockActor3(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, new InjectedActors() { ActorType = typeof(TMockActor2) }, null, null);
        }
    }

    public class MockActor3<TMockActor> : MockActorBase
    {
        public MockActor3(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, null, null, null);
        }
    }

    public class MockActor3 : MockActorBase
    {
        public MockActor3(MockActorState state) : base(state)
        {
        }
    }

    public class MockActor2<TMockActor, TMockActor2, TMockActor3, TMockActor4> : MockActorBase
    {
        public MockActor2(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, new InjectedActors() { ActorType = typeof(TMockActor2) }, new InjectedActors() { ActorType = typeof(TMockActor3) }, new InjectedActors() { ActorType = typeof(TMockActor4) });
        }
    }

    public class MockActor2<TMockActor, TMockActor2, TMockActor3> : MockActorBase
    {
        public MockActor2(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, new InjectedActors() { ActorType = typeof(TMockActor2) }, new InjectedActors() { ActorType = typeof(TMockActor3) }, null);
        }
    }

    public class MockActor2<TMockActor, TMockActor2> : MockActorBase
    {
        public MockActor2(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, new InjectedActors() { ActorType = typeof(TMockActor2) }, null, null);
        }
    }

    public class MockActor2<TMockActor> : MockActorBase
    {
        public MockActor2(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, null, null, null);
        }
    }

    public class MockActor2 : MockActorBase
    {
        public MockActor2(MockActorState state) : base(state)
        {
        }
    }

    public class MockActor1<TMockActor, TMockActor2, TMockActor3, TMockActor4> : MockActorBase
    {
        public MockActor1(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, new InjectedActors() { ActorType = typeof(TMockActor2) }, new InjectedActors() { ActorType = typeof(TMockActor3) }, new InjectedActors() { ActorType = typeof(TMockActor4) });
        }
    }

    public class MockActor1<TMockActor, TMockActor2, TMockActor3> : MockActorBase
    {
        public MockActor1(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, new InjectedActors() { ActorType = typeof(TMockActor2) }, new InjectedActors() { ActorType = typeof(TMockActor3) }, null);
        }
    }

    public class MockActor1<TMockActor, TMockActor2> : MockActorBase
    {
        public MockActor1(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, new InjectedActors() { ActorType = typeof(TMockActor2) }, null, null);
        }
    }

    public class MockActor1<TMockActor> : MockActorBase
    {
        public MockActor1(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, null, null, null);
        }
    }

    public class MockActor1 : MockActorBase
    {
        public MockActor1(MockActorState state) : base(state)
        {
        }
    }

    public class MockActorBase<TMockActor, TMockActor2, TMockActor3, TMockActor4> : MockActorBase
    {
        public MockActorBase(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, new InjectedActors() { ActorType = typeof(TMockActor2) }, new InjectedActors() { ActorType = typeof(TMockActor3) }, new InjectedActors() { ActorType = typeof(TMockActor4) });
        }
    }

    public class MockActorBase<TMockActor, TMockActor2, TMockActor3> : MockActorBase
    {
        public MockActorBase(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, new InjectedActors() { ActorType = typeof(TMockActor2) }, new InjectedActors() { ActorType = typeof(TMockActor3) }, null);
        }
    }

    public class MockActorBase<TMockActor, TMockActor2> : MockActorBase
    {
        public MockActorBase(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, new InjectedActors() { ActorType = typeof(TMockActor2) }, null, null);
        }
    }

    public class MockActorBase<TMockActor> : MockActorBase
    {
        public MockActorBase(MockActorState state) : base(state)
        {
            InjectedActors = new Tuple<InjectedActors, InjectedActors, InjectedActors, InjectedActors>(new InjectedActors() { ActorType = typeof(TMockActor) }, null, null, null);
        }
    }
}