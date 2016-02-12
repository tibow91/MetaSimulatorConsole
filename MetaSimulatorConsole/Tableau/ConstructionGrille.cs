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
            return new CaseAgeOfKebabFactory().CreerCase();
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
            ReinitialiserNodes();
            if (zonegenerale == null) return;
            ConstruireGrilleDepuisZone(zonegenerale);
            LierZoneAuTableau(zonegenerale);
//            zonegenerale.Simulation.Tableau.Afficher();
        }

        private void ReinitialiserNodes()
        {
            for (int i = 0; i < GameManager.Longueur; ++i)
            {
                for (int j = 0; j < GameManager.Largeur; ++j)
                {
                    Vertex v = (Vertex) Tableau[i, j];
                    v.Edges.Clear();
                }
            }
        }


    }

    class ConstructionGrilleAOK : ConstructionGrilleStrategy
    {
        public ConstructionGrilleAOK(Grille tableau) : base(tableau) { }

        protected override Case GetNewCase()
        {
            return new CaseAgeOfKebab();
        }
        
        protected override void ConstruireGrilleDepuisZone(ZoneGenerale zonegenerale)
        {
            var zones = zonegenerale.ObtenirZonesFinales();
            foreach (var zone in zones)
            {
                if (zone == null) continue;
                // On lie toutes les cases adjacentes de la zone entre elles

                foreach (var coor1 in zone.Cases)
                {
                    foreach (var coor2 in zone.Cases)
                    {
                        if (coor1.Equals(coor2)) continue;
                        if (coor1.EstAdjacent(coor2))
                        {
                            var node = (Vertex)Tableau.ElementAt(coor1);
                            node.Edges.Add(new Edge((Vertex)Tableau.ElementAt(coor2), 1));
                        }
                    }
                }
                foreach (var zone2 in zones)
                {
                    if (zone2 == null) continue;
                     // On lie tous les points d'accès adjacents dans des zones différentes
                    
                    if (zone.Equals(zone2)) continue;
                    // on compare mtn les pts d'accès entre eux
                    foreach (var obj in zone.Objets)
                    {
                        if (obj is AccessPoint)
                        {
                            foreach (var obj2 in zone2.Objets)
                            {
                                if (obj2 is AccessPoint)
                                {
                                    if (obj.Case.EstAdjacent(obj2.Case))
                                    {
                                        var node = (Vertex)Tableau.ElementAt(obj.Case);
                                        node.Edges.Add(new Edge((Vertex)Tableau.ElementAt(obj2.Case), 1));
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
