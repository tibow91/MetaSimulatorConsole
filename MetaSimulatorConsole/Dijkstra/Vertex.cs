using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    public class Vertex : IComparable,IEquatable<Vertex>
    {
        public int minDistance = Int32.MaxValue;
        public readonly String name;
        public Vertex previous;
        public List<Edge> Edges = new List<Edge>();

        public Vertex(String name)
        {
            this.name = name;
        }

        public override String ToString()
        {
            return name;
        }

        public int CompareTo(object other)
        {
            if(other == null)
                throw new ArgumentNullException();
            if(other is Vertex)
            {
                var vertex = (Vertex) other;
                return minDistance.CompareTo(vertex.minDistance);
            }
            else
            {
                throw new InvalidCastException();   
            }
        }


        public bool Equals(Vertex other)
        {
            if (this.CompareTo(other) == 0) return true;
            return false;
        }
    }

    public class Edge
    {
        public Vertex Target;
        public int weight;
        public Edge(Vertex argTarget, int argWeight)
        { 
            Target = argTarget; 
            weight = argWeight;
        }
    }


}
