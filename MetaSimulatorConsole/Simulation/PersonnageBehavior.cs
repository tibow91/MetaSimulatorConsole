using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{
    [XmlInclude(typeof(ClientBehavior))]
    public abstract class PersonnageBehavior : IObservateurAbstrait
    {
        private PersonnageMobilisable _personnage;
        protected virtual PersonnageMobilisable Personnage 
        {
            get { return _personnage;  }
            set
            {
                _personnage = value;
                Update();
            }
        }

        protected ZoneGenerale ZonePrincipale;
        protected ZoneFinale ZoneActuelle;
        protected EtatAbstract EtatPersonnage;

        public PersonnageBehavior() { }
        protected PersonnageBehavior(PersonnageMobilisable perso)
        {
            Personnage = perso;
            Update();
        }

        public abstract void AnalyserSituation();
        public abstract void Execution();
        public void Update()
        {
            if (Personnage == null) throw new NullReferenceException("Aucun personnage n'est assigné à ce comportement !");            
            ZonePrincipale = Personnage.ZonePrincipale;
            ZoneActuelle = Personnage.ZoneActuelle;
            EtatPersonnage = Personnage.Etat;
                      
        }

        public void SetPersonnage(PersonnageMobilisable personnageMobilisable)
        {
            Personnage = personnageMobilisable;            
        }
    }

}
