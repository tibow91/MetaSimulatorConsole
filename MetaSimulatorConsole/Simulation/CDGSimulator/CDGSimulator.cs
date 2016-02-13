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
            NomDuJeu = EGame.CDGSimulator;
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

        protected override void LancerMoteurSimulation()
        {
            Stop = false;
            Running = true;
            Console.WriteLine("Simulation lancée");
            Gestionnaire.UpdateObservers();
            while (!Stop)
            {

            }
            Running = false;
            Console.WriteLine("Simulation arrêtée");
            Gestionnaire.UpdateObservers();
            var node = (Node<Case>)Tableau[0, 0];
            node.Value.SetTextures(new TextureHerbe());
        }

        public override void ConstruireZones()
        {
            ZoneGenerale = new ZoneMaker().ConstruireZonesCDGSimulator(this); // A commpléter
        }
    }

}
