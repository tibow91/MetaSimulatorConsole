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
            : base("En Vol")
        {
        }
        public override void ModifieEtat(PersonnageMobilisable p)
        {
            p.Etat = new Etat_EnVol();

            Console.WriteLine(String.Format("{0} est maintenant {1}", p.Nom, this.Nom));
        }
    }
}
