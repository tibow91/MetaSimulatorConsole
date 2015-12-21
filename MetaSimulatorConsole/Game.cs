using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    abstract class ObservateurView
    {
        public abstract void UpdateView();
    }
    abstract class Game : ObservateurView
    {
        private Grille grille;
        public abstract void UpdateView();
        public Game(Grille grille)
        {
            this.grille = grille;
        }
    }

    class GameAgeOfKebab : Game
    {
        public override void UpdateView()
        {
            throw new NotImplementedException();
        }
    }

    class GameCDGSimulator : Game
    {
        public override void UpdateView()
        {
            throw new NotImplementedException();
        }
    }

    class GameHoneywell : Game
    {
        public override void UpdateView()
        {
            throw new NotImplementedException();
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

    class GameHoneywellFactory : GameFactory
    {
        public override Game CreerJeu()
        {
            return new GameHoneywell();
        }
    }


    static class GameFactorySelect
    {
        public static Game CreerJeu(NomJeu nomjeu)
        {
            switch(nomjeu)
            {
                case NomJeu.AgeOfKebab: return new GameAgeOfKebabFactory().CreerJeu();
                case NomJeu.CDGSimulator: return new GameCDGSimulatorFactory().CreerJeu();
                case NomJeu.HoneyWell: return new GameHoneywellFactory().CreerJeu(); 
            }

            return null;
        }
    }
}
