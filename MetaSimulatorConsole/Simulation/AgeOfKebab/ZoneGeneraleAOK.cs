using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MetaSimulatorConsole.Dijkstra;
using MetaSimulatorConsole.Simulation;

namespace MetaSimulatorConsole
{
    public class ZoneGeneraleAOK : ZoneGenerale
    {
        [XmlIgnore]
        private ZoneComposite ZoneInterne;
        [XmlIgnore]
        private ZoneFinale ZoneExterne, CaissesClient, CaissesCuistots, ZoneRepas;

        public ZoneGeneraleAOK() : base("Zone Générale Age Of kebab", null) { }
        public ZoneGeneraleAOK(Game simu) 
            : base("Zone Générale Age Of kebab",simu)
        {
        }

        protected  override void ConstruireZones()
        {
            ZoneInterne = new ZoneComposite("Zone Interne ", Simulation);
            ZoneExterne = new ZoneFinale("Zone Externe ", new TextureHerbe(), Simulation);
            CaissesClient = new ZoneFinale("Caisses client",new TextureGround2(),  Simulation);
            CaissesCuistots = new ZoneFinale("Caisses cuistots", new TextureGround1(), Simulation);
            ZoneRepas = new ZoneFinale("Zone Repas ",new TextureMozaic1(),  Simulation);

        }

        protected  override void  HierarchiserZones()
        {
            AjouterZone(ZoneInterne);
            AjouterZone(ZoneExterne);
            ZoneInterne.AjouterZone(CaissesClient);
            ZoneInterne.AjouterZone(CaissesCuistots);
            ZoneInterne.AjouterZone(ZoneRepas);   
        }
        protected  override void DistribuerZones()
        {
            var link = new LinkCaseToZone();
            for (int i = 0; i < GameManager.Longueur; ++i)
            {
                for (int j = 0; j < GameManager.Largeur; ++j)
                {
                    if (i < GameManager.Longueur/2)
                    {
                        if (j < GameManager.Largeur/3)
                        {
                            // ZONE DEHORS
                            var coor = new Coordonnees(i, j);
                            ZoneExterne.AjouterCase(coor);
                            link.LinkObject(coor, ZoneExterne, Simulation.Tableau);

                        }
                        else if (j < (3*GameManager.Largeur/4))
                        {
                            // CAISSES CLIENTS
                            var coor = new Coordonnees(i, j);
                            CaissesClient.AjouterCase(coor);
                            link.LinkObject(coor, CaissesClient, Simulation.Tableau);
                        }
                        else
                        {
                            // CAISSES CUISTOTS
                            var coor = new Coordonnees(i, j);
                            CaissesCuistots.AjouterCase(coor);
                            link.LinkObject(coor, CaissesCuistots, Simulation.Tableau);
                        }

                    }
                    else
                    {
                        if (j < GameManager.Largeur/3)
                        {
                            // ZONE EXTERNE
                            var coor = new Coordonnees(i, j);
                            ZoneExterne.AjouterCase(coor);
                            link.LinkObject(coor, ZoneExterne, Simulation.Tableau);
                        }
                        else
                        {
                            // ZONE REPAS
                            var coor = new Coordonnees(i, j);
                            ZoneRepas.AjouterCase(coor);
                            link.LinkObject(coor, ZoneRepas, Simulation.Tableau);
                        }
                    }
                   
                }
            }
        }

        protected override void PlacerAccessPoints()
        {
            // POINTS D'ACCES
            AccessPoint.PlacerPoint(ZoneExterne, CaissesClient);
            AccessPoint.PlacerPoint( ZoneExterne, ZoneRepas); 
            AccessPoint.PlacerPoint( CaissesClient, CaissesCuistots); 
            AccessPoint.PlacerPoint( CaissesClient, CaissesCuistots); 
            AccessPoint.PlacerPoint(CaissesClient,ZoneRepas);

            // POINTS D'APPARITION
            SpawnPoint.PlacerPoint(ZoneExterne);
            SpawnPoint.PlacerPoint(ZoneExterne);

        }

    }
}
