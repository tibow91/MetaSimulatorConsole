using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaSimulatorConsole.Dijkstra;
using MetaSimulatorConsole.Simulation;

namespace MetaSimulatorConsole
{

    public class Grille : ConteneurParcourable<Case> 
    {
        private static Grille instance;


        public static bool HasInstance()
        {
            if (instance == null) return false;
            return true;
        }

        public static Grille Instance(int longueur, int largeur)
        {
            if(instance == null)
            {
                instance = new Grille(longueur, largeur);
            }

            return instance;
        }
        protected Grille(int longueur, int largeur)
            : base(longueur, largeur)
        {
            ConstruireGrille();
        }
        public virtual void ConstruireGrille()
        {
            Console.WriteLine("Construction de la Grille ({0},{1})",Longueur,Largeur);

            for (int i = 0; i < Longueur; ++i)
            {
                for (int j = 0; j < Largeur; ++j)
                {
                    this[i, j] = new Case();
                }
            }

            var verticalIt = CreateIterator();
     
            for (; verticalIt.CurrentItem() != null; verticalIt.Bas())
            {
                var It = verticalIt.Clone();
                for (; It.CurrentItem() != null; It.Droite())
                {
                    var node = (Vertex)It.CurrentItem();
                    
                    if (It.ItGauche().CurrentItem() != null)
                    {
                        // Ajouter Le noeud de gauche parmi les edges
                        node.Edges.Add(new Edge((Vertex)It.ItGauche().CurrentItem(),1));
                    }
                    if (It.ItDroite().CurrentItem() != null)
                    {
                        // Ajouter Le noeud de droite parmi les edges
                        node.Edges.Add(new Edge((Vertex)It.ItDroite().CurrentItem(), 1));
                    }
                    if (It.ItHaut().CurrentItem() != null)
                    {
                        // Ajouter Le noeud du haut parmi les edges
                        node.Edges.Add(new Edge((Vertex)It.ItHaut().CurrentItem(), 1));
                    }
                    if (It.ItBas().CurrentItem() != null)
                    {
                        // Ajouter Le noeud du bas parmi les edges
                        node.Edges.Add(new Edge((Vertex)It.ItBas().CurrentItem(), 1));
                    }
                }
            }
            Console.WriteLine("Fin de la Construction");
        }

        public void ConstruireGrilleDepuis(ZoneGenerale zonegenerale)
        {
            Console.WriteLine("GrilleAOK: Construction de la Grille selon une zone ({0},{1})", Longueur, Largeur);

            for (int i = 0; i < Longueur; ++i)
                for (int j = 0; j < Largeur; ++j)
                    this[i, j] = new Case();
            if (zonegenerale == null) return;

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
                                var node = (Vertex)this[coor1.X, coor2.Y];
                                node.Edges.Add(new Edge((Vertex)this[coor2.X, coor2.Y], 1));
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
                                                var node = (Vertex)this[obj.Case.X, obj.Case.Y];
                                                node.Edges.Add(new Edge((Vertex)this[obj2.Case.X, obj2.Case.Y], 1));
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }

            }
            zonegenerale.RelierAuTableauDeJeu();


        }

    }

}
