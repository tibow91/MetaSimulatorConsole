﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace MetaSimulatorConsole
{
    public interface IObservateurAbstrait  
    {
        void UpdateDataFromPersonnage();
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

        public void DeAttachAll()
        {
            observateurList.Clear();
        }
        public void UpdateObservers()
        {
            foreach (IObservateurAbstrait o in observateurList)
            {
                o.UpdateDataFromPersonnage();
            }
        }

        public List<IObservateurAbstrait> GetObservers()
        {
            return observateurList;
        }
    }
}
