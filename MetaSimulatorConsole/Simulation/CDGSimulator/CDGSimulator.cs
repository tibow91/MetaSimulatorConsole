using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaSimulatorConsole.Dijkstra;

namespace MetaSimulatorConsole.Simulation
{
    class CaseCDGSimulator : Case
    {
        public CaseCDGSimulator() : base(new TexturePikachuSurHerbe()) { }
    }

    class GameCDGSimulator : GameObservable
    {
        public GameCDGSimulator(GameManager manager, Grille grille)
            : base(manager,grille)
        {
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
                    Tableau[i, j] = new CaseCDGSimulatorFactory().CreerCase();

                }
            }
        }

        public override void LancerSimulation()
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
            var node = (Node<Case>)Tableau[0, 0];
            node.Value.SetTextures(new TextureHerbe());
        }
    }

}
