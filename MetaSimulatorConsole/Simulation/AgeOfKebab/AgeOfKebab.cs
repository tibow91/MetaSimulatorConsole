using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaSimulatorConsole.Dijkstra;
using MetaSimulatorConsole.Tableau;

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
            ConstruireZones();
            Tableau.SetAlgoConstruction(new ConstructionGrilleAOK(Tableau));
            Tableau.ConstruireGrilleDepuis(ZoneGenerale);
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

}
