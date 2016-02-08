using System;

namespace MetaSimulatorConsole.Simulation.CDGSimulator
{
    class Etat_Atterissage : EtatAbstract
    {
        public Etat_Atterissage()
            : base("En atterissage")
        {
        }

        public override string ModifieEtat(PersonnageAbstract p)
        {
            p.Etat = new Etat_Atterissage();

            return String.Format("{0} est maintenant {1}", p.Nom, this.Nom);
        }
    }
}
