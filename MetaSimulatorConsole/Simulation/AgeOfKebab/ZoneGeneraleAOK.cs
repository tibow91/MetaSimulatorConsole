using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MetaSimulatorConsole.Dijkstra;

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
            ZoneInterne = new ZoneComposite("Zone Interne (Kebab)", Simulation);
            ZoneExterne = new ZoneFinale("Zone Externe (Dehors)", new TextureHerbe2(), Simulation);
            CaissesClient = new ZoneFinale("Zone Finale: Caisses client (Files d'attente)",new TextureGround2(),  Simulation);
            CaissesCuistots = new ZoneFinale("Zone Finale: Caisses cuistots (côté serveur)", new TextureGround1(), Simulation);
            ZoneRepas = new ZoneFinale("Zone Finale: Repas pour les clients (chaises et tables)",new TextureMozaic1(),  Simulation);

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
            for (int i = 0; i < GameManager.Longueur; ++i)
            {
                for (int j = 0; j < GameManager.Largeur; ++j)
                {
                    if (i < GameManager.Longueur/2)
                    {
                        if (j < GameManager.Largeur/2)
                        {
                            ZoneExterne.AjouterCase(new Coordonnees(i, j));
                            var node = (Node<Case>) Simulation.Tableau[i, j];
                            node.Value.SetZoneToObserve(ZoneExterne);
                        }
                        else
                        {
                            CaissesClient.AjouterCase(new Coordonnees(i, j));
                            var node = (Node<Case>)Simulation.Tableau[i, j];
                            node.Value.SetZoneToObserve(CaissesClient);
                        }
                    }
                    else
                    {
                        if (j < GameManager.Longueur/2)
                        {
                            CaissesCuistots.AjouterCase(new Coordonnees(i, j));
                            var node = (Node<Case>)Simulation.Tableau[i, j];
                            node.Value.SetZoneToObserve(CaissesClient);
                        }
                        else
                        {
                            ZoneRepas.AjouterCase(new Coordonnees(i, j));
                        }
                    }
                }
            }
        }
    }
}
