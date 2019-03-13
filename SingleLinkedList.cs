using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAGL
{
    public class SingleLinkedList<T> where T : IComparable
    {
        private SingleLinkedNode<T> pos;
        private int length;

        public SingleLinkedList()//默认初始化
        {
            length = 0;
        }

        public SingleLinkedList(SequencedList<T> tar)//使用SequencedList初始化
        {
            if (tar.getLength == 0)
                return;
            SingleLinkedNode<T> first;
            SingleLinkedNode<T> next;
            first = new SingleLinkedNode<T>(tar[0]);
            pos = first;
            length++;
            for (int i = 1; i < tar.getLength; i++)
            {
                next = new SingleLinkedNode<T>(tar[i],first, pos);
                first.Last = next;
                first = next;
                length++;
            }
        }

        public SingleLinkedList(T[] tar)//使用数组初始化
        {
            if (tar.Length == 0)
                return;
            SingleLinkedNode<T> first;
            SingleLinkedNode<T> next;
            first = new SingleLinkedNode<T>(tar[0]);
            pos = first;
            length++;
            for (int i = 1; i < tar.Length; i++)
            {
                next = new SingleLinkedNode<T>(tar[i], first, pos);
                first.Last = next;
                first = next;
                length++;
            }
        }

        public SingleLinkedList(SingleLinkedList<T> tar)//使用SingleLinkedList初始化
        {
            if (tar.getLength == 0)
                return;
            SingleLinkedNode<T> first;
            SingleLinkedNode<T> next;
            first = new SingleLinkedNode<T>(tar[0]);
            pos = first;
            length++;
            for (int i = 1; i < tar.getLength; i++)
            {
                next = new SingleLinkedNode<T>(tar[i], first, pos);
                first.Last = next;
                first = next;
                length++;
            }
        }

        public void add(T tar)//插入元素
        {
            if (length == 0)
            {
                pos = new SingleLinkedNode<T>(tar);
                length++;
                return;
            }
            SingleLinkedNode<T> first= new SingleLinkedNode<T>(tar, pos.Last, pos) ;
            pos.Last.Next = first;
            pos.Last = first;
            length++;
        }

        public void add(T tar,int id)//插入元素
        {
            if (length == 0)
            {
                pos = new SingleLinkedNode<T>(tar);
                length++;
                return;
            }
            SingleLinkedNode<T> first = pos;
            if (id >= 0)
            {
                for (int j = 0; j < id; j++)
                    first = first.Next;
            }
            else
            {
                for (int j = 0; j > id; j--)
                    first = first.Last;
            }
            SingleLinkedNode<T> now= new SingleLinkedNode<T>(tar, first, first.Next);
            now.Next.Last = now;
            now.Last.Next = now;
            length++;
        }

        public void add(T[] tar)//从数组复制并插入元素
        {
            if (tar.Length == 0)
                return;
            int i;
            if (length == 0)
            {
                i = 1;
                pos = new SingleLinkedNode<T>(tar[0]);
                length++;
            }
            else
                i = 0;
            int num = tar.Length, startOfTar = 0;
            SingleLinkedNode<T> first = pos.Last;
            for (; i < num; i++)
            {
                SingleLinkedNode<T> now = new SingleLinkedNode<T>(tar[startOfTar + i], first, first.Next);
                now.Last.Next = now;
                first = now;
                length++;
            }
            first.Next.Last = first;
        }

        public void add(T[] tar,int startOfMe,int startOfTar=0)//从数组复制并插入元素
        {
            if (tar.Length == 0)
                return;
            if (startOfTar < 0)
                return;
            int i;
            if (length == 0)
            {
                i = 1;
                pos = new SingleLinkedNode<T>(tar[startOfTar]);
                length++;
            }
            else
                i = 0;
            int num = tar.Length;
            
            SingleLinkedNode<T> first = pos;
            if (startOfMe >= 0)
            {
                for (int j = 0; j < startOfMe; j++)
                    first = first.Next;
            }
            else
            {
                for (int j = 0; j > startOfMe; j--)
                    first = first.Last;
            }
            for (; i < num; i++)
            {
                SingleLinkedNode<T> now = new SingleLinkedNode<T>(tar[startOfTar + i], first, first.Next);
                now.Last.Next = now;
                first = now;
                length++;
            }
            first.Next.Last = first;
        }

        public void add(T[] tar, int startOfMe, int startOfTar, int num)//从数组复制并插入元素
        {
            if(tar.Length == 0)
                return;
            if (startOfTar < 0)
                return;
            if (num > tar.Length - startOfTar)
                num = tar.Length - startOfTar;
            int i;
            if (length == 0)
            {
                i = 1;
                pos = new SingleLinkedNode<T>(tar[startOfTar]);
                length++;
            }
            else
                i = 0;
            SingleLinkedNode<T> first = pos;
            if (startOfMe >= 0)
            {
                for (int j = 0; j < startOfMe; j++)
                    first = first.Next;

            }
            else
            {
                for (int j = 0; j > startOfMe; j--)
                    first = first.Last;
            }
            for (; i < num; i++)
            {
                SingleLinkedNode<T> now = new SingleLinkedNode<T>(tar[startOfTar + i], first, first.Next);
                now.Last.Next = now;
                first = now;
                length++;
            }
            first.Next.Last = first;
        }

        public void add(SequencedList<T> tar)//从SequencedList复制并插入元素
        {
            if (tar.getLength == 0)
                return;
            int i;
            if (length == 0)
            {
                i = 1;
                pos = new SingleLinkedNode<T>(tar[0]);
                length++;
            }
            else
                i = 0;
            int num = tar.getLength, startOfTar = 0;
            SingleLinkedNode<T> first = pos.Last;
            for (; i < num; i++)
            {
                SingleLinkedNode<T> now = new SingleLinkedNode<T>(tar[startOfTar + i], first, first.Next);
                now.Last.Next = now;
                first = now;
                length++;
            }
            first.Next.Last = first;
        }

        public void add(SequencedList<T> tar, int startOfMe, int startOfTar = 0)//从SequencedList复制并插入元素
        {
            if (tar.getLength == 0)
                return;
            if (startOfTar < 0)
                return;
            int i;
            if (length == 0)
            {
                i = 1;
                pos = new SingleLinkedNode<T>(tar[startOfTar]);
                length++;
            }
            else
                i = 0;
            int num = tar.getLength;
            SingleLinkedNode<T> first = pos;
            if (startOfMe >= 0)
            {
                for (int j = 0; j < startOfMe; j++)
                    first = first.Next;
            }
            else
            {
                for (int j = 0; j > startOfMe; j--)
                    first = first.Last;
            }
            for (; i < num; i++)
            {
                SingleLinkedNode<T> now = new SingleLinkedNode<T>(tar[startOfTar + i], first, first.Next);
                now.Last.Next = now;
                first = now;
                length++;
            }
            first.Next.Last = first;
        }

        public void add(SequencedList<T> tar, int startOfMe, int startOfTar, int num)//从SequencedList复制并插入元素
        {
            if (tar.getLength == 0)
                return;
            if (startOfTar < 0)
                return;
            if (num > tar.getLength - startOfTar)
                num = tar.getLength - startOfTar;
            int i;
            if (length == 0)
            {
                i = 1;
                pos = new SingleLinkedNode<T>(tar[startOfTar]);
                length++;
            }
            else
                i = 0;
            SingleLinkedNode<T> first = pos;
            if (startOfMe >= 0)
            {
                for (int j = 0; j < startOfMe; j++)
                    first = first.Next;

            }
            else
            {
                for (int j = 0; j > startOfMe; j--)
                    first = first.Last;
            }
            for (; i < num; i++)
            {
                SingleLinkedNode<T> now = new SingleLinkedNode<T>(tar[startOfTar + i], first, first.Next);
                now.Last.Next = now;
                first = now;
                length++;
            }
            first.Next.Last = first;
        }

        public void add(SingleLinkedList<T> tar)//从SingleLinkedList复制并插入元素
        {
            if (tar.getLength == 0)
                return;
            int i;
            if (length == 0)
            {
                i = 1;
                pos = new SingleLinkedNode<T>(tar[0]);
                tar.repos(1);
                length++;
            }
            else
                i = 0;
            int num = tar.getLength;
            SingleLinkedNode<T> first = pos.Last;
            for (; i < num; i++)
            {
                SingleLinkedNode<T> now = new SingleLinkedNode<T>(tar[0], first, first.Next);
                tar.repos(1);
                now.Last.Next = now;
                first = now;
                length++;
            }
            tar.repos(-i);
            first.Next.Last = first;
        }

        public void add(SingleLinkedList<T> tar, int startOfMe, int startOfTar = 0)//从SingleLinkedList复制并插入元素
        {
            if (tar.getLength == 0)
                return;
            int i;
            tar.repos(startOfTar); 
            if (length == 0)
            {
                i = 1;
                pos = new SingleLinkedNode<T>(tar[0]);
                tar.repos(1);
                length++;
            }
            else
                i = 0;
            int num = tar.getLength;
            SingleLinkedNode<T> first = pos;
            if (startOfMe >= 0)
            {
                for (int j = 0; j < startOfMe; j++)
                    first = first.Next;
            }
            else
            {
                for (int j = 0; j > startOfMe; j--)
                    first = first.Last;
            }
            for (; i < num; i++)
            {
                SingleLinkedNode<T> now = new SingleLinkedNode<T>(tar[0], first, first.Next);
                tar.repos(1);
                now.Last.Next = now;
                first = now;
                length++;
            }
            tar.repos(-startOfTar - i);
            first.Next.Last = first;
        }

        public void add(SingleLinkedList<T> tar, int startOfMe, int startOfTar, int num)//从SingleLinkedList复制并插入元素
        {
            if (tar.getLength == 0)
                return;
            if (num > tar.getLength - startOfTar)
                num = tar.getLength - startOfTar;
            int i;
            tar.repos(startOfTar);
            if (length == 0)
            {
                i = 1;
                pos = new SingleLinkedNode<T>(tar[0]);
                tar.repos(1);
                length++;
            }
            else
                i = 0;
            SingleLinkedNode<T> first = pos;
            if (startOfMe >= 0)
            {
                for (int j = 0; j < startOfMe; j++)
                    first = first.Next;

            }
            else
            {
                for (int j = 0; j > startOfMe; j--)
                    first = first.Last;
            }
            for (; i < num; i++)
            {
                SingleLinkedNode<T> now = new SingleLinkedNode<T>(tar[0], first, first.Next);
                tar.repos(1);
                now.Last.Next = now;
                first = now;
                length++;
            }
            tar.repos(-startOfTar - i);
            first.Next.Last = first;
        }

        public void repos(int i)//重置基准点
        {
            if (length == 0)
                return;
            SingleLinkedNode<T> first = pos;
            if (i >= 0)
            {
                for (int j = 0; j < i; j++)
                    first = first.Next;
            }
            else
            {
                for (int j = 0; j > i; j--)
                    first = first.Last;
            }
            pos = first;
        }

        public void delete(int start = 0)//删除元素
        {
            if (length == 0)
                return;
            int num = length;
            SingleLinkedNode<T> first = pos;
            if (start >= 0)
            {
                for (int j = 0; j < start; j++)
                    first = first.Next;
            }
            else
            {
                for (int j = 0; j > start; j--)
                    first = first.Last;
            }
            SingleLinkedNode<T> now = first;
            for (int i = 0; i < num; i++)
                now = now.Next;
            now.Last = first.Last;
            first.Last.Next = now;
            if (start <= 0 && start + num >= 0)
                pos = now;
            length -= num;
        }

        public void delete(int start, int num)//删除元素
        {
            if (length == 0)
                return;
            if (num <= 0)
                return;
            if (num>length)
                num = length;
            SingleLinkedNode<T> first = pos;
            if (start >= 0)
            {
                for (int j = 0; j < start; j++)
                    first = first.Next;
            }
            else
            {
                for (int j = 0; j > start; j--)
                    first = first.Last;
            }
            SingleLinkedNode<T> now = first;
            for (int i = 0; i < num; i++)
                now = now.Next;
            now.Last = first.Last;
            first.Last.Next = now;
            if (start <= 0 && start + num >= 0)
                pos = now;
            length -= num;
        }

        public T pop()//栈顶出栈
        {
            if (length == 0)
                return default(T);
            T obj = pos.Last.Data;
            delete(-1, 1);
            return obj;
        }

        public T start()//队头出队
        {
            if (length == 0)
                return default(T);
            T obj = pos.Data;
            delete(0, 1);
            return obj;
        }

        public int find(T tar, int start = 0)//从左到右查找元素
        {
            int id=start;
            SingleLinkedNode<T> s = pos;
            SingleLinkedNode<T> e = pos.Last;
            if (start >= 0)
            {
                for (int j = 0; j < start; j++)
                    s = s.Next;
            }
            else
            {
                for (int j = 0; j > start; j--)
                    s= s.Last;
            }
            while(s!=e)
            {
                if (s.Data.CompareTo(tar) == 0)
                    return id;
                s = s.Next;
                id++;
            }
            return -1;
        }

        public int find(T tar, int start,int end)//从左到右查找元素
        {
            int id = start;
            SingleLinkedNode<T> s = pos;
            SingleLinkedNode<T> e = pos;
            if (start >= 0)
            {
                for (int j = 0; j < start; j++)
                    s = s.Next;
            }
            else
            {
                for (int j = 0; j > start; j--)
                    s = s.Last;
            }
            if (end >= 0)
            {
                for (int j = 0; j < start; j++)
                    e = e.Next;
            }
            else
            {
                for (int j = 0; j > start; j--)
                    e = e.Last;
            }
            while (s != e)
            {
                if (s.Data.CompareTo(tar) == 0)
                    return id;
                s = s.Next;
                id++;
            }
            return -1;
        }

        public int reverseFind(T tar, int start = 0)//从右到左查找元素
        {
            int id = start;
            SingleLinkedNode<T> s = pos;
            SingleLinkedNode<T> e = pos;
            if (start >= 0)
            {
                for (int j = 0; j < start; j++)
                    s = s.Next;
            }
            else
            {
                for (int j = 0; j > start; j--)
                    s = s.Last;
            }
            while (s != e)
            {
                if (s.Data.CompareTo(tar) == 0)
                    return id;
                s = s.Last;
                id--;
            }
            return -1;
        }

        public int reverseFind(T tar, int start, int end)//从右到左查找元素
        {
            int id = start;
            SingleLinkedNode<T> s = pos;
            SingleLinkedNode<T> e = pos;
            if (start >= 0)
            {
                for (int j = 0; j < start; j++)
                    s = s.Next;
            }
            else
            {
                for (int j = 0; j > start; j--)
                    s = s.Last;
            }
            if (end >= 0)
            {
                for (int j = 0; j < start; j++)
                    e = e.Next;
            }
            else
            {
                for (int j = 0; j > start; j--)
                    e = e.Last;
            }
            while (s != e)
            {
                if (s.Data.CompareTo(tar) == 0)
                    return id;
                s = s.Last;
                id--;
            }
            return -1;
        }

        public int getLength//获取长度
        {
            get { return length; }
        }


        public T this[int i]//链表的索引器
        {
            get
            {
                SingleLinkedNode<T> first=pos;
                if (i >= 0)
                {
                    for (int j = 0; j < i; j++)
                        first = first.Next;
                    return first.Data;
                }
                else
                {
                    for (int j = 0; j > i; j--)
                        first = first.Last;
                    return first.Data;
                }
            }
            set
            {
                SingleLinkedNode<T> first = pos;
                if (i >= 0)
                {
                    for (int j = 0; j < i; j++)
                        first = first.Next;
                    first.Data=value;
                }
                else
                {
                    for (int j = 0; j > i; j--)
                        first = first.Last;
                    first.Data = value;
                }
            }
        }

        private void quickSort(int s, int e)
        {
            if (s < e)
            {
                int i, j;
                T x1, x2;
                i = s;
                j = e;
                SingleLinkedNode<T> first = pos;
                for (int k = 0; k < i; k++)
                    first = first.Next;
                x1 = first.Data;
                x2 = first.Data;
                SingleLinkedNode<T> last = pos;
                for (int k = 0; k < j; k++)
                    last = last.Next;
                while (i < j)
                {
                    while (i < j && last.Data.CompareTo(x1) > 0)
                    {
                        j--;
                        last = last.Last;
                    }
                    if (i < j)
                    {
                        first.Data = last.Data;
                        i++;
                        first = first.Next;
                    }
                    while (i < j && first.Data.CompareTo(x1) < 0)
                    {
                        i++;
                        first = first.Next;
                    }
                    if (i < j)
                    {
                        last.Data = first.Data;
                        j--;
                        last = last.Last;
                    }
                }
                first.Data = x2;
                quickSort(s, i - 1);
                quickSort(i + 1, e);
            }
        }

        public void sort()//快速排序
        {
            quickSort(0, length - 1);
        }

        private void reQuickSort(int s, int e)
        {
            if (s < e)
            {
                int i, j;
                T x1, x2;
                i = s;
                j = e;
                SingleLinkedNode<T> first = pos;
                for (int k = 0; k < i; k++)
                    first = first.Next;
                x1 = first.Data;
                x2 = first.Data;
                SingleLinkedNode<T> last = pos;
                for (int k = 0; k < j; k++)
                    last = last.Next;
                while (i < j)
                {
                    while (i < j && last.Data.CompareTo(x1) < 0)
                    {
                        j--;
                        last = last.Last;
                    }
                    if (i < j)
                    {
                        first.Data = last.Data;
                        i++;
                        first = first.Next;
                    }
                    while (i < j && first.Data.CompareTo(x1) > 0)
                    {
                        i++;
                        first = first.Next;
                    }
                    if (i < j)
                    {
                        last.Data = first.Data;
                        j--;
                        last = last.Last;
                    }
                }
                first.Data = x2;
                reQuickSort(s, i - 1);
                reQuickSort(i + 1, e);
            }
        }

        public void reSort()//快速排序
        {
            reQuickSort(0, length - 1);
        }
    }
}
