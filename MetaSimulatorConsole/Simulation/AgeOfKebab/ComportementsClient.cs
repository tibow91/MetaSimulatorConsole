using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{
    public class ComportementEnAttenteDeFaim : PersonnageBehavior
    {
        public ComportementEnAttenteDeFaim(Client perso) : base(perso) { }

        /* ici on analyse si l'état du personnage convient selon ses paramètres
         * et on change l'état selon les conclusions. L'état se chargera
         * de changer le comportement à associer automatiquement */
        public override void AnalyserSituation()
        {
            if(Personnage == null) throw new NullReferenceException("Personnage is null !");
            if(EtatPersonnage == null) throw new NullReferenceException("EtatPersonnage is null !");
            if (EtatPersonnage is EtatClientEnAttenteDeFaim)
            {
                if (Personnage.PointsDeVie > Personnage.SeuilCritique) return;
                if (EtatPersonnage == null)
                {
                    Personnage.Etat = new EtatClientVaCommander();
                    UpdateDataFromPersonnage();
                }
            }
        }

        public override void Execution()
        {
            throw new NotImplementedException();
        }

        protected override Coordonnees CaseSuivante()
        {
            throw new NotImplementedException();
        }
    }
}
