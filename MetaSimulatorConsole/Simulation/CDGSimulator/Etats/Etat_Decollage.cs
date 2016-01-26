﻿using System;

namespace MetaSimulatorConsole.Simulation.CDGSimulator
{
    class Etat_Decollage : EtatAbstract
    {
        public Etat_Decollage()
        {
            this.Nom = "En décollage";
        }
        public override string ModifieEtat(PersonnageAbstract p)
        {
            p.Etat = new Etat_Decollage();

            return String.Format("{0} est maintenant {1}", p.Nom, this.Nom);
        }
    }
}
}
