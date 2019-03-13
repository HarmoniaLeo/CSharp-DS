using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAGL
{
    public class SingleLinkedNode<T> where T : IComparable
    {
        private T data;
        private SingleLinkedNode<T> next;
        private SingleLinkedNode<T> last;

        public SingleLinkedNode<T> Next { get { return next; } set {next= value; } }
        public SingleLinkedNode<T> Last { get { return last; } set { last = value; } }
        public T Data { get { return data; } set { data = value; } }

        public SingleLinkedNode()
        {
            next = this;
            last = this;
        }

        public SingleLinkedNode(T value)
        {
            data = value;
            next = this;
            last = this;
        }

        public SingleLinkedNode(T value, SingleLinkedNode<T> m_last,SingleLinkedNode<T> m_next)
        {
            data = value;
            next = m_next;
            last = m_last;
        }
    }
}
