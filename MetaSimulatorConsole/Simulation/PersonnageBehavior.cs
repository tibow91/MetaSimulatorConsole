using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{
    public abstract class PersonnageBehavior : IObservateurAbstrait
    {
        protected PersonnageMobilisable Personnage;
        protected ZoneGenerale ZonePrincipale;
        protected ZoneFinale ZoneActuelle;
        protected EtatAbstract EtatPersonnage;

        protected PersonnageBehavior(PersonnageMobilisable perso)
        {
            Personnage = perso;
            UpdateDataFromPersonnage();
        }

        public abstract void AnalyserSituation();
        public abstract void Execution();
        protected abstract Coordonnees CaseSuivante();
        public void UpdateDataFromPersonnage()
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
