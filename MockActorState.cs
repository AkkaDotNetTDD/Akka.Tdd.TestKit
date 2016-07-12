using System.Collections.Generic;

namespace Akka.Tdd.TestKit
{
    public class MockActorState
    {
        public List<MockSetUpMessage> MockSetUpMessages { get; set; }
        public object MockState { get; set; }
    }
}