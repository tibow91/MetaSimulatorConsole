using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaSimulatorConsole.Dijkstra;

namespace MetaSimulatorConsole.Simulation
{
    class CaseHoneyland : Case
    {
        public CaseHoneyland() : base(new TextureHerbe()) { }
    }

    class GameHoneyland : GameObservable
    {
        public GameHoneyland(GameManager manager, Grille grille)
            : base(manager,grille)
        {
            NomDuJeu = EGame.Honeyland;
            RemplirGrille();
        }
        public override void UpdateView()
        {
            throw new NotImplementedException();
        }

        protected override void RemplirGrille()
        {
            // pour chaque element de la grille
            // Inscrire une texture avec pikachu
            for (int i = 0; i < Tableau.Longueur; ++i)
            {
                for (int j = 0; j < Tableau.Largeur; ++j)
                {
                    Tableau[i, j] = new CaseHoneylandFactory().CreerCase();
                }
            }
        }

        protected override void LancerMoteurSimulation()
        {
            Stop = false;
            Started = true;
            Console.WriteLine("Simulation lancée");
            Gestionnaire.Update();
            while (!Stop)
            {

            }
            Started = false;
            Console.WriteLine("Simulation arrêtée");
            Gestionnaire.Update();
            var node = (Node<Case>)Tableau[49, 49];
            node.Value.SetTextures(new TexturePikachuSurHerbe());
        }

        public override void ConstruireZones()
        {
            ZoneGenerale = new ZoneMaker().ConstruireZonesHoneyland(this); // A compléter
        }
    }
}
