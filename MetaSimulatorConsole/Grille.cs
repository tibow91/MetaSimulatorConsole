using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaSimulatorConsole
{
    class Node<T> : Vertex,IComparable,IEquatable<Node<T>>
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
        public ConteneurParcourable(int longueur, int largeur) : base(longueur,largeur)
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

        public ArrayList Route(object paris,object marseille)
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

    class Grille : ConteneurParcourable<Case>
    {
        private static Grille instance;
        protected Grille(int longueur, int largeur)
            : base(longueur, largeur)
        {
            ConstruireGrille();
        }

        public static bool HasInstance()
        {
            if (instance == null) return false;
            return true;
        }

        public static Grille Instance(int longueur,int largeur)
        {
            if(instance == null)
            {
                instance = new Grille(longueur,largeur);
            }

            return instance;
        }

        private void ConstruireGrille()
        {
            Console.WriteLine("Construction de la Grille ({0},{1})",Longueur,Largeur);

            for (int i = 0; i < Longueur; ++i)
            {
                for (int j = 0; j < Largeur; ++j)
                {
                    this[i, j] = new Case();
                }
            }

            var verticalIt = CreateIterator();
     
            for (; verticalIt.CurrentItem() != null; verticalIt.Bas())
            {
                var It = verticalIt.Clone();
                for (; It.CurrentItem() != null; It.Droite())
                {
                    var node = (Vertex)It.CurrentItem();
                    
                    if (It.ItGauche().CurrentItem() != null)
                    {
                        // Ajouter Le noeud de gauche parmi les edges
                        node.Edges.Add(new Edge((Vertex)It.ItGauche().CurrentItem(),1));
                    }
                    if (It.ItDroite().CurrentItem() != null)
                    {
                        // Ajouter Le noeud de droite parmi les edges
                        node.Edges.Add(new Edge((Vertex)It.ItDroite().CurrentItem(), 1));
                    }
                    if (It.ItHaut().CurrentItem() != null)
                    {
                        // Ajouter Le noeud du haut parmi les edges
                        node.Edges.Add(new Edge((Vertex)It.ItHaut().CurrentItem(), 1));
                    }
                    if (It.ItBas().CurrentItem() != null)
                    {
                        // Ajouter Le noeud du bas parmi les edges
                        node.Edges.Add(new Edge((Vertex)It.ItBas().CurrentItem(), 1));
                    }
                }
            }
            Console.WriteLine("Fin de la Construction");
        }

    }
}
