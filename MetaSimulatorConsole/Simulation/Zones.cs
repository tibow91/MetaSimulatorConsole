﻿using MetaSimulatorConsole.Dijkstra;
using MetaSimulatorConsole.Simulation;
using MetaSimulatorConsole.Simulation.Honeyland;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetaSimulatorConsole
{
    public class Coordonnees : IEquatable<Coordonnees>
    {
        [XmlAttribute]
        public int X = -1;
        [XmlAttribute]
        public int Y = -1;

        public Coordonnees() { }
        public Coordonnees(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool EstValide()
        {
            if (X < 0 || Y < 0) return false;
            if (X >= GameManager.Longueur) return false;
            if (Y >= GameManager.Largeur) return false;
            return true; 
        }

        public bool EstAdjacent(Coordonnees coor2)
        {
            if (!EstValide()) return false;
            if (!coor2.EstValide()) return false;
            return ObtenirCasesAdjacentes(this).Any(coor2.Equals);
        }

        public static List<Coordonnees> ObtenirCasesAdjacentes(Coordonnees coor)
        {
            List<Coordonnees> list = new List<Coordonnees>();
            if (coor == null || !coor.EstValide()) return list;
            var coorAdjacent = new Coordonnees(coor.X + 1, coor.Y);
            if (coorAdjacent.EstValide()) list.Add(coorAdjacent);
            coorAdjacent = new Coordonnees(coor.X - 1, coor.Y);
            if (coorAdjacent.EstValide()) list.Add(coorAdjacent);
            coorAdjacent = new Coordonnees(coor.X, coor.Y + 1);
            if (coorAdjacent.EstValide()) list.Add(coorAdjacent);
            coorAdjacent = new Coordonnees(coor.X, coor.Y - 1);
            if (coorAdjacent.EstValide()) list.Add(coorAdjacent);
            return list;
        }

        public bool Equals(Coordonnees other)
        {
            if (!EstValide()) return false;
            if (!other.EstValide()) return false;
            if ((X == other.X) && (Y == other.Y)) return true;
            return false;
        }

        public override string ToString()
        {
            return "(" + X + "," + Y + ")";
        }
        

    }
    [XmlInclude(typeof(ZoneGeneraleAOK))]
    [XmlInclude(typeof(ZoneGenerale))]
    [XmlInclude(typeof(ZoneFinale))]
    [XmlInclude(typeof(ZoneComposite))]
    public abstract class ZoneAbstraite : SujetObserveAbstrait
    {
        [XmlAttribute]
        public string nom;
        [XmlIgnore]
        public Game Simulation;

        public ZoneAbstraite()
        {
            nom = "Zone abstraite générique";
        }
        protected ZoneAbstraite(string unNom,Game simulation)
        {
            nom = unNom;
            Simulation = simulation;
        }
        public abstract void AttacherAuJeu(Game simulation);
        public abstract void LierZonesAuxPersonnages(ZoneGenerale zonegenerale);
        
        public abstract List<ZoneFinale> ObtenirZonesFinales();
        public abstract void AjouterZone(ZoneAbstraite c);
        //public abstract void AfficherZones()
        public abstract List<ObjetAbstrait> ObtenirObjets();
        public abstract bool AjouterPersonnage(PersonnageAbstract personnage);
        public abstract List<PersonnageAbstract> ObtenirPersonnages();
        public abstract bool AjouterObjet(ObjetAbstrait objet);
        public abstract void EnleverObjet(ObjetAbstrait objet);
        public abstract bool AjouterCase(Coordonnees coor);
        public void RelierAuTableauDeJeu()
        {
            DelierDuTableauDeJeu();
            LierAuTableauDeJeu();
        }
        public abstract void LierAuTableauDeJeu();
        public abstract void DelierDuTableauDeJeu();
        public bool ContientMemeInstance(ZoneAbstraite zone)
        {
            if(Simulation == null) return false;
            if(zone.Simulation == null) return false;
            if(Simulation == zone.Simulation) return true;
            return false;
        }
        public abstract bool EstVide();
        public abstract bool ContientDesZonesFinales();
        public abstract bool EstValide();
        public abstract bool EstCompatible(ZoneAbstraite zone);
        public abstract bool PeutPlacerObjet(ObjetAbstrait objet);
        public override string ToString()
        {
 	         return "Zone " + nom;
        }
        public abstract bool ContientCoordonnees(Coordonnees coor);

    }
    
    public class ZoneComposite : ZoneAbstraite
    {
        public readonly List<ZoneAbstraite> Zones = new List<ZoneAbstraite>();

        public ZoneComposite() : base("Zone Composite générique", null) { }
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
            if(!ContientMemeInstance(zone)) {
                Console.WriteLine("AjouterZone: Les zones " + this + " et " + zone + " ne contiennent pas la meme instance! ");
                return;
            }
            // si la zone n'est pas valide (définir les criteres de validité dans une méthode  abstraite EstValide)
                // l'ajout n'est pas validé
            // sinon si la zone est composite                 

            // Sinon l'ajout est validé si la zone à ajouter est compatible avec celle ci (définir les criteres de validité dans une méthode  abstraite EstCompatible)
            if (!EstCompatible(zone))
            {
                Console.WriteLine("AjouterZone: Les zones " + this + " et " + zone + " ne sont pas compatibles");
                return;
            }
            Zones.Add(zone);
        }


        public override List<ZoneFinale> ObtenirZonesFinales()
        {
            var list = new List<ZoneFinale>();
            foreach (var zone in Zones)
            {
                var item = zone as ZoneFinale;
                if (item != null) 
                    list.Add(item);
                else
                    list.AddRange(zone.ObtenirZonesFinales());
            }
            return list;
        }

        public override List<PersonnageAbstract> ObtenirPersonnages()
        {
            return Zones.SelectMany(zone => zone.ObtenirPersonnages()).ToList();
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
            Console.WriteLine("Ajout de Personnage impossible sur les zones composite");
            return false;
        }

        public override bool AjouterObjet(ObjetAbstrait objet)
        {
            Console.WriteLine("Ajout d'Objet impossible sur les zones composite");
            return false;
        }

        public override void EnleverObjet(ObjetAbstrait objet)
        {
            Console.WriteLine("Suppression d'Objet impossible sur les zones composite");
            return;
        }
        public override bool AjouterCase(Coordonnees coor)
        {
            Console.WriteLine("Ajout de case impossible sur les zones composite");
            return false;
        }

        public override bool EstValide()
        {
            if (String.IsNullOrEmpty(nom))
            {
                Console.WriteLine("EstValide: Cette zone " + this + " n'a pas de nom !");
                return false;
            }
            if (this.EstVide()) return false;
            foreach(var zone in Zones)
            {
                if (!zone.EstValide())
                {
                    Console.WriteLine("La zone" + zone + " n'est pas valide");
                    return false;
                }
            }
            return true;
        }

        public override bool EstCompatible(ZoneAbstraite zone)
        {
            if (zone == null) return false;
            if(!ContientMemeInstance(zone)){
                Console.WriteLine("Les zones " + this + " et " + zone + " ne sont pas liées à la même simulation !");
                return false;
            }
            if (zone is ZoneComposite)
            {
                foreach(var z1 in Zones)
                {
                    ZoneComposite zonecast = (ZoneComposite)zone;
                    foreach(var z2 in zonecast.Zones)
                    {
                        if(z1.Equals(z2))
                        {
                            Console.WriteLine("Compatibilité: Les zones " + z1 + " et " + z2 + " sont les mêmes !");
                            return false;
                        }
                        if (!z1.EstCompatible(z2))
                        {
                            Console.WriteLine("Compatibilité: La zone " + z1 + " n'est pas compatible avec la zone " + z2);
                            return false;
                        }
                    }
                }
                return true;
            }
            if(zone is ZoneFinale)
            {
                ZoneFinale zonecast = (ZoneFinale)zone;
                foreach (var z1 in Zones)
                {
                    if (!z1.EstCompatible(zonecast))
                    {
                        Console.WriteLine("Compatibilité: La zone " + z1 + " n'est pas compatible avec la zone " + zonecast);
                        return false;
                    }
                }
                return true;
            }
            Console.WriteLine("Compatibilité: La zone " + zone + " est de type non reconnu");
            return false;
                
        }

        public override bool EstVide()
        {
             if (Zones.Count > 0)  return false;            
             return true;
        }

        public override bool ContientDesZonesFinales()
        {
            foreach(var zone in Zones)
            {
                if(zone is ZoneComposite)
                {
                    if (zone.ContientDesZonesFinales()) return true;
                }
                else if(zone is ZoneFinale)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("ContientDesZonesFinales: La zone " + zone + " est de type non déterminé ");
                }
            }
            return false;
        }

        public override bool ContientCoordonnees(Coordonnees coor)
        {
            if (!coor.EstValide())
            {
                Console.WriteLine("ContientCoordonnees: les coordonnées " + coor + " ne sont pas valides");
                return false;
            }
            foreach(var zone in Zones)
            {
                if (zone.ContientCoordonnees(coor)) return true;
            }
            return false;
        }

        public override bool PeutPlacerObjet(ObjetAbstrait objet)
        {
            Console.WriteLine("Impossible de placer un objet dans une Zone Composite !");
            return false;
        }



        public override void LierAuTableauDeJeu()
        {
            foreach (var zone in Zones)
            {
                zone.LierAuTableauDeJeu();
            }
        }

        public override void DelierDuTableauDeJeu()
        {
            foreach(var zone in Zones)
            {
                zone.DelierDuTableauDeJeu();
            }
        }

        public override void AttacherAuJeu(Game simulation)
        {
            Simulation = simulation;
            foreach(var zone in Zones)
            {
                zone.AttacherAuJeu(simulation);
            }
        }

        public override void LierZonesAuxPersonnages(ZoneGenerale zonegenerale)
        {
            foreach(var zone in Zones)
            {
                zone.LierZonesAuxPersonnages(zonegenerale);
            }
        }
    }


    public class ZoneFinale :  ZoneAbstraite,IEquatable<ZoneFinale>
    {
        public Texture Texture;
        public List<PersonnageAbstract> Personnages = new List<PersonnageAbstract>();
        public List<ObjetAbstrait> Objets = new List<ObjetAbstrait>();
        public List<Coordonnees> Cases = new List<Coordonnees>();
        public ZoneFinale() : base("Zone Finale générique",null) { }
        public ZoneFinale(string name,Texture texture,Game simu)
            : base(name,simu)
        {
            Texture = texture;
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
            if (!personnage.Case.EstValide())
            {
                Console.WriteLine("AjouterPersonnage: les coordonnées du personnage " + personnage + "ne sont pas valides");
                return false;
            }
            if (!this.ContientCoordonnees(personnage.Case))
            {
                Console.WriteLine("AjouterPersonnage: Impossible d'ajouter personnage " + personnage + " car il ne se situe pas géographiquement dans cette zone " + this);
                return false;
            }
            foreach(var perso in Personnages)
            {
                if (perso.Equals(personnage))
                {
                    Console.WriteLine("AjouterPersonnage: Les personnages " + perso + " et " + personnage + " se situent aux mêmes coordonnées !");
                    return false;
                }
            }           
            Personnages.Add(personnage);
            new LinkCaseToPersonnage().LinkObject(personnage.Case, personnage, Simulation.Tableau);
            return true;
        }

        public override bool AjouterObjet(ObjetAbstrait objet)
        {
            if (!PeutPlacerObjet(objet))
            {
                Console.WriteLine("AjouterObjet: Placement de l'objet " + objet +" impossible");
                return false;
            }
            Objets.Add(objet);
            new LinkCaseToObject().LinkObject(objet.Case, objet, Simulation.Tableau);
            return true;
        }

        public override void EnleverObjet(ObjetAbstrait objet)
        {
            if (objet == null) return;
            foreach (var obj in Objets)
            {
                if (obj.Equals(objet))
                {
                    foreach (var observer in objet.GetObservers())
                    {
                        if (observer is Case)
                        {
                            var cell = (Case)observer;
                            cell.SetObjectToObserve(null);
                        }
                        DeAttach(observer);
                    }
                    Objets.Remove(objet);
                    return;
                }
            }
        }

        public override bool AjouterCase(Coordonnees coor)
        {
            if (coor == null) return false;
            if (!coor.EstValide())
            {
                Console.WriteLine("AjouterCase: les coordonnées de la case " + coor + " ne sont pas valides");
                return false;
            }
            if (this.ContientCoordonnees(coor))
            {
                Console.WriteLine("AjouterCase: La zone " + this + " contient déjà la case " + coor);
                return false;
            }
            Cases.Add(coor);
            return true;
        }

        public override bool EstVide()
        {
            if (Cases.Count > 0) return false;
            return true;
        }

        public override bool ContientDesZonesFinales()
        {
            return true;
        }

        public override bool EstValide()
        {
            if (EstVide())
            {
                Console.WriteLine("Validité: Une zone finale ne peut être vide !");
                return false;
            }
            // pour chacune de ses cases
            foreach(var coor in Cases)
            {
                if (!coor.EstValide()) // si une n'est pas valide, alors c'est pas valide
                {
                    Console.WriteLine("EstValide: Zone " + this + " - Case " + coor + " non valide");
                    return false;
                }
            }

            // Si chaque case a une autre case adjacente dans la zone alors c'est bon
            foreach (var coor1 in Cases)
            {
                bool found = false;

                foreach (var coor2 in Cases)
                {
                    if (!coor1.Equals(coor2))
                    {
                        if (coor1.EstAdjacent(coor2))
                        {
                            found = true;
                            break;
                        }
                    }
                }
                if (!found)
                {
                    Console.WriteLine("Zone " + this + ": Non valide car la case " + coor1 + " n'est adjacente à aucune autre case");
                    return false;
                }
            }
            // pour chacun de ses objets
            foreach(var objet in Objets)
            {
                if (!objet.EstValide())  // si n'est pas valide, alors c'est pas valide
                {
                    Console.WriteLine("EstValide: Zone " + this + " - Objet " + objet + " non valide");
                    return false;
                }
                if (!ContientCoordonnees(objet.Case))
                {
                    Console.WriteLine("Erreur de placement " + objet + " n'est pas dans la zone " + this + " !!");
                    return false;
                }
            }

            // pour chacun de ses personnages

            foreach(var personnage in Personnages)
            {
                if(!personnage.EstValide())// si un n'est pas valide, alors c'est pas valide
                {
                    Console.WriteLine("EstValide: Zone " + this + " - Personnage " + personnage + " non valide");
                    return false;
                }
                if (!ContientCoordonnees(personnage.Case))
                {
                    Console.WriteLine("EstValide: Zone " + this + " - personnage " + personnage + " n'est pas ici !");
                    return false;
                }
            }
            return true;
        }

        public override bool EstCompatible(ZoneAbstraite zone)
        {
            if (zone == null) return false;
            if (!ContientMemeInstance(zone))
            {
                Console.WriteLine("Les zones " + this + " et " + zone + " ne sont pas liées à la même simulation !");
                return false;
            }
            if(zone is ZoneComposite)
            {
                if (!zone.EstCompatible(this)) return false;
                return true;
            }
            if(zone is ZoneFinale)
            {
                if (this.EstVide()) return true;
                if (zone.EstVide()) return true;
                ZoneFinale zoneCastee = (ZoneFinale)zone;

                // regarder si chacune des cases de cette zone est différente de celles de l'autre zone
                foreach(var coor1 in Cases)
                {
                    foreach (var coor2 in zoneCastee.Cases)
                    {
                        if(coor1.Equals(coor2))
                        {
                            Console.WriteLine("EstCompatible: Zone " + this + " - Case " + coor1 + " égale à Zone " + zoneCastee + " - Case " + coor2 );
                            return false;
                        }
                    }
                }

                // Sinon regarder si les personnages de chaque zone ont des coordonnées différentes des perso de l'autre zone

                foreach (var perso1 in Personnages)
                {
                    foreach (var perso2 in zoneCastee.Personnages)
                    {
                        if (perso1.Equals(perso2))
                        {
                            Console.WriteLine("Equals: Le personnage " + perso1 + " et le personnage " + perso2 +
                              " partagent les mêmes coordonnées !");
                            return false;
                        }
                    }
                }


                 // Sinon regarder si les objets de chaque zone ont des coordonnées différentes des objets de l'autre zone

                foreach (var objet1 in Objets)
                {
                    foreach (var objet2 in zoneCastee.Objets)
                    {
                        if (objet1.Equals(objet2))
                        {
                            Console.WriteLine("Equals: L'objet " + objet1 + " et l'objet " + objet2 +
                               " partagent les mêmes coordonnées ");
                            return false;
                        }
                        
                    }
                }

            }

            return true;
        }

        public override bool ContientCoordonnees(Coordonnees Coor)
        {
            foreach(var coor in Cases)
            {
                if (coor.Equals(Coor))
                {
                    return true;
                }
            }
            return false;
        }

        public override bool PeutPlacerObjet(ObjetAbstrait objet)
        {
            if (objet == null) return false;
            if (Simulation.NomDuJeu != objet.TypeSimulation)
            {
                Console.WriteLine("On ne peut pas ajouter de personnages associés à une autre simulation !");
                return false;
            }
            if (!objet.Case.EstValide())
            {
                Console.WriteLine("AjouterObjet: les coordonnées de l'objet " + objet + "ne sont pas valides");
                return false;
            }
            if (!this.ContientCoordonnees(objet.Case))
            {
                Console.WriteLine("AjouterObjet: Impossible d'ajouter l'objet " + objet + " car il ne se situe pas géographiquement dans cette zone " + this);
                return false;
            }
            foreach (var obj in Objets)
            {
                if (obj.Case.Equals(objet.Case))
                {
                    Console.WriteLine("AjouterObjet: Les objets " + obj + " et " + objet + " se situent aux mêmes coordonnées !");
                    return false;
                }
            }
            return true;
        }



        public bool Equals(ZoneFinale other)
        {
            if (other == null) return false;

            if (Simulation == other.Simulation) // Même Simulation
            {
                if (nom == other.nom) return true;
            }
            return false;
        }

        public override void LierAuTableauDeJeu()
        {
            var linkZone = new LinkCaseToZone();
            foreach (var coor in Cases)
                linkZone.LinkObject(coor, this, Simulation.Tableau);

            var linkObject = new LinkCaseToObject();
            foreach (var obj in Objets)
            {
                linkObject.LinkObject(obj.Case, obj, Simulation.Tableau);
            }

            var linkPersonnage = new LinkCaseToPersonnage();
            foreach (var perso in Personnages)
            {
                linkPersonnage.LinkObject(perso.Case, perso, Simulation.Tableau);
            }
        }

        public override void DelierDuTableauDeJeu()
        {
            var unlinkZone = new UnLinkCaseFromZone();
            foreach (var coor in Cases)
                unlinkZone.LinkObject(coor, this, Simulation.Tableau);

            var unlinkObject = new UnLinkCaseFromObject();
            foreach (var obj in Objets)
            {
                unlinkObject.LinkObject(obj.Case, obj, Simulation.Tableau);
            }

            var unlinkPersonnage = new UnLinkCaseFromPersonnage();
            foreach (var perso in Personnages)
            {
                unlinkPersonnage.LinkObject(perso.Case, perso, Simulation.Tableau);
            }
        }

        public override List<ZoneFinale> ObtenirZonesFinales()
        {
            return new List<ZoneFinale>(){ this};
        }

        public override void AttacherAuJeu(Game simulation)
        {
            Simulation = simulation;
        }

        public override void LierZonesAuxPersonnages(ZoneGenerale zonegenerale)
        {
            foreach(var personnage in Personnages)
            {
                if(personnage is PersonnageMobilisable)
                {
                    var perso = (PersonnageMobilisable)personnage;
                    perso.SetZones(zonegenerale, this);
                    perso.RechargerComportement();
                }
            }
        }
    }


    public abstract class ZoneGenerale : ZoneComposite
    {
        public ZoneGenerale() : base("Zone Générale générique", null) { }
        protected ZoneGenerale(string name,Game simu)
            : base(name, simu)
        {
            if (Simulation == null)
            {
                Console.WriteLine("La simulation doit être lancée (instanciée) avant de pouvoir construire les zones");
                return;
            }
            ConstruireZones();
            DistribuerZones();
            HierarchiserZones();
            PlacerObjets();
        }

        public void RattacherAccessPoints()
        {

            foreach (var zone in ObtenirZonesFinales())
            {
                var acpoints = new List<AccessPoint>();
                foreach (var obj in zone.ObtenirObjets())
                {
                    if (obj is AccessPoint)
                        acpoints.Add((AccessPoint)obj);   // On récupère les points d'accès de la première zone         
                }

                foreach (var zone2 in ObtenirZonesFinales())
                {
                    if (zone.Equals(zone2)) continue;
                    foreach (var obj in zone2.ObtenirObjets())
                    {
                        if (obj is AccessPoint) // Chaque point d'accès de la première zone est comparé à la seconde
                        {
                            foreach (var acpoint in acpoints)
                            {
                                if (acpoint.Case.EstAdjacent(obj.Case))
                                {
                                    acpoint.AddZoneAnnexe(zone2);
                                    var acpoint2 = (AccessPoint)obj;
                                    acpoint2.AddZoneAnnexe(zone);
                                }
                            }
                        }
                    }

                }
            }

        }

        protected abstract void ConstruireZones();
        protected abstract void HierarchiserZones();
        protected abstract void DistribuerZones();
        protected abstract void PlacerObjets();

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
