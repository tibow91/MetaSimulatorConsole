using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{
    public abstract class PersonnageBehavior : IObservateurAbstrait
    {
        protected virtual PersonnageMobilisable Personnage { get; set; }

        protected ZoneGenerale ZonePrincipale;
        protected ZoneFinale ZoneActuelle;
        protected EtatAbstract EtatPersonnage;

        protected PersonnageBehavior(PersonnageMobilisable perso)
        {
            Personnage = perso;
            Update();
        }

        public abstract void AnalyserSituation();
        public abstract void Execution();
        public void Update()
        {
            if (Personnage != null)
            {
                ZonePrincipale = Personnage.ZonePrincipale;
                ZoneActuelle = Personnage.ZoneActuelle;
                EtatPersonnage = Personnage.Etat;
            }            
        }    


    }

}
