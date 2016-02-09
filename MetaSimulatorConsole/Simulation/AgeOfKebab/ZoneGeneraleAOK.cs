﻿using System;
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
            var link = new LinkCaseToZone();
            for (int i = 0; i < GameManager.Longueur; ++i)
            {
                for (int j = 0; j < GameManager.Largeur; ++j)
                {
                    if (i < GameManager.Longueur/2)
                    {
                        if (j < GameManager.Largeur/2)
                        {
                            var coor = new Coordonnees(i, j);
                            ZoneExterne.AjouterCase(coor);
                            link.LinkObject(coor, ZoneExterne, Simulation.Tableau);
                        }
                        else
                        {
                            var coor = new Coordonnees(i, j);
                            CaissesClient.AjouterCase(coor);
                            link.LinkObject(coor, CaissesClient, Simulation.Tableau);

                        }
                    }
                    else
                    {
                        if (j < GameManager.Longueur/2)
                        {
                            var coor = new Coordonnees(i, j);
                            CaissesCuistots.AjouterCase(coor);
                            link.LinkObject(coor, CaissesCuistots, Simulation.Tableau);
                        }
                        else
                        {
                            var coor = new Coordonnees(i, j);
                            ZoneRepas.AjouterCase(coor);
                            link.LinkObject(coor, ZoneRepas, Simulation.Tableau);
                        }
                    }
                }
            }
        }
    }
}