using Akka.Actor;
using Akka.Tdd.Core;
using Autofac;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Akka.Tdd.TestKit
{
    public class TddTestKitfactoryFactory
    {
        public ConcurrentDictionary<Guid, MockMessages> MessagesReceived { get; }

        public TddTestKitfactoryFactory(IContainer container, ActorSystem actorSystem)
        {
            MessagesReceived = new ConcurrentDictionary<Guid, MockMessages>();
            Container = container;
            ActorSystem = actorSystem;
        }


        public IActorRef CreateActor<T>(ActorSetUpOptions option = null, ActorMetaData parentActorMetaData = null) where T : ActorBase
        {
            return ActorSystem.CreateActor<T>(option, parentActorMetaData);
        }

        public ActorReceives<T> WhenActorReceives<T>(T message = default(T))
        {
            return new ActorReceives<T>(MessagesReceived, Container, ActorSystem, null, message);
        }

        public ActorReceives<MockActorInitializationMessage> WhenActorStarts()
        {
            return new ActorReceives<MockActorInitializationMessage>(MessagesReceived, Container, ActorSystem);
        }

        public ActorSelection LocateActor<T>(ActorMetaData parentActorMetaData = null)
        {
            return new SelectableActor().Select(typeof(T), parentActorMetaData, ActorSystem);
        }

        public ActorSelection LocateActor<T>(ActorSelection parentActorSelection)
        {
            return new SelectableActor().Select(typeof(T), parentActorSelection.ToActorMetaData(), ActorSystem);
        }

        public ActorSelection LocateActor(Type type, ActorMetaData parentActorMetaData = null)
        {
            return new SelectableActor().Select(type, parentActorMetaData, ActorSystem);
        }

        public ActorSelection LocateActor(Type type, ActorSelection parentActorSelection)
        {
            return new SelectableActor().Select(type, parentActorSelection.ToActorMetaData(), ActorSystem);
        }

        public ExpectMockActor ExpectMockActor(Type actor)
        {
            return new ExpectMockActor(MessagesReceived, actor, Container, ActorSystem);
        }

        public ExpectMockActor ExpectMockActor(IActorRef actorRef)
        {
            return new ExpectMockActor(MessagesReceived, actorRef, Container, ActorSystem);
        }

        public void ClearAllMessages()
        {
            MessagesReceived.Clear();
        }

        private IContainer Container { get; set; }
        private ActorSystem ActorSystem { get; set; }

        public void UpdateContainer(Action<ContainerBuilder> action)
        {
            var builder = new ContainerBuilder();
            action(builder);

            builder.Update(Container);
        }

        public void Dispose()
        {
            Container.Dispose();
        }

        public void JustWait(int durationMilliseconds = 600000)
        {
            var now = DateTime.Now;
            var counter = 0;
            while ((DateTime.Now - now).TotalMilliseconds < durationMilliseconds)
            {
                Thread.Sleep(10 * counter);
                counter++;
            }
        }

        public void AwaitAssert(Action action, int durationMilliseconds = 3000, int sleepIntervalMilliseconds = 50)
        {
            AwaitAssert(() =>
            {
                action();
                return true;
            },  durationMilliseconds, sleepIntervalMilliseconds);
        }

        public  T AwaitAssert<T>(Func<T> action, int durationMilliseconds=3000, int sleepIntervalMilliseconds = 50)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            var now = DateTime.Now;
            var passed = false;
            var lastException = new Exception();
            T result = default(T);
            while ((DateTime.Now - now).TotalMilliseconds <= durationMilliseconds)
            {
                try
                {
                    result = action();
                    passed = true;
                    break;
                }
                catch (Exception e)
                {
                    lastException = e;
                }
                System.Threading.Thread.Sleep(sleepIntervalMilliseconds);
            }
            if (!passed)
            {
                throw new Exception("Could not pass in " + durationMilliseconds + " ms ", lastException);
            }
            return result;
        }
    }
}