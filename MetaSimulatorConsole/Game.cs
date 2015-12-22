using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    public interface IObservateurView
    {
        void UpdateView();
    }
    abstract class Game
    {
        protected Grille Tableau;
        public Game(Grille grille)
        {
            this.Tableau = grille;
        }

        protected abstract void RemplirGrille();
    }

    class GameObservable : Game,IObservateurView
    {
        public GameObservable(Grille grille) : base(grille) { }

        public virtual void UpdateView()
        {
        }

        protected override void RemplirGrille(){}
    
    }
    class GameAgeOfKebab : GameObservable
    {
        protected override void RemplirGrille()
        {
            // pour chaque element de la grille
            // Inscrire une texture avec pikachu
            for (int i = 0; i < Tableau.Longueur; ++i)
            {
                for (int j = 0; j < Tableau.Largeur; ++j)
                {
                    Tableau[i, j] = new Case(new TexturePikachu());
                }
            }
        }
        public GameAgeOfKebab(Grille grille) : base(grille)
        {
            RemplirGrille();
        }

        public override void UpdateView()
        {
            throw new NotImplementedException();
        }

    }

    class GameCDGSimulator : GameObservable
    {
        public GameCDGSimulator(Grille grille) : base(grille)
        {
            RemplirGrille();
        }

        public override void UpdateView()
        {
 	        throw new NotImplementedException();
        }

        protected override void RemplirGrille()
        {
            // pour chaque element de la grille
            // Inscrire une texture avec pikachu
            for (int i = 0; i < Tableau.Longueur; ++i)
            {
                for (int j = 0; j < Tableau.Largeur; ++j)
                {
                    Tableau[i, j] = new Case(new TexturePikachu(new TextureHerbe()));
                }
            }
        }
    }

    class GameHoneywell :  GameObservable
    {
        public GameHoneywell(Grille grille)
            : base(grille)
        {
        }
        public override void UpdateView()
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
