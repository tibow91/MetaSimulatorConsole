using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaSimulatorConsole.Simulation;

namespace MetaSimulatorConsole.Dijkstra
{
    public abstract class ComputePathsStrategy<T>
    {
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
                if (u == dest)
                {
                    source.previous = null;
                    return;
                }
                // Visit each edge exiting u
                foreach (Edge e in u.Edges)
                {
                    Node<T> v = (Node<T>)e.Target;
                    if (!TestValidNode(v)) continue;
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

        protected abstract bool TestValidNode(Node<T> node);

    }

    public class ComputePathDefault<T> : ComputePathsStrategy<T>
    {
        protected override bool TestValidNode(Node<T> node)
        {
            return true;
        }
    }

    public class ComputePathsAOK<T> : ComputePathsStrategy<T>
    {
        protected override bool TestValidNode(Node<T> node)
        {
            return true;
        }
    }
}
