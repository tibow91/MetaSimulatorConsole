using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    abstract class Deplacement
    {
        public abstract String SeDeplacer();
        public static String NePasSeDeplacer()
        {
            return "Je ne me deplace pas";
        }
                
    }
}
