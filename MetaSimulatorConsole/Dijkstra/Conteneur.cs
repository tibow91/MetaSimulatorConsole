using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Dijkstra
{
    public class Node<T> : Vertex, IComparable, IEquatable<Node<T>>
    {
        private T _value;
        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Coordonnees Coor;

        public Node(Coordonnees coor)
            : base(coor.ToString())
        {
            Coor = coor;
        }

        public bool Equals(Node<T> other)
        {
            if (other.Coor.Equals(Coor)) return true;
            return false;
        }
    }

    public abstract class IIterable
    {
        public abstract IIterateur CreateIterator();
    }

    public class Conteneur<T> : IIterable
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
                    noeuds[i, j] = new Node<T>(new Coordonnees(i,j));
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

        public object ElementAt(Coordonnees coor)
        {
            if (!coor.EstValide()) return null;
            return this[coor.X, coor.Y];
        }

        public override IIterateur CreateIterator()
        {
            return new Iterateur<T>(this);
        }

    }

    public class ConteneurParcourable<T> : Conteneur<T>
    {
        private ComputePathsStrategy<T> AlgoComputePath;
        public ConteneurParcourable(int longueur, int largeur)
            : base(longueur, largeur)
        {
            SetAlgoComputePath(new ComputePathDefault<T>(this));
        }

        public void SetAlgoComputePath(ComputePathsStrategy<T> algo)
        {
            AlgoComputePath = algo;
        }
        public void computePaths(Node<T> source, Node<T> dest)
        {
            AlgoComputePath.ComputePaths(source,dest);
            source.minDistance = 0;

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

        public ArrayList Route(Coordonnees paris, Coordonnees marseille)
        {
            if(!paris.EstValide()) throw new InvalidOperationException("Route: Les coordonnées choisies sont invalides !");
            if (!marseille.EstValide()) throw new InvalidOperationException("Route: Les coordonnées choisies sont invalides !");
            computePaths((Node<T>)this[paris.X,paris.Y], (Node<T>)this[marseille.X,marseille.Y]);
            return getShortestPathTo((Vertex)this[marseille.X,marseille.Y]);
        }
    }
}
