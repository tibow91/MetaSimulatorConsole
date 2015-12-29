using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaSimulatorConsole.Simulation;

namespace MetaSimulatorConsole
{
    public interface IObservateurView
    {
        void UpdateView();
    }
    abstract class Game
    {
        private bool _stop = true;
        public bool Stop
        {
            get { return _stop; }
            set { _stop = value; }
        }

        private bool _started = false;

        public bool Started
        {
            get { return _started;  }
            set { _started = value; }
        }
        protected Grille Tableau;
        public Game(Grille grille)
        {
            this.Tableau = grille;
        }

        protected abstract void RemplirGrille();
        public abstract void LancerSimulation();

        public void AfficherGrille()
        {
            RemplirGrille();
        }
    }

    class GameObservable : Game,IObservateurView
    {
        public GameObservable(Grille grille) : base(grille) { }

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
        public abstract Game CreerJeu(Grille grille);
    }

    class GameAgeOfKebabFactory : GameFactory
    {
        public override Game CreerJeu(Grille grille)
        {
            return new GameAgeOfKebab(grille);
        }
    }

    class GameCDGSimulatorFactory : GameFactory
    {
        public override Game CreerJeu(Grille grille)
        {
            return new GameCDGSimulator(grille);
        }
    }

    class GameHoneywellFactory : GameFactory
    {
        public override Game CreerJeu(Grille grille)
        {
            return new GameHoneywell(grille);
        }
    }


    static class GameFactorySelect
    {
        public static Game CreerJeu(NomJeu nomjeu,Grille grille)
        {
            switch(nomjeu)
            {
                case NomJeu.AgeOfKebab: return new GameAgeOfKebabFactory().CreerJeu(grille);
                case NomJeu.CDGSimulator: return new GameCDGSimulatorFactory().CreerJeu(grille);
                case NomJeu.HoneyWell: return new GameHoneywellFactory().CreerJeu(grille); 
            }

            return null;
        }
    }
}
