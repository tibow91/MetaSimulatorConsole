using System;

namespace MetaSimulatorConsole.Simulation.Honeyland.Etats
{
    class Etat_Busy : EtatAbstract
    {
        public Etat_Busy()
        {
            this.Nom = "Occupée";
        }

        public override string ModifieEtat(PersonnageAbstract p)
        {
            p.Etat = new Etat_Busy();

            return String.Format("{0} est maintenant {1}", p.Nom, this.Nom);
        }
    }
}
