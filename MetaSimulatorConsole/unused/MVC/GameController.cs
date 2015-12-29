using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    class GameController : IObservateurView
    {
        private GameView View;
        private GameObservable Model;

        public GameController(GameObservable jeu,GameView view)
        {
            this.Model = jeu;
            this.View = view;
        }

        public void UpdateView()
        {
            Model.UpdateView();
        }
    }
}
