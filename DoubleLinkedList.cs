using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAGL
{
    public class iterator<T> where T : IComparable
    {
        public class DoubleLinkedNode
        {
            private T data;
            public bool avaluable;
            private DoubleLinkedNode next;
            private DoubleLinkedNode last;

            public DoubleLinkedNode Next { get { return next; } set { next = value; } }
            public DoubleLinkedNode Last { get { return last; } set { last = value; } }
            public T Data { get { return data; } }

            public DoubleLinkedNode()
            {
                next = this;
                last = this;
                avaluable = false;
            }

            public DoubleLinkedNode(T value)
            {
                data = value;
                next = this;
                last = this;
                avaluable = true;
            }

            public DoubleLinkedNode(DoubleLinkedNode value)
            {
                if(!value.avaluable)
                {
                    next = this;
                    last = this;
                    avaluable = false;
                }
                data = value.data;
                next = this;
                last = this;
                avaluable = true;
            }

            public DoubleLinkedNode(DoubleLinkedNode value, DoubleLinkedNode m_last, DoubleLinkedNode m_next)
            {
                if (!value.avaluable)
                {
                    next = this;
                    last = this;
                    avaluable = false;
                }
                if (!m_last.avaluable || !m_next.avaluable)
                {
                    data = value.data;
                    next = this;
                    last = this;
                    avaluable = true;
                }
                data = value.data;
                next = m_next;
                m_next.last = this;
                last = m_last;
                m_last.next = this;
                avaluable = true;
            }

            public DoubleLinkedNode(T value, DoubleLinkedNode m_last, DoubleLinkedNode m_next)
            {
                if (!m_last.avaluable || !m_next.avaluable)
                {
                    data = value;
                    next = this;
                    last = this;
                    avaluable = true;
                }
                data = value;
                next = m_next;
                m_next.last = this;
                last = m_last;
                m_last.next = this;
                avaluable = true;
            }

            public void delete()
            {
                next.last = last;
                last.next = next;
                next = this;
                last = this;
                avaluable = false;
            }
        }

        private static DoubleLinkedNode findPotion(DoubleLinkedNode start,int id)
        {
            if (id >= 0)
            {
                for (int i = 0; i < id; i++)
                    start = start.Next;
            }
            else
            {
                for (int i = 0; i > id; i--)
                    start = start.Last;
            }
            return start;
        }

        private DoubleLinkedNode pos;

        public iterator()
        {
            pos = new DoubleLinkedNode();
        }

        public iterator(T obj)
        {
            pos = new DoubleLinkedNode(obj);
        }

        public iterator(iterator<T> it, int id = 0)
        {
            pos = findPotion(pos,id);
        }

        public iterator(T[] tar)//使用数组初始化
        {
            if (tar.Length == 0)
            {
                pos = new DoubleLinkedNode();
                return;
            }
            DoubleLinkedNode first;
            DoubleLinkedNode next;
            first = new DoubleLinkedNode(tar[0]);
            pos=first;
            for (int i = 1; i < tar.Length; i++)
            {
                next = new DoubleLinkedNode(tar[i], first, pos);
                first = next;
            }
        }

        public static iterator<T> operator ++(iterator<T> a)
        {
            a.pos = a.pos.Next;
            return a;
        }

        public static iterator<T> operator --(iterator<T> a)
        {
            a.pos = a.pos.Last;
            return a;
        }

        public static iterator<T> operator +(iterator<T> a, int id)
        {
            iterator<T> b = new iterator<T>();
            b.pos = findPotion(a.pos, id);
            return b;
        }

        public static iterator<T> operator -(iterator<T> a, int id)
        {
            iterator<T> b = new iterator<T>();
            b.pos = findPotion(a.pos, -id);
            return b;
        }

        public T this[int id]
        {
            get
            {
                return findPotion(pos, id).Data;
            }
            set
            {
                DoubleLinkedNode next = findPotion(pos, id);
                DoubleLinkedNode newPos = new DoubleLinkedNode(value, next.Last, next);
                if (id == 0)
                    pos = newPos;
                next.delete();
            }
        }

        public bool Equal(iterator<T> it)
        {
            if (pos==it.pos)
                return true;
            return false;
        }

        public void add(T tar)//插入元素
        {
            if (!pos.avaluable)
            {
                pos = new DoubleLinkedNode(tar);
                return;
            }
            DoubleLinkedNode first = new DoubleLinkedNode(tar, pos.Last, pos);
        }

        public void add(T tar, int id)//插入元素
        {
            if (!pos.avaluable)
            {
                pos = new DoubleLinkedNode(tar);
                return;
            }
            DoubleLinkedNode first = findPotion(pos,id);
            DoubleLinkedNode now = new DoubleLinkedNode(tar, first, first.Next);
        }

        public void add(T[] tar)//从数组复制并插入元素
        {
            if (tar.Length == 0)
                return;
            int i;
            if (!pos.avaluable)
            {
                i = 1;
                pos = new DoubleLinkedNode(tar[0]);
            }
            else
                i = 0;
            int num = tar.Length, startOfTar = 0;
            DoubleLinkedNode first = pos.Last;
            for (; i < num; i++)
            {
                DoubleLinkedNode now = new DoubleLinkedNode(tar[startOfTar + i], first, first.Next);
                first = now;
            }
        }

        public void add(T[] tar, int startOfMe, int startOfTar = 0)//从数组复制并插入元素
        {
            if (tar.Length == 0)
                return;
            if (startOfTar < 0)
                return;
            int i;
            if (!pos.avaluable)
            {
                i = 1;
                pos = new DoubleLinkedNode(tar[startOfTar]);
            }
            else
                i = 0;
            int num = tar.Length;
            DoubleLinkedNode first = findPotion(pos,startOfMe);
            for (; i < num; i++)
            {
                DoubleLinkedNode now = new DoubleLinkedNode(tar[startOfTar + i], first, first.Next);
                first = now;
            }
        }

        public void add(T[] tar, int startOfMe, int startOfTar, int num)//从数组复制并插入元素
        {
            if (tar.Length == 0)
                return;
            if (startOfTar < 0)
                return;
            if (num > tar.Length - startOfTar)
                num = tar.Length - startOfTar;
            int i;
            if (!pos.avaluable)
            {
                i = 1;
                pos = new DoubleLinkedNode(tar[startOfTar]);
            }
            else
                i = 0;
            DoubleLinkedNode first = findPotion(pos, startOfMe);
            for (; i < num; i++)
            {
                DoubleLinkedNode now = new DoubleLinkedNode(tar[startOfTar + i], first, first.Next);
                first = now;
            }
        }

        public void add(iterator<T> tar)//从DoubleLinkedList复制并插入元素
        {
            if (!tar.pos.avaluable)
                return;
            iterator<T> tar2=new iterator<T>(tar);
            if (!pos.avaluable)
            {
                pos = new DoubleLinkedNode(tar2[0]);
                tar2++;
            }
            DoubleLinkedNode first = pos.Last;
            do
            {
                DoubleLinkedNode now = new DoubleLinkedNode(tar2[0], first, first.Next);
                tar2++;
                first = now;
            } while (!tar2.Equal(tar));
        }

        public void add(iterator<T> tar, int startOfMe, int startOfTar = 0)//从DoubleLinkedList复制并插入元素
        {
            if (!tar.pos.avaluable)
                return;
            iterator<T> tar2 = new iterator<T>(tar+startOfTar);
            if (!pos.avaluable)
            {
                pos = new DoubleLinkedNode(tar2[0]);
                tar2++;
            }
            DoubleLinkedNode first = findPotion(pos,startOfMe);
            do
            {
                DoubleLinkedNode now = new DoubleLinkedNode(tar2[0], first, first.Next);
                tar2++;
                first = now;
            } while (!tar2.Equal(tar +startOfTar));
        }

        public void add(iterator<T> tar, int startOfMe, int startOfTar,int num)//从DoubleLinkedList复制并插入元素
        {
            if (!tar.pos.avaluable)
                return;
            int count=0;
            iterator<T> tar2 = new iterator<T>(tar + startOfTar);
            if (!pos.avaluable)
            {
                pos = new DoubleLinkedNode(tar2[0]);
                tar2++;
                count++;
            }
            DoubleLinkedNode first = findPotion(pos, startOfMe);
            do
            {
                if (count >= num)
                    break;
                DoubleLinkedNode now = new DoubleLinkedNode(tar2[0], first, first.Next);
                tar2++;
                count++;
                first = now;
            } while (!tar2.Equal(tar + startOfTar));
        }

        public void delete(int start = 0)//删除元素
        {
            DoubleLinkedNode now = findPotion(pos, start);
            do
            {
                now = now.Next;
                now.Last.delete();
            } while (now != pos) ;
        }

        public void delete(int start, int num)//删除元素
        {
            DoubleLinkedNode now = findPotion(pos, start);
            int count = 0;
            while (count<num)
            {
                now = now.Next;
                if (now.Last == pos)
                    pos = now;
                now.Last.delete();
                count++;
            }
            
        }

        public iterator<T> sub(int start = 0)
        {
            DoubleLinkedNode now = findPotion(pos, start);
            iterator<T> it = new iterator<T>();
            it.pos=new DoubleLinkedNode(now);
            DoubleLinkedNode next = it.pos;
            do
            {
                now = now.Next;
                new DoubleLinkedNode(now,next,it.pos);
                next = next.Next;
            } while (now != pos.Last);
            return it;
        }

        public iterator<T> sub(int start, int num)
        {
            DoubleLinkedNode now = findPotion(pos, start);
            DoubleLinkedNode end = findPotion(now, num);
            iterator<T> it = new iterator<T>();
            it.pos = new DoubleLinkedNode(now);
            DoubleLinkedNode next = it.pos;
            do
            {
                now = now.Next;
                new DoubleLinkedNode(now, next, it.pos);
                next = next.Next;
            } while (now != end);
            return it;
        }

        public T pop()//栈顶出栈
        {
            if (!pos.avaluable)
                return default(T);
            T obj = pos.Last.Data;
            pos.Last.delete();
            return obj;
        }

        public T start()//队头出队
        {
            if (!pos.avaluable)
                return default(T);
            T obj = pos.Data;
            pos = pos.Next;
            pos.Last.delete();
            return obj;
        }

        public iterator<T> find(T tar, int start = 0)//从左到右查找元素
        {
            iterator<T> target = this + start;
            do
            {
                if (target[0].CompareTo(tar)==0)
                    break;
                target++;
            } while (target!=this+start);
            return target;
        }

        public iterator<T> find(T tar, int start,int end)//从左到右查找元素
        {
            iterator<T> target = this + start;
            iterator<T> ending = this+end;
            do
            {
                if (target[0].CompareTo(tar) == 0)
                    break;
                target++;
            } while (target != ending);
            return target;
        }

        public iterator<T> reverseFind(T tar, int start = 0)//从右到左查找元素
        {
            iterator<T> target = this + start;
            do
            {
                if (target[0].CompareTo(tar) == 0)
                    break;
                target--;
            } while (target != this + start);
            return target;
        }

        public iterator<T> reverseFind(T tar, int start, int end)//从右到左查找元素
        {
            iterator<T> target = this + start;
            iterator<T> ending = this + end;
            do
            {
                if (target[0].CompareTo(tar) == 0)
                    break;
                target--;
            } while (target != ending);
            return target;
        }

        private void quickSort(iterator<T> s, iterator<T> e)
        {
            if (s - 1 != e && s != e)
            {
                iterator<T> i, j;
                T x1, x2;
                i = s;
                j = e;
                x1 = i[0];
                x2 = i[0];
                while (i != j)
                {
                    while (i != j && j[0].CompareTo(x1) > 0)
                        j--;
                    if (i != j)
                    {
                        i[0] = j[0];
                        i++;
                    }
                    while (i != j && i[0].CompareTo(x1) < 0)
                        i++;
                    if (i != j)
                    {
                        j[0] = i[0];
                        j--;
                    }
                }
                i[0] = x2;
                reQuickSort(s, i - 1);
                reQuickSort(i + 1, e);
            }
        }

        public void sort()//快速排序
        {
            quickSort(this, this-1);
        }

        private void reQuickSort(iterator<T> s,iterator<T>  e)
        {
            if (s-1 != e&&s!=e)
            {
                iterator<T> i, j;
                T x1, x2;
                i = s;
                j = e;
                x1 = i[0];
                x2 = i[0];
                while (i != j)
                {
                    while (i != j && j[0].CompareTo(x1) < 0)
                        j--;
                    if (i != j)
                    {
                        i[0] =j[0];
                        i++;
                    }
                    while (i!=j && i[0].CompareTo(x1) > 0)
                        i++;
                    if (i != j)
                    {
                        j[0] = i[0];
                        j--;
                    }
                }
                i[0] = x2;
                reQuickSort(s, i - 1);
                reQuickSort(i + 1, e);
            }
        }

        public void reSort()//快速排序
        {
            reQuickSort(this,this-1);
        }
    }
}
