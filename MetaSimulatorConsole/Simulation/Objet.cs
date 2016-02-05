using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    public abstract class ObjetAbstrait
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
            return "Objet " + Nom + ", " + Case + " (Jeu " + TypeSimulation.ToString() + ")";
        }
    }
}
