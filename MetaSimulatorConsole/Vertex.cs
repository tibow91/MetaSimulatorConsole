using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    class Vertex : IComparable<Vertex>,IEquatable<Vertex>
    {
        public int minDistance = Int32.MaxValue;
        public readonly String name;
        public Vertex previous;
        public List<Edge> Edges = new List<Edge>();

        public Vertex(String name)
        {
            this.name = name;
        }

        public  String ToString()
        {
            return name;
        }

        public int CompareTo(Vertex other)
        {
            return minDistance.CompareTo(other.minDistance);
        }


        public bool Equals(Vertex other)
        {
            if (this.CompareTo(other) == 0) return true;
            return false;
        }
    }

    class Edge
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
