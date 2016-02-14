using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaSimulatorConsole.Dijkstra;
using MetaSimulatorConsole.Tableau;
using System.Threading;

namespace MetaSimulatorConsole.Simulation
{
    public class CaseAgeOfKebab : Case
    {
        public bool Walkable = true;
        public CaseAgeOfKebab() : base(new TexturePikachuSurHerbe()) { }
        public override void SetObjectToObserve(ObjetAbstrait obj)
        {
            if (obj == null) Walkable = true;
            else if ((obj is SpawnPoint) || (obj is GatherPoint))
                Walkable = false;
            else
                Walkable = true;
            base.SetObjectToObserve(obj);
        }

        public override void SetPersonnageToObserve(PersonnageAbstract perso)
        {
            if (perso == null)
                Walkable = true;
            else
                Walkable = false;
            base.SetPersonnageToObserve(perso);
        }
    }
    public class GameAgeOfKebab : Game
    {
        public GameAgeOfKebab() : base() 
        {
            NomDuJeu = EGame.AgeOfKebab;
            ZoneGenerale = new ZoneGeneraleAOK();
        }

        protected override void ConstruireQG()
        {
            DetacherAncienQG();
            QG = new QuartierGeneralAOK(this);
            Attach(QG);
        }
        
        
        public override void ConstruireZones()
        {
            ZoneGenerale = new ZoneMaker().ConstruireZonesAgeOfKebab(this);
        }


        protected override void ChargerAlgorithmes()
        {
            if (Tableau == null) throw new NullReferenceException("La grille est nulle !");
            Tableau.SetAlgoConstruction(new ConstructionGrilleAOK(Tableau));
            Tableau.SetAlgoComputePath(new ComputePathsAOK(Tableau));
        }

    }

}
