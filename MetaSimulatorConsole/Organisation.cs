using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    enum eMode { Paix, Guerre };
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

    class Organisation : SujetObserveAbstrait
    {
        public eMode ModeFonctionnement { get; set; }
        private Organisation Parent = null;

        public Organisation() { ModeFonctionnement = eMode.Paix; }
        public Organisation(Organisation _parent) 
        {
            Parent = _parent;
            ModeFonctionnement = _parent.ModeFonctionnement;
        }

    }
}
