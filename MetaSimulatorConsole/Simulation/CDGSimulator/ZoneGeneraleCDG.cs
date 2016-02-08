using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    class ZoneGeneraleCDG : ZoneGenerale
    {
        public ZoneGeneraleCDG(Game simu) 
            : base("Zone Générale CDG Simulator",simu)
        {
        
        }

        protected override void ConstruireZones()
        {
            throw new NotImplementedException();
        }

        protected override void HierarchiserZones()
        {
            throw new NotImplementedException();
        }

        protected override void DistribuerZones()
        {
            throw new NotImplementedException();
        }
    }
}
