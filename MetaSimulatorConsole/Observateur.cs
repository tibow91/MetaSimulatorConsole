using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    abstract class IObservateurAbstrait
    {
        public abstract void Update();
    }


    abstract class SujetObserveAbstrait
    {
        private readonly List<IObservateurAbstrait> observateurList = new List<IObservateurAbstrait>();

        public void Attach(IObservateurAbstrait observer)
        {
            observateurList.Add(observer);
        }

        public void DeAttach(IObservateurAbstrait observer)
        {
            observateurList.Remove(observer);
        }

        public void Update()
        {
            foreach (IObservateurAbstrait o in observateurList)
            {
                o.Update();
            }
        }
    }
}
