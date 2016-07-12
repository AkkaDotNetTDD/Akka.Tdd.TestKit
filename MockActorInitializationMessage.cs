namespace Akka.Tdd.TestKit
{
    public class MockActorInitializationMessage
    {
        public MockActorInitializationMessage(object initilization)
        {
            Initilization = initilization;
        }

        public object Initilization { private set; get; }
    }
}