using System;

namespace MetaSimulatorConsole.Simulation.CDGSimulator
{
    class Etat_AttenteAutorisation : EtatAbstract
    {
        public Etat_AttenteAutorisation() : base ("En attente")
        {
        }
        public override void ModifieEtat(PersonnageMobilisable p)
        {
            p.Etat = new Etat_AttenteAutorisation();

            Console.WriteLine(String.Format("{0} est maintenant {1}", p.Nom, this.Nom));
        }
    }
}
