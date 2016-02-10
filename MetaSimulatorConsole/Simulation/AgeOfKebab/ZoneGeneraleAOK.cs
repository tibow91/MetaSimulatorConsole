using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MetaSimulatorConsole.Dijkstra;
using MetaSimulatorConsole.Simulation;
using MetaSimulatorConsole.Simulation.AgeOfKebab;

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

        protected override void PlacerObjets()
        {
            // POINTS D'ACCES
            AccessPoint.PlacerPoint(ZoneExterne, CaissesClient);
            AccessPoint.PlacerPoint( ZoneExterne, ZoneRepas); 
            AccessPoint.PlacerPoint( CaissesClient, CaissesCuistots); 
            AccessPoint.PlacerPoint( CaissesClient, CaissesCuistots); 
            AccessPoint.PlacerPoint(CaissesClient,ZoneRepas);

            // POINTS D'APPARITION
            SpawnPoint.PlacerPoint(ZoneExterne);

            // POINTS DE RASSEMBLEMENT
            GatherPoint.PlacerPoint(ZoneExterne);
            PlacerCaisses();
            PlacerTables();

        }

        private void PlacerCaisses()
        {
            // Enlever les points d'accès des caisses client vers les zones du personnel (caisses cuistots)
            var list = new List<ObjetAbstrait>(CaissesClient.Objets);
            var listCoor = new List<Coordonnees>();
            foreach (var obj in list) // pour chaque objet
            {
                if (obj is AccessPoint)
                {
                    AccessPoint point = (AccessPoint) obj;
                    if (point.ZoneAnnexes.Contains(CaissesCuistots)) // si pt d'accès vers caisses cuistots
                    {
                        listCoor.Add(obj.Case);
                        CaissesClient.EnleverObjet(obj);
                    }
                }
            }

            // Remplacer aux mêmes coordonnées par des objets de type Caisse
            int i = 1;
            foreach (var coor in listCoor)
            {
                var caisse = new Caisse("Caisse" + i, coor);
                CaissesClient.AjouterObjet(caisse);
                ++i;
            }

            // Enlever les points d'accès de la zone caisse cuistot vers caisses clients
            list = new List<ObjetAbstrait>(CaissesCuistots.Objets);
            foreach (var obj in list)
            {
                if (obj is AccessPoint)
                {
                    AccessPoint point = (AccessPoint) obj;
                    if (point.ZoneAnnexes.Contains(CaissesClient))
                    {
                        CaissesCuistots.EnleverObjet(obj);
                    }
                }
            }

        }

        private void PlacerTables()
        {
            
        }


    }
}
