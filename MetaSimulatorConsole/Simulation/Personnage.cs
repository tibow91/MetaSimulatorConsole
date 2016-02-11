using System;
using System.Xml.Serialization;
using MetaSimulatorConsole.Simulation.AgeOfKebab;

namespace MetaSimulatorConsole.Simulation
{
//    [XmlInclude(typeof(Client)), XmlInclude(typeof(Serveur))]
    public abstract class PersonnageAbstract : SujetObserveAbstrait, IEquatable<PersonnageAbstract>
    {
        public EGame Simulation;
        public string Nom { get; set; }
        public int PointsDeVie { get; set; }
        public int SeuilCritique { get; set; }

        public EtatAbstract Etat { get; set; }
        public Coordonnees Case { get; set; }
        public TextureDecorator Texture { get; set; }

        protected PersonnageAbstract(string nom,EGame simulation,TextureDecorator texture)
        {
            Simulation = simulation;
            Nom = nom;
            Texture = texture;
            PointsDeVie = 100;
            SeuilCritique = 50;
            Etat = null;
        }

        public void BaisserPointsDeVie()
        {
            if(PointsDeVie > 0) --PointsDeVie;
        }

        public abstract void AnalyserSituation();
        public abstract void Execution();

        public void SetCoordonnees(Coordonnees coor)
        {
            Case = coor;
        }

        public bool Equals(PersonnageAbstract other)
        {
            if (other == null) return false;

            if (Simulation == other.Simulation) // Même Simulation
            {
                if (Case.Equals(other.Case))
                {
                    Console.WriteLine("Equals: Jeu " + Simulation + " Le personnage " + this + " et le personnage " + other +
                                      " partagent les mêmes coordonnées " + Case);
                    return true;
                }
            }
            return false;
        }
        

        public virtual bool EstValide()
        {
            if (!Case.EstValide())
            {
                Console.WriteLine("Personnage " + this + " invalide car la case " + Case + " n'est pas valide !");
                return false;
            }
            if (Texture == null)
            {
                Console.WriteLine("Personnage " + this + " invalide: Texture nulle !");
                return false;
            }
            if (Etat == null)
            {
                Console.WriteLine("Personnage " + this + " invalide: Etat nul !");
                return false;
            }
            return true;
        }
        public override string ToString()
        {
            return "Personnage " + Nom + ", " + PointsDeVie + " XP, Etat " + Etat + " " + Case + " (Jeu " + Simulation.ToString() +")";
        }

    }


    public abstract class PersonnageMobilisable : PersonnageAbstract,IPersonnageAMobiliser
    {
        public PersonnageBehavior Comportement;
        [XmlIgnore]
        public ZoneGenerale ZonePrincipale;
        [XmlIgnore]
        public ZoneFinale ZoneActuelle;
        protected PersonnageMobilisable(string nom,EGame simulation,TextureDecorator texture)
            : base(nom, simulation, texture) { }
        public void SetZones(ZoneGenerale zonegenerale,ZoneFinale zoneactuelle) // définit le contexte d'exécution
        {
            ZonePrincipale = zonegenerale;
            ZoneActuelle = zoneactuelle;
            if(Comportement != null) Comportement.UpdateDataFromPersonnage();
        }
        
        public override abstract void AnalyserSituation();

        public override abstract void Execution();

        public abstract void Mobiliser();
    }
}
