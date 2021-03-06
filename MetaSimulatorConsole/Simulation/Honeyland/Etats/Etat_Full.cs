﻿using System;

namespace MetaSimulatorConsole.Simulation.Honeyland.Etats
{
    class Etat_Full : EtatAbstract
    {
        public Etat_Full() : base("Porteuse de pollen")
        {
        }

        public override void ModifieEtat(PersonnageMobilisable p)
        {
            p.Etat = new Etat_Full();

            Console.WriteLine(String.Format("{0} est maintenant {1}", p.Nom, this.Nom));
        }
    }
}
