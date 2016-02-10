using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{
    public abstract  class EtatClient 
    {
        public abstract void ModifieEtat(Client client);
        public abstract String AfficherEtat();
    }

}
