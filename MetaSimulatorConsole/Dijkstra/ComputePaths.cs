using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaSimulatorConsole.Simulation;

namespace MetaSimulatorConsole.Dijkstra
{
    public abstract class ComputePathsTemplate<T>
    {
        protected ConteneurParcourable<T> Tableau;

        protected ComputePathsTemplate(ConteneurParcourable<T> grille)
        {
            Tableau = grille;
        }
        public void ComputePaths(Node<T> source, Node<T> dest)
        {
            source.minDistance = 0;
            //  visit each vertex u, always visiting vertex with smallest minDistance first

            PriorityQueue<Node<T>> vertexQueue = new PriorityQueue<Node<T>>();
            vertexQueue.add(source);
            bool firststep = true;


            while (vertexQueue.peek() != null)
            {
                Node<T> u = vertexQueue.remove();
                if (u.Equals(dest))
                {
                    source.previous = null;
                    return;
                }
                // Visit each edge exiting u
//                Console.WriteLine("Visiting edges of " + u + "(minDistance = " + u.minDistance + ",nb d'edges = " + u.Edges.Count +" )");
                foreach (Edge e in u.Edges)
                {
                    Node<T> v = (Node<T>)e.Target;
//                    Console.WriteLine("Vertex: " + v + " (minDistance = " + v.minDistance + ", weight = " + e.weight);
                    if (!TestValidNode(v.Coor) && !dest.Equals(v)) continue;
                    //if(v.is_arrival() && dest != v) continue;
                    //if(firststep && !v.is_Walkable()) continue;

                    int distance = u.minDistance + e.weight;
                    if (distance < v.minDistance)
                    {
//                        Console.WriteLine("Distance of edge to " + v + " is min !");
                        vertexQueue.remove(v);
                        v.minDistance = distance;
                        v.previous = u;
                        vertexQueue.add(v);
                    }
//                    				Console.WriteLine("edge");
                }
                firststep = false;
            }
            source.previous = null;
        }

        protected abstract bool TestValidNode(Coordonnees c);

    }

    public class ComputePathDefault<T> : ComputePathsTemplate<T>
    {
        protected override bool TestValidNode(Coordonnees c)
        {
            return true;
        }

        public ComputePathDefault(ConteneurParcourable<T> grille) : base(grille)
        {
        }
    }

    public class ComputePathsAOK : ComputePathsTemplate<Case>
    {
        protected override bool TestValidNode(Coordonnees coor)
        {
            if(coor == null) throw new NullReferenceException("Les coordonnées de la node à valider sont nulles !");
            if(!coor.EstValide()) throw new InvalidOperationException("Les coordonnées renseignées ne sont pas valides");
//            var node = (Node<Case>)tableau[i, j];
            var node = (Node<Case>) Tableau.ElementAt(coor);
            CaseAgeOfKebab c = (CaseAgeOfKebab)node.Value;
//            CaseAgeOfKebab c = node.Value as CaseAgeOfKebab;
            if (c == null) throw new NullReferenceException("Le type de case ne correspond pas !!");
            if (!c.Walkable)
            {
                //Console.WriteLine("Cette case n'est pas Walkable ! " + node.Coor);
                return false; // la case n'est pas valide si elle n'est pas Walkable !
            }
            return true;
        }

        public ComputePathsAOK(Grille grille) : base(grille)
        {
        }
    }
}
