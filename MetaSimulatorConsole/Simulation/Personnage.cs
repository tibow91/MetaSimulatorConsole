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

        public Coordonnees Case { get; set; }
        private TextureDecorator texture;
        public TextureDecorator Texture 
        {
            get { return texture; }
            set
            {
                texture = value;
                UpdateObservers();
            }
        }

        protected PersonnageAbstract(string nom,EGame simulation,TextureDecorator texture)
        {
            Simulation = simulation;
            Nom = nom;
            Texture = texture;
            PointsDeVie = 100;
            SeuilCritique = 50;
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

            return true;
        }
        public override string ToString()
        {
            return "Personnage " + Nom + ", " + PointsDeVie + " XP, " + Case + " (Jeu " + Simulation.ToString() +")";
        }

    }


    public abstract class PersonnageMobilisable : PersonnageAbstract,IPersonnageAMobiliser
    {
        private EtatAbstract etat;
        public EtatAbstract Etat
        {
            get
            {
                return etat;
            }
            set
            {
                etat = value;
                if (etat != null) etat.ModifieEtat(this);
                if (Comportement != null) Comportement.UpdateDataFromPersonnage();
            }
        }

        public PersonnageBehavior Comportement;
        private ZoneGenerale _zonegenerale;
        [XmlIgnore]
        public ZoneGenerale ZonePrincipale
        {
            get
            {
                return _zonegenerale;
            }
            set
            {
                _zonegenerale = value;
                if (Comportement != null) Comportement.UpdateDataFromPersonnage();
            }
        }
        private ZoneFinale _zoneactuelle;

        [XmlIgnore]
        public ZoneFinale ZoneActuelle
        {
            get { return _zoneactuelle;  }
            set
            {
                _zoneactuelle = value;
                if (Comportement != null) Comportement.UpdateDataFromPersonnage();
            }
        }
        protected PersonnageMobilisable(string nom,EGame simulation,TextureDecorator texture)
            : base(nom, simulation, texture)
        {
            Etat = null;
        }
        public void SetZones(ZoneGenerale zonegenerale,ZoneFinale zoneactuelle) // définit le contexte d'exécution
        {
            ZonePrincipale = zonegenerale;
            ZoneActuelle = zoneactuelle;
            if(Comportement != null) Comportement.UpdateDataFromPersonnage();
        }
        
        public override abstract void AnalyserSituation();

        public override abstract void Execution();

        public abstract void Mobiliser();
        public override string ToString()
        {
            return "Personnage " + Nom + ", " + PointsDeVie + " XP, Etat " + Etat + " " + Case + " (Jeu " + Simulation.ToString() + ")";
        }

    }
}
