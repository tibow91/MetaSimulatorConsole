using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.Honeyland
{
    class ZoneGeneraleHoneyland : ZoneGenerale
    {
        public ZoneGeneraleHoneyland(Game simu) 
            : base("Zone Générale Honeyland",simu)
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

        protected override void PlacerAccessPoints()
        {
            throw new NotImplementedException();
        }
    }
}
