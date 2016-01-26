using System;

namespace MetaSimulatorConsole.Simulation.CDGSimulator
{
    class Etat_AttenteAutorisation : EtatAbstract
    {
        public Etat_AttenteAutorisation()
        {
            this.Nom = "En attente";
        }
        public override string ModifieEtat(PersonnageAbstract p)
        {
            p.Etat = new Etat_AttenteAutorisation();

            return String.Format("{0} est maintenant {1}", p.Nom, this.Nom);
        }
    }
}
