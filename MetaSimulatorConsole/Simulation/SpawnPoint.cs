using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation
{
    public class SpawnPoint : ObjetAbstrait
    {
        public SpawnPoint() : base("Point d'apparition générique",new TextureCrossedCircle()){}
        public SpawnPoint(EGame nomdujeu) : base("Point d'apparition générique", nomdujeu, new TextureCrossedCircle()) { }

        public SpawnPoint(EGame nomdujeu, Coordonnees coor)
            : base("Point d'apparition générique", nomdujeu, new TextureCrossedCircle())
        {
            SetCoordonnees(coor);
        }

        public SpawnPoint(string nom) : base(nom,new TextureCrossedCircle()){}


        public SpawnPoint(string nom, TextureDecorator texture) : base(nom, texture){}

        public SpawnPoint(string nom, EGame nomdujeu, TextureDecorator texture) : base(nom, nomdujeu, texture){}

        public static void PlacerPoint(ZoneFinale zone)
        {
            if (zone == null) return;
            var list = new SpawnPointFinder().FindAvailableCases(zone);
            if (list.Count == 0)
            {
                Console.WriteLine("SpawnPoint -PlacerPoint: Impossible de trouver une case disponible dans la zone " + zone);
                return;
            }
            var point = new SpawnPoint(EGame.AgeOfKebab, list[0]);
            if (!zone.AjouterObjet(point))
                Console.WriteLine("SpawnPoint - PlacerPoint: Erreur rencontrée à l'ajout du pt d'apparition " + point + " dans la zone " + zone);
        }
    }

    class SpawnPointFinder
    {
        public List<Coordonnees> FindAvailableCases(ZoneFinale zone)
        {
            List<Coordonnees> list = new List<Coordonnees>();
            if (zone == null) return list;
            list = zone.Cases;

            foreach (var obj in zone.Objets) // Les cases ayant des objets ne sont pas disponibles pour un pt d'app
                list.Remove(obj.Case);

            return list;
        }
    }
}
