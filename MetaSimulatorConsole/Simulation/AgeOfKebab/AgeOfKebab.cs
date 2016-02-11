using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaSimulatorConsole.Dijkstra;

namespace MetaSimulatorConsole.Simulation
{
    class CaseAgeOfKebab : Case
    {
        public CaseAgeOfKebab() : base(new TexturePikachuSurHerbe()) { }
    }
    class GameAgeOfKebab : GameObservable
    {

        public GameAgeOfKebab(GameManager manager,Grille grille)
            : base(manager,grille)
        {
            NomDuJeu = EGame.AgeOfKebab;
            //RemplirGrille();
            manager.TableauDeJeu = new GrilleAOK();
            ConstruireZones();
            QG = new QuartierGeneralAOK(this);
            Attach(QG);
        }

        protected override void RemplirGrille()
        {
            // pour chaque element de la grille
            // Inscrire une texture avec pikachu
            for (int i = 0; i < Tableau.Longueur; ++i)
            {
                for (int j = 0; j < Tableau.Largeur; ++j)
                {
                    Tableau[i, j] = new CaseAgeOfKebabFactory().CreerCase();
                }
            }
        }

        public override void UpdateView()
        {
            throw new NotImplementedException();
        }

        protected override void LancerMoteurSimulation()
        {
            Stop = false;
            Started = true;
            Console.WriteLine("Simulation lancée");
            UpdateObservers();
            while (!Stop)
            {

            }
            Started = false;
            Console.WriteLine("Simulation arrêtée");
            UpdateObservers();
            var node = (Node<Case>)Tableau[24, 24];
            node.Value.SetTextures(new TexturePikachuSurHerbe());
        }

        public override void ConstruireZones()
        {
            ZoneGenerale = new ZoneMaker().ConstruireZonesAgeOfKebab(this);
            UpdateObservers(); // pour mettre à jour le QG sur les zones
            new ZoneSerializer().Serialize(ZoneGenerale,"ZoneGenerale");
        }
    }


    class GrilleAOK : Grille
    {
        public GrilleAOK() : base(50, 50) { }
        public override void ConstruireGrille()
        {
            Console.WriteLine("GrilleAOK: Construction de la Grille ({0},{1})", Longueur, Largeur);

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
                        node.Edges.Add(new Edge((Vertex)It.ItGauche().CurrentItem(), 1));
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

        public ConstruireGrille(ZoneGenerale zone)
        {

        }

    }
}
