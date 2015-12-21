using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{

    class PriorityQueue<T> where T : IComparable<T>,IEquatable<T>
    {
        private MyLinkedList<T> first;

        public void add(T o)
        {

            MyLinkedList<T> next = new MyLinkedList<T>();
            next.setValue(o);

            if (this.first == null)
            {
                this.first = next;
            }
            else
            {

                ArrayList ObjectList = new ArrayList();

                MyLinkedList<T> temp = this.first;

                while (temp != null)
                {
                    ObjectList.Add(temp.getValue());
                    temp = temp.getNext();
                }
                ObjectList.Add(next.getValue());
                ObjectList.Sort();

                this.first = new MyLinkedList<T>();
                this.first.setValue((T)ObjectList[0]);

                MyLinkedList<T> v1;
                MyLinkedList<T> v2;

                v2 = this.first;

                for (int i = 1; i < ObjectList.Count; i++)
                {

                    v1 = new MyLinkedList<T>();
                    v1.setValue((T)ObjectList[i]);

                    v2.setNext(v1);
                    v2 = v1;
                }
            }
        }

        public T peek()
        {

            if (first != null)
            {
                return first.getValue();
            }    
               
            return default(T);
          
        }

        public T remove()
        {
            MyLinkedList<T> rem = this.first;

            if (first != null)
            {
                this.first = rem.getNext();
                return rem.getValue();
            }
            else
            {
                return default(T);
            }
        }

        public T remove(T item)
        {
            if (this.first == null) return default(T);
            if(first.getValue().Equals(item))
            {
                if (this.first.getNext() == null)
                    this.first = null;
                else
                    this.first = this.first.getNext();
                return item;
            }
            else
            {
                MyLinkedList<T> temp = this.first.getNext();

                while (temp != null && !temp.getValue().Equals(item))
                {
                    temp = temp.getNext();
                }
                if (temp == null)
                    return default(T);
                else
                {
                    temp.remove();
                    return item;
                }
            }
        }
    }

    public class ReverseComparer : IComparer
    {
        private static ReverseComparer instance;

        protected ReverseComparer()
        {
        }

        public static ReverseComparer Instance()
        {
            if (instance == null)
            {
                instance = new ReverseComparer();
            }

            return instance;
        }

        // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer.Compare(Object x, Object y)
        {

            return ((new CaseInsensitiveComparer()).Compare(y, x));

        }
    }
}
