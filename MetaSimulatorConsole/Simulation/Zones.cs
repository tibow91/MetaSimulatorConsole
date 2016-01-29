using MetaSimulatorConsole.Simulation;
using MetaSimulatorConsole.Simulation.Honeyland;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    abstract class ZoneAbstraite
    {
        protected string nom;
        public Game Simulation; 

        protected ZoneAbstraite(string unNom,Game simulation)
        {
            nom = unNom;
            Simulation = simulation;
        }

        public abstract void AjouterZone(ZoneAbstraite c);
        //public abstract void AfficherZones()
        public abstract List<PersonnageAbstract> ObtenirPersonnages();
        public abstract List<ObjetAbstrait> ObtenirObjets();
        public abstract bool AjouterPersonnage(PersonnageAbstract personnage);
        public abstract bool AjouterObjet(ObjetAbstrait objet);

    }
    //enum ZoneAgeOfKebab { Repas, Caisses, CaissesStaff, Externe };

    class ZoneComposite : ZoneAbstraite
    {
        protected readonly List<ZoneAbstraite> Zones = new List<ZoneAbstraite>();

        public ZoneComposite(string name,Game simu)
            : base(name,simu)
        {
        }

        public override void AjouterZone(ZoneAbstraite zone)
        {
            if (zone == null) return;
            if (Simulation != zone.Simulation)
            {
                Console.WriteLine("On ne peut pas ajouter de zones associées à une autre simulation !");
                return;
            }
            Zones.Add(zone);
        }


        public override List<PersonnageAbstract> ObtenirPersonnages()
        {
            var liste = new List<PersonnageAbstract>();
            foreach (ZoneAbstraite zone in Zones)
            {
                foreach(var personnage in zone.ObtenirPersonnages())
                    liste.Add(personnage);
            }
            return liste;
        }

        public override List<ObjetAbstrait> ObtenirObjets()
        {
            var liste = new List<ObjetAbstrait>();
            foreach (ZoneAbstraite zone in Zones)
            {
                foreach (var objet in zone.ObtenirObjets())
                    liste.Add(objet);
            }
            return liste;
        }

        public override bool AjouterPersonnage(PersonnageAbstract personnage)
        {
            Console.WriteLine("Ajouter de Personnage impossible sur les zones composite");
            return false;
        }

        public override bool AjouterObjet(ObjetAbstrait objet)
        {
            Console.WriteLine("Ajouter d'Objet impossible sur les zones composite");
            return false;
        }
    }

    class ZoneFinale : ZoneAbstraite
    {
        protected List<PersonnageAbstract> Personnages = new List<PersonnageAbstract>();
        protected List<ObjetAbstrait> Objets = new List<ObjetAbstrait>();

        public ZoneFinale(string name,Game simu)
            : base(name,simu)
        {
        }
        public override void AjouterZone(ZoneAbstraite c)
        {
            Console.WriteLine("Une zone finale ne peut pas contenir d'autres zones");
        }

        public override List<PersonnageAbstract> ObtenirPersonnages()
        {
            return Personnages;
        }

        public override List<ObjetAbstrait> ObtenirObjets()
        {
            return Objets;
        }

        public override bool AjouterPersonnage(PersonnageAbstract personnage)
        {
            if (personnage == null) return false;
            if (Simulation.NomDuJeu != personnage.Simulation)
            {
                Console.WriteLine("On ne peut pas ajouter de personnages associés à une autre simulation !");
                return false;
            }
            Personnages.Add(personnage);
            return true;
        }

        public override bool AjouterObjet(ObjetAbstrait objet)
        {
            if (objet == null) return false;
            if (Simulation.NomDuJeu != objet.TypeSimulation)
            {
                Console.WriteLine("On ne peut pas ajouter de personnages associés à une autre simulation !");
                return false;
            }
            Objets.Add(objet);
            return true;
        }
    }

    class ZoneMaker
    {
        protected ZoneGeneraleAOK ZoneGeneraleAgeOfKebab; // AgeOfKebab
        protected ZoneGeneraleCDG ZoneGeneraleCDGSimulator; // CDGSimulator
        protected ZoneGeneraleHoneyland ZoneGeneraleHoneyLand; // Honeyland

        public ZoneGeneraleAOK ConstruireZonesAgeOfKebab(Game simulation)
        {
            if (ZoneGeneraleAgeOfKebab == null)            
                ZoneGeneraleAgeOfKebab = new ZoneGeneraleAOK(simulation);
            return ZoneGeneraleAgeOfKebab;

        }

        public ZoneGeneraleCDG ConstruireZonesCDGSimulator(Game simulation) // A faire
        {
            if (ZoneGeneraleCDGSimulator == null)
                ZoneGeneraleCDGSimulator = new ZoneGeneraleCDG(simulation);
            return ZoneGeneraleCDGSimulator;
        }

        public ZoneGeneraleHoneyland ConstruireZonesHoneyland(Game simulation) // A faire
        {
            if (ZoneGeneraleHoneyLand == null)
                ZoneGeneraleHoneyLand = new ZoneGeneraleHoneyland(simulation);
            return ZoneGeneraleHoneyLand;
        }

    }
}
