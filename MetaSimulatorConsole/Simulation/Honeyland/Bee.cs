using System;

namespace MetaSimulatorConsole.Simulation.Honeyland
{
    public class Bee : PersonnageAbstract
    {
        public Bee()
            : base("Abeille",EGame.Honeyland,new TextureBee())
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
