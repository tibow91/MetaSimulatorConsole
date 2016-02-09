using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaSimulatorConsole.Simulation;

namespace MetaSimulatorConsole
{
    public abstract class ObjetAbstrait : SujetObserveAbstrait,IEquatable<ObjetAbstrait>
    {
        public EGame TypeSimulation;
        public string Nom { get; set; }
        public Coordonnees Case { get; set; }
        public TextureDecorator Texture { get; set; }

        protected ObjetAbstrait(string nom, EGame nomdujeu,TextureDecorator texture)
        {
            TypeSimulation = nomdujeu;
            Nom = "nom";
            Texture = texture;
        }

        public virtual bool EstValide()
        {
            if (!Case.EstValide())
            {
                Console.WriteLine("Objet " + this + " invalide car la case "+ Case + " n'est pas valide !");
                return false;
            }
            if (Texture == null)
            {
                Console.WriteLine("Objet "+ this + " invalide: Texture nulle !");
                return false;
            }
            return true;
        }
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
