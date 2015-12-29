using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaSimulatorConsole.Dijkstra;

namespace MetaSimulatorConsole.Simulation
{
    class GameHoneyland : GameObservable
    {
        public GameHoneyland(Grille grille)
            : base(grille)
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
                    Tableau[i, j] = new Case(new TextureHerbe());
                }
            }
        }

        public override void LancerSimulation()
        {
            Stop = false;
            Started = true;
            Console.WriteLine("Simulation lancée");
            while (!Stop)
            {

            }
            Started = false;
            Console.WriteLine("Simulation arrêtée");
            var node = (Node<Case>)Tableau[49, 49];
            node.Value.SetTextures(new TexturePikachuSurHerbe());
        }
    }
}
