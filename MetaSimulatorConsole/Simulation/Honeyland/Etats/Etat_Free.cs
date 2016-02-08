using System;

namespace MetaSimulatorConsole.Simulation.Honeyland.Etats
{
    class Etat_Free : EtatAbstract
    {
        public Etat_Free() : base("Libre")
        {
          
        }

        public override string ModifieEtat(PersonnageAbstract p)
        {
            p.Etat = new Etat_Free();

            return String.Format("{0} est maintenant {1}", p.Nom, this.Nom);
        }
    }
}
