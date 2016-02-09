using System.Collections.Generic;

namespace MetaSimulatorConsole
{
    public abstract class IObservateurAbstrait
    {
        public abstract void Update();
    }

    public abstract class SujetObserveAbstrait
    {
        private List<IObservateurAbstrait> observateurList = new List<IObservateurAbstrait>();

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

        public List<IObservateurAbstrait> GetObservers()
        {
            return observateurList;
        }
    }
}
