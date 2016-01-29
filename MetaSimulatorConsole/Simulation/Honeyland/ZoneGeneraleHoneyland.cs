using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.Honeyland
{
    class ZoneGeneraleHoneyland : ZoneComposite 
    {
        public ZoneGeneraleHoneyland(Game simu) 
            : base("Zone Générale Honeyland",simu)
        {
            if (Simulation == null)
            {
                Console.WriteLine("La simulation doit être lancée (instanciée) avant de pouvoir construire les zones");
                return;
            }
        
        }
    }
}
