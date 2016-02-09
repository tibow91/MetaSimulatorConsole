using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation
{
    public abstract class AccessPoint : ObjetAbstrait
    {
        public ZoneFinale ZoneAnnexe;
        protected AccessPoint(string nom, EGame nomdujeu, TextureDecorator texture,ZoneFinale zone)
            : base(nom, nomdujeu, texture)
        {
            ZoneAnnexe = zone;
        }

        public override bool EstValide()
        {
            if (!base.EstValide()) return false;
            if (ZoneAnnexe == null)
            {
                Console.WriteLine("Point D'accès " + this + " invalide: Zone Annexe nulle !");
                return false;
            }
            return true;
        }

        public void SetZoneAnnexe(ZoneFinale zone)
        {
            ZoneAnnexe = zone;
        }
    }

    public class AccessPointVertical : AccessPoint
    {
        public AccessPointVertical(string nom, EGame nomdujeu, ZoneFinale zone)
            : base(nom, nomdujeu, new TextureWoodPlatformVertical(), zone)
        {
        }
    }

    public class AccessPointHorizontal : AccessPoint
    {
        public AccessPointHorizontal(string nom, EGame nomdujeu, ZoneFinale zone)
            : base(nom, nomdujeu, new TextureWoodPlatformHorizontal(), zone)
        {
        }
    }


}
