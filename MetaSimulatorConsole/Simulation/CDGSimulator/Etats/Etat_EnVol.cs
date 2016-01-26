using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.CDGSimulator
{
    class Etat_EnVol : EtatAbstract
    {
        public Etat_EnVol()
        {
            this.Nom = "En Vol";
        }
        public override string ModifieEtat(PersonnageAbstract p)
        {
            p.Etat = new Etat_EnVol();

            return String.Format("{0} est maintenant {1}", p.Nom, this.Nom);
        }
    }
}
