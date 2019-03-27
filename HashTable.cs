using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAGL
{
    public class HashTable<T1,T2> where T1: IComparable where T2 : IComparable
    {
        private class HashNode
        {
            T1 key;
            T2 value;
            HashNode next;
            bool ocp;

            public HashNode()
            {
                ocp = false;
            }

            public void setValue(T1 m_key, T2 m_value)
            {
                if (ocp)
                {
                    if (m_key.CompareTo(key) == 0)
                    {
                        value = m_value;
                        return;
                    }
                    next.setValue(m_key, m_value);
                    return;
                }
                key = m_key;
                value = m_value;
                ocp = true;
                next = new HashNode();
            }

            public T2 findValue(T1 m_key)
            {
                if (!ocp)
                    return default(T2);
                if (m_key.CompareTo(key) == 0)
                    return value;
                else
                    return next.findValue(m_key);
            }
        }

        HashNode[] list;

        public HashTable()
        {
            list = new HashNode[65536];
        }

        private int hash(T1 tar)
        {
            if (tar is string)
            {
                int hashcode=0;
                string tarstr = tar.ToString();
                foreach(char a in tarstr)
                    hashcode = hashcode * 65539 + a;
                return Math.Abs(hashcode) %65537;
            }
            else
                return Math.Abs(tar.GetHashCode()) % 65537; 
        }
            
        public  T2 this[T1 key]
        {
            get
            {
                if (list[hash(key)] != null)
                    return list[hash(key)].findValue(key);
                else
                    return default(T2);
            }
            set
            {
                if (list[hash(key)] == null)
                    list[hash(key)] = new HashNode();
                list[hash(key)].setValue(key, value);
            }
        }
    }
}
