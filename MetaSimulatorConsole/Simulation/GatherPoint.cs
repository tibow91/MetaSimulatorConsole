using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation
{
    public class GatherPoint : ObjetAbstrait
    {
        public GatherPoint(string nom) : base(nom)
        {
        }
        public GatherPoint() : base("Point d'apparition générique",new TextureFootIcon()){}
        public GatherPoint(EGame nomdujeu) : base("Point d'apparition générique", nomdujeu, new TextureFootIcon()) { }

        public GatherPoint(EGame nomdujeu, Coordonnees coor)
            : base("Point de rassemblement générique", nomdujeu, new TextureFootIcon())
        {
            SetCoordonnees(coor);
        }
        public GatherPoint(string nom, TextureDecorator texture) : base(nom, texture)
        {
        }

        public GatherPoint(string nom, EGame nomdujeu, TextureDecorator texture) : base(nom, nomdujeu, texture)
        {
        }

        public static void PlacerPoint(ZoneFinale zone)
        {
            if (zone == null) return;
            var list = new ObjectPointFinder().FindAvailableCases(zone);
            if (list.Count == 0)
            {
                Console.WriteLine("GatherPoint -PlacerPoint: Impossible de trouver une case disponible dans la zone " + zone);
                return;
            }
            var point = new GatherPoint(EGame.AgeOfKebab, list[list.Count/2]);
            if (!zone.AjouterObjet(point))
                Console.WriteLine("GatherPoint - PlacerPoint: Erreur rencontrée à l'ajout du pt d'apparition " + point + " dans la zone " + zone);
     
        }

    }
}
