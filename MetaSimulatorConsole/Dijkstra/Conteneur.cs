using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Dijkstra
{
    class Node<T> : Vertex, IComparable, IEquatable<Node<T>>
    {
        private T _value;
        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Node(string name)
            : base(name)
        {

        }

        public bool Equals(Node<T> other)
        {
            if (this.CompareTo(other) == 0) return true;
            return false;
        }
    }

    internal abstract class IIterable
    {
        public abstract IIterateur CreateIterator();
    }

    internal class Conteneur<T> : IIterable
    {
        Node<T>[,] noeuds;
        public int Longueur, Largeur;

        public Conteneur(int longueur, int largeur)
        {
            Longueur = longueur;
            Largeur = largeur;
            noeuds = new Node<T>[longueur, largeur];
            ConstruireNoeuds();
        }

        private void ConstruireNoeuds()
        {
            for (int i = 0; i < Longueur; i++)
            {
                for (int j = 0; j < Largeur; j++)
                {
                    noeuds[i, j] = new Node<T>(i + "-" + j);
                }
            }
        }

        public int Count
        {
            get { return Longueur * Largeur; }
        }

        public object this[int x, int y]
        {
            get
            {
                if (x < 0 || y < 0) return null;
                if (x >= Longueur || y >= Largeur) return null;
                return noeuds[x, y];
            }
            set
            {
                if (x < 0 || y < 0) return;
                if (x < Longueur && y < Largeur)
                    noeuds[x, y].Value = (T)value;
            }
        }

        public override IIterateur CreateIterator()
        {
            return new Iterateur<T>(this);
        }

    }

    class ConteneurParcourable<T> : Conteneur<T>
    {
        public ConteneurParcourable(int longueur, int largeur)
            : base(longueur, largeur)
        {
        }
        public void computePaths(Node<T> source, Node<T> dest)
        {
            source.minDistance = 0;
            //  visit each vertex u, always visiting vertex with smallest minDistance first

            PriorityQueue<Node<T>> vertexQueue = new PriorityQueue<Node<T>>();
            vertexQueue.add(source);
            bool firststep = true;


            while (vertexQueue.peek() != null)
            {
                Node<T> u = vertexQueue.remove();
                if (u == dest)
                {
                    source.previous = null;
                    return;
                }
                // Visit each edge exiting u
                foreach (Edge e in u.Edges)
                {
                    Node<T> v = (Node<T>)e.Target;
                    //if(v.is_arrival() && dest != v) continue;
                    //if(firststep && !v.is_Walkable()) continue;
                    int weight = e.weight;
                    int distance = u.minDistance + weight;
                    if (distance < v.minDistance)
                    {
                        vertexQueue.remove(v);
                        v.minDistance = distance;
                        v.previous = u;
                        vertexQueue.add(v);
                        //				    System.out.println("<");
                    }
                    //				System.out.println("edge");
                }
                firststep = false;

                //            System.out.println("IDijkstraNode u = " + u);
            }
            source.previous = null;
        }

        public ArrayList getShortestPathTo(Vertex target)
        {
            ArrayList path = new ArrayList();
            for (Vertex vertex = target; vertex != null; vertex = vertex.previous)
            {
                path.Add(vertex);
            }

            //path.Sort(ReverseComparer.Instance().);
            path.Reverse();
            path.RemoveAt(0);
            return path;
        }

        public ArrayList Route(object paris, object marseille)
        {
            if (paris is Node<T> && marseille is Node<T>)
            {
                computePaths((Node<T>)paris, (Node<T>)marseille);
                return getShortestPathTo((Vertex)marseille);
            }
            else
            {
                throw new InvalidCastException();
            }
        }
    }
}
