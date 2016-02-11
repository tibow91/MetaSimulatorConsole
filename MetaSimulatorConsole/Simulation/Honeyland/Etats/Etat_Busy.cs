using System;

namespace MetaSimulatorConsole.Simulation.Honeyland.Etats
{
    class Etat_Busy : EtatAbstract
    {
        public Etat_Busy() : base("Occupée")
        {
        }

        public override void ModifieEtat(PersonnageMobilisable p)
        {
            p.Etat = new Etat_Busy();

            Console.WriteLine(String.Format("{0} est maintenant {1}", p.Nom, this.Nom));
        }
    }
}
