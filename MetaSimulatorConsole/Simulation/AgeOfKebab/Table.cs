using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{
    public class Table : ObjetAbstrait
    {
        public Table() : base("Table générique", EGame.AgeOfKebab, new TextureTable()) { }

        public Table(string nom, Coordonnees coor) : base(nom, EGame.AgeOfKebab, new TextureTable())
        {
            SetCoordonnees(coor);
        }
    }
}
