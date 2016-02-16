using MetaSimulatorConsole.Simulation.Honeyland.Etats;
using System;

namespace MetaSimulatorConsole.Simulation.Honeyland
{
    public class Bee : PersonnageMobilisable
    {
        public Bee(): base("Abeille",EGame.Honeyland,new TextureBee()){ }
        public Bee(string nom,Coordonnees coor)
            : base("Abeille", EGame.Honeyland, new TextureBee())
        {
            SetCoordonnees(coor);
            Etat = new Etat_Free();
        }

        public override void AnalyserSituation()
        {
            if (Comportement != null)
                Comportement.AnalyserSituation();
        }

        public override void Execution()
        {
            if (Comportement != null)
                Comportement.Execution();
        }
    }
}
