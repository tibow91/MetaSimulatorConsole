using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaSimulatorConsole
{
    class Node<T> : Vertex
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
            construireNoeuds();
        }

        private void construireNoeuds()
        {
            for (int i = 0; i < Longueur; i++)
            {
                for (int j = 0; j < Largeur; j++)
                {
                    noeuds[i, j] = new Node<T>(i.ToString() + "-" + j.ToString());
                }
            }
        }

        public int Count
        {
            get { return Longueur * Largeur; }
        }

        public T this[int x, int y]
        {
            get { return noeuds[x, y].Value; }
            set { noeuds[x, y].Value = value; }
        }

        public override IIterateur CreateIterator()
        {
            return new Iterateur<T>(this);
        }
    }
    class ConteneurParcourable<T> : Conteneur<T>
    {
        public void computePaths(Vertex source, Vertex dest)
        {
            source.minDistance = 0;
            //  visit each vertex u, always visiting vertex with smallest minDistance first

            PriorityQueue<Vertex> vertexQueue = new PriorityQueue<Vertex>();
            vertexQueue.add(source);
            bool firststep = true;


            while (vertexQueue.peek() != null)
            {
                Vertex u = vertexQueue.remove();
                if (u == dest)
                {
                    source.previous = null;
                    return;
                }
                // Visit each edge exiting u
                foreach (Edge e in u.Edges)
                {
                    Vertex v = e.Target;
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

            path.Sort(ReverseComparer.Instance());
            path.RemoveAt(0);
            return path;
        }

        public ArrayList route(Vertex paris,Vertex marseille)
        {
    	    computePaths(paris, marseille);
    	    return getShortestPathTo(marseille);
        }
    }

    class Grille : ConteneurParcourable<Case>
    {

    }
}
