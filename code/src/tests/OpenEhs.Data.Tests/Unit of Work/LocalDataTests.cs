using System;
using System.Threading;
using NUnit.Framework;

namespace OpenEhs.Data.Tests.Unit_of_Work
{
    [TestFixture]
    public class LocalDataTests
    {
        private ManualResetEvent _event;

        [SetUp]
        public void SetupContext()
        {
            Local.Data.Clear();
        }

        [Test]
        public void CanStoreValuesInLocalData()
        {
            Local.Data["one"] = "This is a string";
            Local.Data["two"] = 99.9m;
            var person = new {Name = "John Doe", Birthdate = new DateTime(1991, 1, 15)};
            Local.Data[1] = person;

            Assert.AreEqual(3, Local.Data.Count);
            Assert.AreEqual("This is a string", Local.Data["one"]);
            Assert.AreEqual(99.9m, Local.Data["two"]);
            Assert.AreEqual(person, Local.Data[1]);
        }

        [Test]
        public void CanClearLocalData()
        {
            Local.Data["one"] = "This is a string";
            Local.Data["two"] = 99.9m;
            Assert.AreEqual(2, Local.Data.Count);
            Local.Data.Clear();
            Assert.AreEqual(0, Local.Data.Count);
        }

        [Test]
        public void LocalDataIsThreadLocal()
        {
            Console.WriteLine("Starting in main thread {0}", Thread.CurrentThread.ManagedThreadId);
            Local.Data["one"] = "This is a string";
            Assert.AreEqual(1, Local.Data.Count);

            _event = new ManualResetEvent(false);
            var backgroundThread = new Thread(RunInOtherThread);
            backgroundThread.Start();

            // Give the background thread some time to do its job.
            Thread.Sleep(100);
            // We still have only one entry (in this thread).
            Assert.AreEqual(1, Local.Data.Count);

            Console.WriteLine("Signaling background thread from main thread {0}", Thread.CurrentThread.ManagedThreadId);
            _event.Set();
            backgroundThread.Join();
        }

        private void RunInOtherThread()
        {
            Console.WriteLine("Starting (background-) thread {0}", Thread.CurrentThread.ManagedThreadId);

            // Initially the local data must be empty for this NEW thread!
            Assert.AreEqual(0, Local.Data.Count);
            Local.Data["one"] = "This is another string";
            Assert.AreEqual(1, Local.Data.Count);

            Console.WriteLine("Waiting on (background-) thread {0}", Thread.CurrentThread.ManagedThreadId);
            _event.WaitOne();
            Console.WriteLine("Ending (background-) thread {0}", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
