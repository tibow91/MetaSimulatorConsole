using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaSimulatorConsole.Dijkstra;
using MetaSimulatorConsole.Simulation;
using MetaSimulatorConsole.Tableau;

namespace MetaSimulatorConsole
{

    public class Grille : ConteneurParcourable<Case> 
    {
        private static Grille instance;
        protected ConstructionGrilleStrategy AlgoConstruction;

        public static bool HasInstance()
        {
            if (instance == null) return false;
            return true;
        }

        public void SetAlgoConstruction(ConstructionGrilleStrategy algo)
        {
            AlgoConstruction = algo;
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
            if (AlgoConstruction == null)
            {
                Console.WriteLine("Aucun algorithme de construction n'a été sélectionné");
                return;
            }
            AlgoConstruction.ConstruireGrilleDepuis(zonegenerale);
        }

    }

}
