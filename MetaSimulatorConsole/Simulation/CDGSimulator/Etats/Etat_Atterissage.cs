using System;

namespace MetaSimulatorConsole.Simulation.CDGSimulator
{
    class Etat_Atterissage : EtatAbstract
    {
        public Etat_Atterissage()
            : base("En atterissage")
        {
        }

        public override void ModifieEtat(PersonnageMobilisable p)
        {
            p.Etat = new Etat_Atterissage();

            Console.WriteLine(String.Format("{0} est maintenant {1}", p.Nom, this.Nom));
        }
    }
}
