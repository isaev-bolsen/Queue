using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQueue
{
    //«Реализовать блокирующую очередь и unit-тесты к ней (на C#).»
    public class MyQueue<T>// : System.Collections.Generic.ICollection<T>, IEnumerable<T>
        {
        private class Node
            {
            public T item;
            public Node next;
            }
        private object tailLock=new object();
        private object headLock=new object();

        private Node first;
        private Node last;
        private int count=0;

        public int Count { get { return count; } }

        public void Enqueue(T item)
            {
            lock (tailLock)
                {
                Node oldlast = last;
                last = new Node();
                last.item = item;
                last.next = null;
                if (first == null) first = last;
                else oldlast.next = last;
                ++count;
                }

            }
        public T Dequeue()
            {
            lock(headLock)
            try 
                {
                T item = first.item;
                first = first.next;
                if (first == null) last = null;
                --count;
                return item;
                }
            catch (NullReferenceException exc)
                {
                throw new InvalidOperationException("Queue is empty", exc);
                }
            }
     }
}
