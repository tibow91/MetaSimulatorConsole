using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    public abstract class ObjetAbstrait
    {
        public EGame TypeSimulation;

        protected ObjetAbstrait(EGame nomdujeu)
        {
            TypeSimulation = nomdujeu;
        }
    }
}
