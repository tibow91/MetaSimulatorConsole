using System;

namespace MetaSimulatorConsole.Simulation.CDGSimulator
{
    public class Avion : PersonnageAbstract
    {
        public Avion()
            : base("Avion",EGame.CDGSimulator,new TexturePlane1())
        {
        }

        public override void AnalyserSituation()
        {
            throw new NotImplementedException();
        }

        public override void Execution()
        {
            throw new NotImplementedException();
        }


        public override bool EstValide()
        {
            throw new NotImplementedException();
        }
    }
}
