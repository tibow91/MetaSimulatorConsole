using System;
using MetaSimulatorConsole.Simulation;

namespace MetaSimulatorConsole
{
    public interface IObservateurView
    {
        void UpdateView();
    }

    abstract class Game
    {
        public bool Stop { get; set; }
        public bool Started { get; set; }

        protected Grille Tableau;
        protected GameManager Gestionnaire;
        public Game(GameManager manager,Grille grille)
        {
            Gestionnaire = manager;
            this.Tableau = grille;
        }

        protected abstract void RemplirGrille();
        public abstract void LancerSimulation();

        public void AfficherGrille()
        {
            RemplirGrille();
        }
    }

    abstract class GameObservable : Game,IObservateurView
    {
        public GameObservable(GameManager manager,Grille grille) : base(manager,grille) { }

        public virtual void UpdateView()
        {
        }

        protected override void RemplirGrille(){}


        public override void LancerSimulation()
        {
            throw new NotImplementedException();
        }

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
