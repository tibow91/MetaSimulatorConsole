using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{
    public class Caisse : ObjetAbstrait
    {
        public Caisse() : base("Caisse générique",EGame.AgeOfKebab, new TextureDollar3D()) { }

        public Caisse(string nom, Coordonnees coor) : base(nom, EGame.AgeOfKebab, new TextureDollar3D())
        {
            SetCoordonnees(coor);
        }

    }
}
