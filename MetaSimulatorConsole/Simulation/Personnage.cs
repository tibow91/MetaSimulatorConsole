﻿using System;
using System.Xml.Serialization;
using MetaSimulatorConsole.Dijkstra;
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
  
        private Coordonnees _case;
        public virtual Coordonnees Case
        {
            get { return _case;  }
            set { _case = value; }
        } 

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
        protected UnLinkCaseFromPersonnage UnLink = new UnLinkCaseFromPersonnage();
        protected LinkCaseToPersonnage Link = new LinkCaseToPersonnage();
        public override Coordonnees Case
        {
            get { return base.Case; }
            set
            {
                if (base.Case != null && base.Case.EstValide() && ZonePrincipale == null)
                    throw new NullReferenceException("La Zone générale du personnage est nulle !");
                if (ZonePrincipale == null)
                {
                    base.Case = value;
                    return;
                }
                UnLink.LinkObject(base.Case, this, ZonePrincipale.Simulation.Tableau);
                base.Case = value;
                Link.LinkObject(base.Case,this,ZonePrincipale.Simulation.Tableau);
            }
        }

  
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
                if (Comportement != null) Comportement.Update();
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
                if (Comportement != null) Comportement.Update();
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
                if (Comportement != null) Comportement.Update();
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
            if(Comportement != null) Comportement.Update();
        }
        
        public override abstract void AnalyserSituation();

        public override abstract void Execution();

        public virtual void Mobiliser()
        {
//            Console.WriteLine("Route entre (0,0) et (4,4) :");
//                var path = ZonePrincipale.Simulation.Tableau.Route(new Coordonnees(0, 0), new Coordonnees(4, 4)) ;
//                foreach (Vertex elem in path)
//                {
//                    Console.WriteLine(elem.ToString());
//                }
//                ZonePrincipale.Simulation.Tableau.ReinitialiserMinDistances();


            Console.WriteLine("Mobilisation du personnage " + this);
            AnalyserSituation();
            Execution();
//            AfficherCasesNonWalkable();

        }
        private void AfficherCasesNonWalkable()
        {
            var tableau = ZonePrincipale.Simulation.Tableau;
            for (int i = 0; i < GameManager.Longueur; ++i)
            {
                for (int j = 0; j < GameManager.Largeur; ++j)
                {
                    var node = (Node<Case>)tableau[i, j];
                    CaseAgeOfKebab c = (CaseAgeOfKebab)node.Value;
                    if (!c.Walkable) Console.WriteLine("La case " + new Coordonnees(i, j) + " n'est pas Walkable !");
                }
            }
        }
        public override string ToString()
        {
            return "Personnage " + Nom + ", " + PointsDeVie + " XP, Etat " + Etat + " " + Case + " (Jeu " + Simulation.ToString() + ")";
        }

    }
}
