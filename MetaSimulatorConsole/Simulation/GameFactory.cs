using System;
using System.Xml.Serialization;
using MetaSimulatorConsole.Simulation;

namespace MetaSimulatorConsole
{
    public interface IObservateurView
    {
        void UpdateView();
    }

    public enum EGame { AgeOfKebab, CDGSimulator, Honeyland}

    public abstract class Game : SujetObserveAbstrait, IEquatable<Game>,IObservateurAbstrait
    {
        public EGame NomDuJeu;
        public bool Stop { get; set; }
        public bool Started { get; set; }

        public Grille Tableau;
        protected GameManager Gestionnaire;
        public ZoneGenerale ZoneGenerale;
        [XmlIgnore]
        public QuartierGeneralAbstrait QG;
        protected Game(GameManager manager,Grille grille)
        {

            Gestionnaire = manager;
            Attach(Gestionnaire); // Le gestionnaire met à jours les classes système selon le jeu
            Gestionnaire.Attach(this); // Le jeu met à jour son tableau selon le gestionnaire
            this.Tableau = grille;
            //RemplirGrille();
            //ConstruireZones();
        }

        protected abstract void RemplirGrille();
        public void LancerSimulation()
        {
            // si la zone générale est pas valide   // ne pas lancer la simulation
            if (!ZoneGenerale.EstValide())
            {
                Console.WriteLine("La simulation ne peut être lancée car la zone générale n'est pas valide !");
                return;
            }
            if (!VerifierValiditeZones())
            {
                Console.WriteLine("La simulation ne peut être lancée car les zones ne sont pas conformes !");
                return;
            }
            LancerMoteurSimulation();
        }

        protected abstract void LancerMoteurSimulation();

        public void AfficherGrille()
        {
            RemplirGrille();
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

    abstract class GameObservable : Game
    {
        protected GameObservable(GameManager manager,Grille grille) : base(manager,grille) { }

        public virtual void UpdateView()
        {
        }

        protected override void RemplirGrille(){}
        
        protected abstract override void LancerMoteurSimulation();

        public abstract override void ConstruireZones();


    }
   

    abstract class GameFactory 
    {
        public abstract Game CreerJeu(GameManager manager, Grille grille);
    }

    class GameAgeOfKebabFactory : GameFactory
    {
        public override Game CreerJeu(GameManager manager, Grille grille) 
        {
            return new GameAgeOfKebab(manager,grille);
        }
    }

    class GameCDGSimulatorFactory : GameFactory
    {
        public override Game CreerJeu(GameManager manager, Grille grille) 
        {
            return new GameCDGSimulator(manager,grille);
        }
    }

    class GameHoneylandFactory : GameFactory
    {
        public override Game CreerJeu(GameManager manager, Grille grille) 
        {
            return new GameHoneyland(manager,grille);
        }
    }


    static class GameFactorySelect
    {
        public static Game CreerJeu(NomJeu nomjeu, GameManager manager, Grille grille) 
        {
            switch(nomjeu)
            {
                case NomJeu.AgeOfKebab: return new GameAgeOfKebabFactory().CreerJeu(manager,grille);
                case NomJeu.CDGSimulator: return new GameCDGSimulatorFactory().CreerJeu(manager,grille);
                case NomJeu.Honeyland: return new GameHoneylandFactory().CreerJeu(manager,grille); 
            }

            return null;
        }
    }
}
