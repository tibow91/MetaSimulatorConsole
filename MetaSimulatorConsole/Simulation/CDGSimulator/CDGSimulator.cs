using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaSimulatorConsole.Dijkstra;

namespace MetaSimulatorConsole.Simulation
{
    class CaseCDGSimulator : Case
    {
        public CaseCDGSimulator() : base(new TexturePikachuSurHerbe()) { }
    }

    public class GameCDGSimulator : Game
    {
        public GameCDGSimulator() : base() { NomDuJeu = EGame.CDGSimulator; }


        public override void ConstruireZones()
        {
            ZoneGenerale = new ZoneMaker().ConstruireZonesCDGSimulator(this); // A commpléter
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
