using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaSimulatorConsole
{
    abstract class GameView
    {
        private GameObservable Jeu;
        public GameView(GameObservable jeu)
        {
            this.Jeu = jeu;
        }
        public void Afficher()
        {
            Jeu.UpdateView();
        }
    }
}
