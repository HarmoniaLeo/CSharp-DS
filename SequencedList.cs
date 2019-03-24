using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAGL
{
    public class SequencedList<T> where T : IComparable
    {
        private T[] content;
        private int length;
        private int max;
        private int head;

        public SequencedList()//默认初始化
        {
            content = new T[128];
            length = 0;
            max = 128;
            head = 0;
        }

        public SequencedList(SequencedList<T> tar)//使用SequencedList初始化
        {
            content = new T[(tar.getLength / 128 + 1) * 128];
            for (int i = 0; i < tar.getLength; i++)
                content[i] = tar[i];
            length = tar.getLength;
            max = (tar.getLength / 128 + 1) * 128;
            head = 0;
        }

        public SequencedList(SingleLinkedList<T> tar)//使用SingleLinkedList初始化
        {
            content = new T[(tar.getLength / 128 + 1) * 128];
            int i;
            for (i = 0; i < tar.getLength; i++)
            {
                content[i] = tar[0];
                tar.repos(1);
            }
            tar.repos(i);
            length = tar.getLength;
            max = (tar.getLength / 128 + 1) * 128;
            head = 0;
        }

        public SequencedList(T[] tar)//使用数组或数据列表初始化
        {
            content = new T[(tar.Length / 128 + 1) * 128];
            for (int i = 0; i < tar.Length; i++)
                content[i] = tar[i];
            length = tar.Length;
            max = (tar.Length / 128 + 1) * 128;
            head = 0;
        }

        public int getLength//获取长度
        {
            get { return length; }
        }

        public int Head//获取队头
        {
            get { return head; }
            set { head = value; }
        }

        public T this[int i]//顺序表的索引器
        {
            get
            {
                if (i < 0 || i >= length)
                    return default(T);
                return content[i];
            }
            set
            {
                if (i >= 0 && i < length)
                    content[i] = value;
            }
        }

        private void resize(int rank)
        {
            T[] newContent = new T[128 * rank];
            Array.Copy(content, newContent, length);
            content = newContent;
            max = 128 * rank;
        }

        public void add(T tar)//插入元素
        {
            int pos = length;
            if (pos >= max - 1)
                resize(max / 128 + 1);
            length += 1;
            for (int i = length; i > pos; i--)
                content[i] = content[i - 1];
            content[pos] = tar;
        }

        public void add(T tar, int pos)//插入元素
        {
            if (pos < 0 && pos >= length)
                return;
            length += 1;
            for (int i = length; i > pos; i--)
                content[i] = content[i - 1];
            content[pos] = tar;
        }

        public void add(T[] tar)//从数组复制并插入元素
        {
            int startOfMe = length,num=tar.Length,startOfTar=0;
            if (length + num >= max - 1)
                resize((length + num) / 128 + 1);
            length += num;
            for(int k=startOfMe;k<length-num;k++)
                content[k+num] = content[k];
            int i = 0, j = 0;
            while (j < num)
                content[startOfMe + i++] = tar[startOfTar + j++];
        }

        public void add(T[] tar,int startOfMe, int startOfTar = 0)//从数组复制并插入元素
        {
            int num = tar.Length;
            if (startOfMe < 0 && startOfMe >= length)
                return;
            if (startOfTar < 0 || startOfTar >= tar.Length)
                return;
            if (length + num >= max - 1)
                resize((length + num) / 128 + 1);
            length += num;
            for (int k = startOfMe; k < length - num; k++)
                content[k + num] = content[k];
            int i = 0, j = 0;
            while (j < num)
                content[startOfMe + i++] = tar[startOfTar + j++];
        }

        public void add(T[] tar, int startOfMe,int startOfTar,int num)//从数组复制并插入元素
        {
            if (startOfMe < 0 && startOfMe >= length)
                return;
            if (startOfTar < 0 || startOfTar >= tar.Length)
                return;
            if (num > tar.Length - startOfTar)
                num = tar.Length - startOfTar;
            if (length + num >= max - 1)
                resize((length + num) / 128 + 1);
            length += num;
            for (int k = startOfMe; k < length - num; k++)
                content[k + num] = content[k];
            int i = 0, j = 0;
            while (j < num)
                content[startOfMe + i++] = tar[startOfTar + j++];
        }

        public void add(SequencedList<T> tar)//从SequencedList复制并插入元素
        {
            int startOfMe = length, num = tar.getLength, startOfTar = 0;
            if (length + num >= max - 1)
                resize((length + num) / 128 + 1);
            length += num;
            for (int k = startOfMe; k < length - num; k++)
                content[k + num] = content[k];
            int i = 0, j = 0;
            while (j < num)
                content[startOfMe + i++] = tar[startOfTar + j++];
        }

        public void add(SequencedList<T> tar, int startOfMe, int startOfTar = 0)//从SequencedList复制并插入元素
        {
            int num = tar.getLength;
            if (startOfMe < 0 && startOfMe >= length)
                return;
            if (length + num >= max - 1)
                resize((length + num) / 128 + 1);
            length += num;
            for (int k = startOfMe; k < length - num; k++)
                content[k + num] = content[k];
            int i = 0, j = 0;
            while (j < num)
                content[startOfMe + i++] = tar[startOfTar + j++];
        }

        public void add(SequencedList<T> tar, int startOfMe, int startOfTar, int num)//从SequencedList复制并插入元素
        {
            if (startOfMe < 0 && startOfMe >= length)
                return;
            if (startOfTar < 0 || startOfTar >= tar.getLength)
                return;
            if (num > tar.getLength - startOfTar)
                num = tar.getLength - startOfTar;
            if (length + num >= max - 1)
                resize((length + num) / 128 + 1);
            length += num;
            for (int k = startOfMe; k < length - num; k++)
                content[k + num] = content[k];
            int i = 0, j = 0;
            while (j < num)
                content[startOfMe + i++] = tar[startOfTar + j++];
        }

        public void add(SingleLinkedList<T> tar)//从SingleLinkedList复制并插入元素
        {
            int startOfMe = length, num = tar.getLength;
            if (length + num >= max - 1)
                resize((length + num) / 128 + 1);
            length += num;
            for (int k = startOfMe; k < length - num; k++)
                content[k + num] = content[k];
            int i = 0, j = 0;
            while (j < num)
            {
                content[startOfMe + i++] = tar[0];
                tar.repos(1);
                j++;
            }
            tar.repos(-j);
        }

        public void add(SingleLinkedList<T> tar, int startOfMe, int startOfTar = 0)//从SingleLinkedList复制并插入元素
        {
            int num = tar.getLength;
            if (startOfMe < 0 && startOfMe >= length)
                return;
            if (length + num >= max - 1)
                resize((length + num) / 128 + 1);
            length += num;
            for (int k = startOfMe; k < length - num; k++)
                content[k + num] = content[k];
            int i = 0, j = 0;
            tar.repos(startOfTar);
            while (j < num)
            {
                content[startOfMe + i++] = tar[0];
                tar.repos(1);
                j++;
            }
            tar.repos(-startOfTar-j);
        }

        public void add(SingleLinkedList<T> tar, int startOfMe, int startOfTar, int num)//从SingleLinkedList复制并插入元素
        {
            if (startOfMe < 0 && startOfMe >= length)
                return;
            if (num > tar.getLength - startOfTar)
                num = tar.getLength - startOfTar;
            if (length + num >= max - 1)
                resize((length + num) / 128 + 1);
            length += num;
            for (int k = startOfMe; k < length - num; k++)
                content[k + num] = content[k];
            int i = 0, j = 0;
            tar.repos(startOfTar);
            while (j < num)
            {
                content[startOfMe + i++] = tar[0];
                tar.repos(1);
                j++;
            }
            tar.repos(-startOfTar-j);
        }

        public void cover(T[] tar)//从数组复制并覆盖元素
        {
            int startOfMe = length, num = tar.Length,startOfTar=0;
            int j = 0, i = 0;
            while (j < num)
            {
                if (startOfMe + j >= max - 1)
                    resize((startOfMe + j) / 128 + 1);
                if (startOfMe + j >= length)
                    length = startOfMe + j + 1;
                content[startOfMe + i++] = tar[startOfTar + j++];
            }
        }

        public void cover(T[] tar, int startOfMe, int startOfTar = 0)//从数组复制并覆盖元素
        {
            int num = tar.Length;
            if (startOfMe < 0 && startOfMe >= length)
                return;
            if (startOfTar < 0 || startOfTar >= tar.Length)
                return;
            int j = 0, i = 0;
            while (j < num)
            {
                if (startOfMe + j >= max - 1)
                    resize((startOfMe + j) / 128 + 1);
                if (startOfMe + j >= length)
                    length = startOfMe + j + 1;
                content[startOfMe + i++] = tar[startOfTar + j++];
            }
        }

        public void cover(T[] tar, int startOfMe, int startOfTar, int num)//从数组复制并覆盖元素
        {
            if (startOfMe < 0 && startOfMe >= length)
                return;
            if (startOfTar < 0 || startOfTar >= tar.Length)
                return;
            if (num > tar.Length - startOfTar)
                num = tar.Length - startOfTar;
            int j = 0, i = 0;
            while (j < num)
            {
                if (startOfMe + j >= max - 1)
                    resize((startOfMe + j) / 128 + 1);
                if (startOfMe + j >= length)
                    length = startOfMe + j + 1;
                content[startOfMe + i++] = tar[startOfTar + j++];
            }
        }

        public void cover(SequencedList<T> tar)//从SequencedList复制并覆盖元素
        {
            int startOfMe = length, num = tar.getLength, startOfTar = 0;
            int j = 0, i = 0;
            while (j < num)
            {
                if (startOfMe + j >= max - 1)
                    resize((startOfMe + j) / 128 + 1);
                if (startOfMe + j >= length)
                    length = startOfMe + j + 1;
                content[startOfMe + i++] = tar[startOfTar + j++];
            }
        }

        public void cover(SequencedList<T> tar, int startOfMe, int startOfTar = 0)//从SequencedList复制并覆盖元素
        {
            int num = tar.getLength;
            if (startOfMe < 0 && startOfMe >= length)
                return;
            if (startOfTar < 0 || startOfTar >= tar.getLength)
                return;
            int j = 0, i = 0;
            while (j < num)
            {
                if (startOfMe + j >= max - 1)
                    resize((startOfMe + j) / 128 + 1);
                if (startOfMe + j >= length)
                    length = startOfMe + j + 1;
                content[startOfMe + i++] = tar[startOfTar + j++];
            }
        }

        public void cover(SequencedList<T> tar, int startOfMe, int startOfTar, int num)//从SequencedList复制并覆盖元素
        {
            if (startOfMe < 0 && startOfMe >= length)
                return;
            if (startOfTar < 0 || startOfTar >= tar.getLength)
                return;
            if (num > tar.getLength - startOfTar)
                num = tar.getLength - startOfTar;
            int j = 0, i = 0;
            while (j < num)
            {
                if (startOfMe + j >= max - 1)
                    resize((startOfMe + j) / 128 + 1);
                if (startOfMe + j >= length)
                    length = startOfMe + j + 1;
                content[startOfMe + i++] = tar[startOfTar + j++];
            }
        }

        public void cover(SingleLinkedList<T> tar)//从SingleLinkedListt复制并覆盖元素
        {
            int startOfMe = length, num = tar.getLength;
            int j = 0, i = 0;
            while (j < num)
            {
                if (startOfMe + j >= max - 1)
                    resize((startOfMe + j) / 128 + 1);
                if (startOfMe + j >= length)
                    length = startOfMe + j + 1;
                content[startOfMe + i++] = tar[0];
                tar.repos(1);
                j++;
            }
            tar.repos(-j);
        }

        public void cover(SingleLinkedList<T> tar, int startOfMe, int startOfTar = 0)//从SingleLinkedList复制并覆盖元素
        {
            int num = tar.getLength;
            if (startOfMe < 0 && startOfMe >= length)
                return;
            int j = 0, i = 0;
            tar.repos(startOfTar);
            while (j < num)
            {
                if (startOfMe + j >= max - 1)
                    resize((startOfMe + j) / 128 + 1);
                if (startOfMe + j >= length)
                    length = startOfMe + j + 1;
                content[startOfMe + i++] = tar[0];
                tar.repos(1);
                j++;
            }
            tar.repos(-j);
        }

        public void cover(SingleLinkedList<T> tar, int startOfMe, int startOfTar, int num)//从SingleLinkedList复制并覆盖元素
        {
            if (startOfMe < 0 && startOfMe >= length)
                return;
            int j = 0, i = 0;
            tar.repos(startOfTar);
            while (j < num)
            {
                if (startOfMe + j >= max - 1)
                    resize((startOfMe + j) / 128 + 1);
                if (startOfMe + j >= length)
                    length = startOfMe + j + 1;
                content[startOfMe + i++] = tar[0];
                tar.repos(1);
                j++;
            }
            tar.repos(-j);
        }

        public void delete(int start = 0)//删除元素
        {
            if (start < 0 || start >= length)
                return;
            int num = length-start;
            for (int i = start; i + num < length; i++)
                content[i] = content[i + num];
            length -= num;
            if (length < 0)
                length = 0;
            if(length<max-128)
                resize(length / 128 + 1);
        }

        public void delete(int start,int num)//删除元素
        {
            if (num <= 0)
                return;
            if (start + num > length)
                num = length - start;
            if (start < 0 || start >= length)
                return;
            for (int i = start; i + num < length; i++)
                content[i] = content[i + num];
            length -= num;
            if (length < 0)
                length = 0;
            if (length < max - 128)
                resize(length / 128 + 1);
        }

        public T pop()//栈顶出栈
        {
            if (length == 0)
                return default(T);
            T obj = content[length - 1];
            delete(length - 1, 1);
            return obj;
        }

        public T start()//队头出队
        {
            if (head == length)
                return default(T);
            T obj = content[head];
            head++;
            return obj;
        }

        public int find(T tar, int start = 0)//从左到右查找元素
        {
            if (start < 0 || start >= length)
                return -1;
            int end = length-1;
            int id = start;
            while(id<=end)
            {
                if (content[id].CompareTo(tar)==0)
                    return id;
                id++;
            }
            return -1;
        }

        public int find(T tar, int start,int end)//从左到右查找元素
        {
            if (start < 0 || start >= length||end<start||end<0||end>=length)
                return -1;
            int id = start;
            while (id <= end)
            {
                if (content[id].CompareTo(tar) == 0)
                    return id;
                id++;
            }
            return -1;
        }

        public int reverseFind(T tar, int start = 0)//从右到左查找元素
        {
            if (start < 0 || start >= length)
                return -1;
            int end= length-1;
            int id = end;
            while(id>=start)
            {
                if (content[id].CompareTo(tar)==0)
                    return id;
                id--;
            }
            return -1;
        }

        public int reverseFind(T tar, int start, int end)//从右到左查找元素
        {
            if (start < 0 || start >= length || end < start || end < 0 || end >= length)
                return -1;
            if (end < 0)
                end = length - 1;
            int id = end;
            while (id >= start)
            {
                if (content[id].CompareTo(tar) == 0)
                    return id;
                id--;
            }
            return -1;
        }

        private void quickSort(int s, int e)
        {
            if (s < e)
            {
                int i, j;
                T x1, x2;
                i = s;
                j = e;
                x1 = content[i];
                x2 = content[i];
                while (i < j)
                {
                    while (i < j && content[j].CompareTo(x1)>0)
                        j--;
                    if (i < j)
                        content[i++] = content[j];
                    while (i < j && content[i].CompareTo(x1)<0)
                        i++;
                    if (i < j)
                        content[j--] = content[i];
                }
                content[i] = x2;
                quickSort(s, i - 1);
                quickSort(i + 1, e);
            }
        }

        private void reQuickSort(int s, int e)
        {
            if (s < e)
            {
                int i, j;
                T x1, x2;
                i = s;
                j = e;
                x1 = content[i];
                x2 = content[i];
                while (i < j)
                {
                    while (i < j && content[j].CompareTo(x1) < 0)
                        j--;
                    if (i < j)
                        content[i++] = content[j];
                    while (i < j && content[i].CompareTo(x1) > 0)
                        i++;
                    if (i < j)
                        content[j--] = content[i];
                }
                content[i] = x2;
                reQuickSort(s, i - 1);
                reQuickSort(i + 1, e);
            }
        }

        public void sort()//快速排序
        {
            quickSort(0, length-1);
        }

        public void reSort()//快速排序
        {
            reQuickSort(0, length - 1);
        }
    }
}
