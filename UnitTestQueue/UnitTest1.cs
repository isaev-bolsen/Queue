using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            Assert.AreEqual(3, queue.Count);
            Assert.AreEqual(100, queue.Dequeue());
            Assert.AreEqual(200, queue.Dequeue());
            Assert.AreEqual(300, queue.Dequeue());
            Assert.AreEqual(0, queue.Count);
            }

        private void enqueueAlot(int count, MyQueue.MyQueue<int> queue)
            {
            for (int i = 0; i < count; ++i) queue.Enqueue(i); 
            }
        private List<int> dequeueAlot(int count, MyQueue.MyQueue<int> queue)
            {
            List<int> toReturn = new List<int>();
            for (int i = 0; i < count; ++i) toReturn.Add(queue.Dequeue());
            return toReturn;
            }
        
        [TestMethod]
        public void asyncTest()
            {
            MyQueue.MyQueue<int> queue = new MyQueue.MyQueue<int>();
            //попробовать много раз
            for (int k=0; k < 100; ++k)
                {
                //запустить задачи по заполнению очереди...
                System.Threading.Tasks.TaskFactory TF = new System.Threading.Tasks.TaskFactory();
                TF.StartNew(() => enqueueAlot(300, queue));
                TF.StartNew(() => enqueueAlot(300, queue));
                //подождать, когда заполнится на треть
                while (queue.Count < 200) System.Threading.Thread.Sleep(1);
                //запустить задачи по извлечению. Собрать одну последовательность.
                List<int> result = new List<int>();
                List<System.Threading.Tasks.Task<List<int>>> tasks = new List<System.Threading.Tasks.Task<List<int>>>();
                for (int i = 0; i < 3; ++i) tasks.Add(TF.StartNew(() => dequeueAlot(200, queue)));
                foreach (var task in tasks) result.AddRange(task.Result);
                //проверить последовательность.
                result.Sort();
                int index = 0;
                for (int i = 0; i < 300; ++i)
                    {
                    Assert.AreEqual(i, result[index]);
                    ++index;
                    Assert.AreEqual(i, result[index]);
                    ++index;
                    }
                }
            }
        }
    }
