using System;

namespace MetaSimulatorConsole.Simulation.CDGSimulator
{
    class Etat_Decollage : EtatAbstract
    {
        public Etat_Decollage() : base("En decollage")
        {
        }
        public override void ModifieEtat(PersonnageMobilisable p)
        {
            p.Etat = new Etat_Decollage();

            Console.WriteLine(String.Format("{0} est maintenant {1}", p.Nom, this.Nom));
        }
    }
}
