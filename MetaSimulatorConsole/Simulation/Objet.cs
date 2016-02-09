using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MetaSimulatorConsole.Simulation;

namespace MetaSimulatorConsole
{
    [XmlInclude(typeof(AccessPoint)), XmlInclude(typeof(SpawnPoint))]
    public abstract class ObjetAbstrait : SujetObserveAbstrait,IEquatable<ObjetAbstrait>
    {
        [XmlAttribute]
        public string Nom { get; set; }
        [XmlAttribute]
        public EGame TypeSimulation;
        public Coordonnees Case { get; set; }
        public TextureDecorator Texture { get; set; }

        protected ObjetAbstrait(string nom)
        {
            Nom = nom;
        }
        protected ObjetAbstrait(string nom, TextureDecorator texture)
        {
            Nom = nom;
            Texture = texture;
        }
        protected ObjetAbstrait(string nom, EGame nomdujeu,TextureDecorator texture)
        {
            TypeSimulation = nomdujeu;
            Nom = nom;
            Texture = texture;
        }

        public void SetCoordonnees(Coordonnees coor)
        {
            Case = coor;
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
