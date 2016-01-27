using System;

namespace MetaSimulatorConsole.Simulation.Honeyland.Etats
{
    class Etat_Full : EtatAbstract
    {
        public Etat_Full()
        {
            this.Nom = "Porteuse de pollen";
        }

        public override string ModifieEtat(PersonnageAbstract p)
        {
            p.Etat = new Etat_Full();

            return String.Format("{0} est maintenant {1}", p.Nom, this.Nom);
        }
    }
}
