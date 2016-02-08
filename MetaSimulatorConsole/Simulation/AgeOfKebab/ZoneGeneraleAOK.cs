using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    public class ZoneGeneraleAOK : ZoneComposite
    {
        public ZoneGeneraleAOK() : base("Zone Générale Age Of kebab", null) { }
        public ZoneGeneraleAOK(Game simu) 
            : base("Zone Générale Age Of kebab",simu)
        {
            if (Simulation == null)
            {
                Console.WriteLine("La simulation doit être lancée (instanciée) avant de pouvoir construire les zones");
                return;
            }
            var ZoneInterne = new ZoneComposite("Zone Interne (Kebab)", Simulation);
            var ZoneExterne = new ZoneFinale("Zone Externe (Dehors)", Simulation);
            this.AjouterZone(ZoneInterne);
            this.AjouterZone(ZoneExterne);
            var CaissesClient = new ZoneFinale("Zone Finale: Caisses client (Files d'attente)", Simulation);
            var CaissesCuistots = new ZoneFinale("Zone Finale: Caisses cuistots (côté serveur)", Simulation);
            var ZoneRepas = new ZoneFinale("Zone Finale: Repas pour les clients (chaises et tables)", Simulation);
            ZoneInterne.AjouterZone(CaissesClient);
            ZoneInterne.AjouterZone(CaissesCuistots);
            ZoneInterne.AjouterZone(ZoneRepas);            
        }

        //protected void ConstruireZones
    }
}
