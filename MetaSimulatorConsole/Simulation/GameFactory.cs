using System;
using System.Xml.Serialization;
using MetaSimulatorConsole.Simulation;
using System.Threading;

namespace MetaSimulatorConsole
{
    public interface IObservateurView
    {
        void UpdateView();
    }

    public enum EGame { AgeOfKebab, CDGSimulator, Honeyland}

    [XmlInclude(typeof(GameAgeOfKebab)), XmlInclude(typeof(GameCDGSimulator)), XmlInclude(typeof(GameHoneyland))]
    public abstract class Game : SujetObserveAbstrait, IEquatable<Game>,IObservateurAbstrait
    {
        public EGame NomDuJeu;

        [XmlIgnore]
        public bool Stop { get; set; } // Permet d'arrêter la simulation
        [XmlIgnore]
        public bool Running { get; set; } // Indique si la simulation est en cours
        [XmlIgnore]

        public Grille Tableau;
        [XmlIgnore]
        protected GameManager Gestionnaire;
        public ZoneGenerale ZoneGenerale;
        public QuartierGeneralAbstrait QG;
        private bool AttachedToManager,QGSet,PersoLoaded;
        public Game() {       }

        protected void DetacherAncienQG()
        {
            DeAttachAll();
        }
        protected abstract void ConstruireQG();
        public bool EstValide()
        {
            // si la zone générale est pas valide   // ne pas lancer la simulation
            if (!ZoneGenerale.EstValide())
            {
                Console.WriteLine("La simulation ne peut être lancée car la zone générale n'est pas valide !");
                return false;
            }
            if (!VerifierValiditeZones())
            {
                Console.WriteLine("La simulation ne peut être lancée car les zones ne sont pas conformes !");
                return false;
            }
            return true;
        }

        public void LancerSimulation()
        {
            if (!EstValide()) return;
            LancerMoteurSimulation();
        }

        protected void AttachToGameManager(GameManager manager)
        {
            Gestionnaire = manager;
            Tableau = manager.TableauDeJeu;
            if (Tableau == null) throw new NullReferenceException("La grille est nulle !");
            if (!QGSet){
                QG.Simulation = this;
                QGSet = true;
            }
            if (AttachedToManager) return;
            Attach(Gestionnaire); // Le gestionnaire met à jours les classes système selon le jeu
            Gestionnaire.Attach(this); // Le jeu met à jour son tableau selon le gestionnaire
        }


        public virtual void CreerUneNouvellePartie(GameManager manager) // Doit charger les éléments par défault des zones
        {
            ConstruireQG(); // Détacher l'ancien QG d'abord s'il y a
            AttachToGameManager(manager);
            ConstruireZones();
            QGSet = true;
            RechargerLaPartie(manager);
        }
        public virtual void RechargerLaPartie(GameManager manager)
        {
            AttachToGameManager(manager);            
            ChargerAlgorithmes();
            if (ZoneGenerale == null) throw new NullReferenceException("La zone générale est nulle !");
            ZoneGenerale.AttacherAuJeu(this);
            if(!PersoLoaded){
                ZoneGenerale.LierZonesAuxPersonnages(ZoneGenerale);
                ZoneGenerale.RattacherAccessPoints();
                PersoLoaded = true;
            }
            Tableau.ConstruireGrilleDepuis(ZoneGenerale);
            UpdateObservers(); // pour mettre à jour le QG sur les zones
        }

        protected abstract void ChargerAlgorithmes();

        private void LancerMoteurSimulation()
        {
            Stop = false;
            Running = true;
            Console.WriteLine("Simulation lancée");
            UpdateObservers();
            while (!Stop)
            {
                if (QG == null) throw new NullReferenceException("QG is null !");
                QG.GererUnTour(true);
                Thread.Sleep(2000);
            }
            Running = false;
            Console.WriteLine("Simulation arrêtée");
            UpdateObservers();
        }

        public bool Equals(Game other)
        {
            if (NomDuJeu == other.NomDuJeu)
                return true;
            return false;
        }

        public abstract void ConstruireZones();

        public bool VerifierValiditeZones()
        {
            //ZG est valide si toutes les zones occupent toutes les cases de la grille
            for (int i = 0; i < GameManager.Longueur; ++i)
            {
                for (int j = 0; j < GameManager.Largeur; ++j)
                {
                    if (!ZoneGenerale.ContientCoordonnees(new Coordonnees(i, j)))
                    {
                        Console.WriteLine("La zone générale " + ZoneGenerale + " ne contient pas les coordonéees " + new Coordonnees(i, j));
                        return false;
                    }
                }
            }
            return true;
        }

        public void Update()
        {
            Tableau = Gestionnaire.TableauDeJeu;
        }

    }
    
    abstract class GameFactory 
    {
        public abstract Game CreerJeu();
    }

    class GameAgeOfKebabFactory : GameFactory
    {
        public override Game CreerJeu() 
        {
            return new GameAgeOfKebab();
        }
    }

    class GameCDGSimulatorFactory : GameFactory
    {
        public override Game CreerJeu() 
        {
            return new GameCDGSimulator();
        }
    }

    class GameHoneylandFactory : GameFactory
    {
        public override Game CreerJeu() 
        {
            return new GameHoneyland();
        }
    }


    static class GameFactorySelect
    {
        public static Game CreerJeu(NomJeu nomjeu, GameManager manager, Grille grille) 
        {
            switch(nomjeu)
            {
                case NomJeu.AgeOfKebab: return new GameAgeOfKebabFactory().CreerJeu();
                case NomJeu.CDGSimulator: return new GameCDGSimulatorFactory().CreerJeu();
                case NomJeu.Honeyland: return new GameHoneylandFactory().CreerJeu(); 
            }

            return null;
        }
    }
}
