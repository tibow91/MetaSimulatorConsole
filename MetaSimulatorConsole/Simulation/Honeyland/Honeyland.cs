using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaSimulatorConsole.Dijkstra;

namespace MetaSimulatorConsole.Simulation
{
    class CaseHoneyland : Case
    {
        public CaseHoneyland() : base(new TextureHerbe()) { }
    }

    public class GameHoneyland : Game
    {
        public GameHoneyland() : base() { NomDuJeu = EGame.Honeyland; }


        public override void ConstruireZones()
        {
            ZoneGenerale = new ZoneMaker().ConstruireZonesHoneyland(this); // A compléter
        }


        protected override void ChargerAlgorithmes()
        {
            throw new NotImplementedException();
        }

        protected override void ConstruireQG()
        {
            throw new NotImplementedException();
        }
    }
}
