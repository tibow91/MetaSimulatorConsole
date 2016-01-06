using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation
{
    abstract class IObservateurPersonnage
    {
        public abstract void BaisserPointsDeVie();
    }
    abstract class PersonnageAbstract : IObservateurPersonnage,IEquatable<PersonnageAbstract>
    {
        protected int _lifePoints = 100;

        protected int PointsDeVie
        {
            get { return _lifePoints;}
            set { _lifePoints = value; }
        }

        protected int SeuilCritique = 50;

        public override void BaisserPointsDeVie()
        {
            if(PointsDeVie > 0) --PointsDeVie;
        }

        public abstract void AnalyserSituation();
        public abstract void Execution();

        public bool Equals(PersonnageAbstract other)
        {
            throw new NotImplementedException();
        }

        public abstract void Ajoute(PersonnageAbstract c);
        public abstract void Retire(PersonnageAbstract c);
    }
}
