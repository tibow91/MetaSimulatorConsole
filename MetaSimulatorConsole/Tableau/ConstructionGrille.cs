using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaSimulatorConsole.Simulation;

namespace MetaSimulatorConsole.Tableau
{
    public abstract class ConstructionGrilleStrategy // PATTERN STRATEGIE + TEMPLATE
    {
        protected Grille Tableau;

        protected ConstructionGrilleStrategy(Grille tableau)
        {
            Tableau = tableau;
        }

        protected virtual Case GetNewCase()
        {
            return new Case();
        }
        private void ConstruireCases()
        {
            int longueur = GameManager.Longueur;
            int largeur = GameManager.Largeur;
            Console.WriteLine("Construction de la Grille selon une zone ({0},{1})", longueur, largeur);

            for (int i = 0; i < longueur; ++i)
                for (int j = 0; j < largeur; ++j)
                    Tableau[i, j] = GetNewCase();
        }
        protected abstract void ConstruireGrilleDepuisZone(ZoneGenerale zoneGenerale);

        private void LierZoneAuTableau(ZoneGenerale zoneGenerale)
        {
            zoneGenerale.LierAuTableauDeJeu();
        }

        public void ConstruireGrilleDepuis(ZoneGenerale zonegenerale)
        {
            ConstruireCases();
            if (zonegenerale == null) return;
            ConstruireGrilleDepuisZone(zonegenerale);
            LierZoneAuTableau(zonegenerale);
        }


    }

    class ConstructionGrilleAOK : ConstructionGrilleStrategy
    {
        protected override Case GetNewCase()
        {
            return new CaseAgeOfKebab();
        }

        protected override void ConstruireGrilleDepuisZone(ZoneGenerale zonegenerale)
        {
            foreach (var zone in zonegenerale.Zones)
            {
                if (zone is ZoneFinale) // On lie toutes les cases adjacentes de la zone entre elles
                {
                    ZoneFinale zonecast = (ZoneFinale)zone;
                    foreach (var coor1 in zonecast.Cases)
                    {
                        foreach (var coor2 in zonecast.Cases)
                        {
                            if (coor1.Equals(coor2)) continue;
                            if (coor1.EstAdjacent(coor2))
                            {
                                var node = (Vertex)Tableau[coor1.X, coor2.Y];
                                node.Edges.Add(new Edge((Vertex)Tableau[coor2.X, coor2.Y], 1));
                            }
                        }
                    }
                    foreach (var zone2 in zonegenerale.Zones)
                    {
                        if (zone2 is ZoneFinale) // On lie tous les points d'accès adjacents dans des zones différentes
                        {
                            ZoneFinale zonecast2 = (ZoneFinale)zone2;
                            if (zonecast.Equals(zonecast2)) continue;
                            // on compare mtn les pts d'accès entre eux
                            foreach (var obj in zonecast.Objets)
                            {
                                if (obj is AccessPoint)
                                {
                                    foreach (var obj2 in zonecast2.Objets)
                                    {
                                        if (obj2 is AccessPoint)
                                        {
                                            if (obj.Case.EstAdjacent(obj2.Case))
                                            {
                                                var node = (Vertex)Tableau[obj.Case.X, obj.Case.Y];
                                                node.Edges.Add(new Edge((Vertex)Tableau[obj2.Case.X, obj2.Case.Y], 1));
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }

            }
        }

        public ConstructionGrilleAOK(Grille tableau) : base(tableau){}
    }
}
