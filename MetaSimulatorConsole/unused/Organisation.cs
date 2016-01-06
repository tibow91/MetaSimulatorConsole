using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    enum eMode { Paix, Guerre };


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
