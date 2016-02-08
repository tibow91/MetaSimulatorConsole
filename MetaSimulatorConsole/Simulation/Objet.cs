using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    public abstract class ObjetAbstrait : IEquatable<ObjetAbstrait>
    {
        public EGame TypeSimulation;
        public string Nom { get; set; }
        public Coordonnees Case { get; set; }


        protected ObjetAbstrait(EGame nomdujeu)
        {
            TypeSimulation = nomdujeu;
            Nom = "Objet Sans nom";
        }
        public abstract bool EstValide();
        public override string ToString()
        {
            return "Objet " + Nom + ", " + Case + " (Jeu " + TypeSimulation + ")";
        }

        public bool Equals(ObjetAbstrait other)
        {
            if (other == null) return false;

            if (TypeSimulation == other.TypeSimulation) // Même Simulation
            {
                if (Case.Equals(other.Case))
                {

                    return true;
                }
            }
            return false;
        }
    }
}
