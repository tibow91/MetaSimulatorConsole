using MetaSimulatorConsole.Simulation;
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

        public ZoneAbstraite(string unNom)
        {
            nom = unNom;
        }

        public abstract void AjouterZone(ZoneAbstraite c);
        //public abstract void AfficherZones()
        public abstract List<PersonnageAbstract> ObtenirPersonnages();
        public abstract List<ObjetAbstract> ObtenirObjets();

    }


    class ZoneComposite : ZoneAbstraite
    {
        private readonly List<ZoneAbstraite> Zones = new List<ZoneAbstraite>();

        public ZoneComposite(string name)
            : base(name)
        {
        }

        public override void AjouterZone(ZoneAbstraite component)
        {
            Zones.Add(component);
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

        public override List<ObjetAbstract> ObtenirObjets()
        {
            var liste = new List<ObjetAbstract>();
            foreach (ZoneAbstraite zone in Zones)
            {
                foreach (var objet in zone.ObtenirObjets())
                    liste.Add(objet);
            }
            return liste;
        }
    }

    class ZoneFinale : ZoneAbstraite
    {
        private List<PersonnageAbstract> Personnages = new List<PersonnageAbstract>();
        private List<ObjetAbstract> Objets = new List<ObjetAbstract>();

        public ZoneFinale(string name)
            : base(name)
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

        public override List<ObjetAbstract> ObtenirObjets()
        {
            return Objets;
        }
    }
}
