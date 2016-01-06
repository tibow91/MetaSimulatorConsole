using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace MetaSimulatorConsole.Fenetre
{
    class Clavier
    {
        private Window Partie;
        private AppuiClavier CommandesClavier;
        private AppuiClavier CommandesSpeciales;

        public Clavier(Window jeu)
        {
            this.Partie = jeu;
            CommandesClavier = AppuiClavier.ChaineDeCommandes(Partie);
            CommandesSpeciales = AppuiClavier.ChaineDeCommandesSpeciales(Partie);
            ChargerConfiguration();
        }
        public void ChargerConfiguration()
        {
            Partie.KeyPress += RetroactionTouches;
            Partie.UpdateFrame += RetroactionTouchesSpeciales;
        }
        private void RetroactionTouches(object sender, KeyPressEventArgs e)
        {
            if (CommandesClavier != null)
                CommandesClavier.Traitement();
        }
        private void RetroactionTouchesSpeciales(object sender, FrameEventArgs e)
        {
            if (CommandesSpeciales != null)
                CommandesSpeciales.Traitement();
        }

        internal List<IObservateurAbstrait> Observers()
        {
            var observers = new List<IObservateurAbstrait>();
            observers.Add(CommandesClavier);
            observers.Add(CommandesSpeciales);
            return observers;
        }
    }
}
