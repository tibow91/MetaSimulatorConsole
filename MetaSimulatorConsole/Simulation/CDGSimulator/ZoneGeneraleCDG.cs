using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    class ZoneGeneraleCDG : ZoneComposite
    {
        public ZoneGeneraleCDG(Game simu) 
            : base("Zone Générale CDG Simulator",simu)
        {
            if (Simulation == null)
            {
                Console.WriteLine("La simulation doit être lancée (instanciée) avant de pouvoir construire les zones");
                return;
            }
        
        }
    }
}
