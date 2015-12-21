using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    class GameController : ObservateurView
    {
        private GameView View;
        private Game Model;

        public GameController(Game jeu,GameView view)
        {
            this.Model = jeu;
            this.View = view;
        }

        public override void UpdateView()
        {
            Model.UpdateView();
        }
    }
}
