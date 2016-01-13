using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.CDGSimulator
{
    abstract class EtatAbstrait
    {
        public abstract string ModifieEtat(Avion a);
    }
}
