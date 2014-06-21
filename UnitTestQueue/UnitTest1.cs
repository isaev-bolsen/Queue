using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestQueue
    {
    [TestClass]
    public class UnitTest1
        {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EmptyTest()
            {
            MyQueue.MyQueue<int> queue = new MyQueue.MyQueue<int>();
            queue.Enqueue(100);
            Assert.AreEqual(100, queue.Dequeue());
            queue.Dequeue();
            }
        
        [TestMethod]
        public void TestMethod1()
            {
            MyQueue.MyQueue<int> queue = new MyQueue.MyQueue<int>();
            queue.Enqueue(100);
            queue.Enqueue(200);
            queue.Enqueue(300);
            Assert.AreEqual(100, queue.Dequeue());
            Assert.AreEqual(200, queue.Dequeue());
            Assert.AreEqual(300, queue.Dequeue());
            }
        }
    }
