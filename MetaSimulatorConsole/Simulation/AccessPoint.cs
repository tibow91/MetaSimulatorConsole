using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetaSimulatorConsole.Simulation
{
    public class AccessPoint : ObjetAbstrait
    {
        [XmlIgnore]
        public List<ZoneFinale> ZoneAnnexes = new List<ZoneFinale>();

        public AccessPoint() : base("AccessPoint générique",new TextureWoodPlatformVertical()) {}
        public AccessPoint(string nom, EGame nomdujeu, ZoneFinale zone)
            : base(nom, nomdujeu, new TextureWoodPlatformVertical())
        {
            AddZoneAnnexe(zone);
        }

        public override bool EstValide()
        {
            if (!base.EstValide()) return false;
            if (ZoneAnnexes.Count == 0)
            {
                Console.WriteLine("Ce Point D'accès " + this + " n'a pas de zones annexes !");
                return false;
            }
            return true;
        }

        public void AddZoneAnnexe(ZoneFinale zone)
        {
            if(zone != null) ZoneAnnexes.Add(zone);
        }

        protected static bool PlacerPoint( ZoneFinale zonedepart,ZoneFinale zonearrivee, bool doReverseToo,Coordonnees coorReverse)
        {
            if (zonedepart == null || zonearrivee == null)
            {
                Console.Write("PlacerPoint: La zone de départ/arrivée est nulle ! ");
                return false;
            }

            string accesspointname = "Pt d'accès de " + zonedepart.nom + " vers " + zonearrivee.nom;

            var accesspoint = new AccessPoint(accesspointname, zonedepart.Simulation.NomDuJeu, zonearrivee);
      
            if (doReverseToo)
            {
                var list = new AccessPointFinder().Find(zonedepart, zonearrivee);
                if (list.Count == 0)
                {
                    Console.WriteLine("Aucun point d'accès ne peut être placé entre la zone " + zonedepart + " et " +
                                      zonearrivee);
                }
                else
                {
                    foreach (var coor in list) // Place l'objet dans la première coordonnée valide
                    {
                        accesspoint.SetCoordonnees(coor);
                        if (zonedepart.PeutPlacerObjet(accesspoint))
                        {
                            if (!zonedepart.AjouterObjet(accesspoint)) // Pas normal donc break;
                                Console.WriteLine("PlacerPoint: Erreur anormale rencontrée au moment de l'ajout du pt d'accès" +
                                                  accesspoint + " à la zone " + zonedepart);
                            else
                            {
                                foreach (var adj in new AccessPointFinder().FindAjacentOfZone(coor, zonearrivee))
                                {
                                    if (PlacerPoint(zonearrivee, zonedepart, false, adj)) break;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                if (coorReverse == null)
                {
                    Console.WriteLine("Coordonnées du point d'accès reverse est null !");
                    return false;
                }
                accesspoint.SetCoordonnees(coorReverse);
                if (!zonedepart.AjouterObjet(accesspoint))
                {

                    foreach (var obj in zonedepart.Objets) // s'il existe dejà un pt d'acces a cet endroit, rajouter la zone d'arrivée parmi les zones annexes
                    {
                        if (obj is AccessPoint)
                        {
                            if (obj.Case.Equals(coorReverse))
                            {
                                var objAccess = (AccessPoint) obj;
                                objAccess.AddZoneAnnexe(zonearrivee);
                                return true;
                            }
                        }
                    }
                    Console.WriteLine("PlacerPoint: Erreur rencontrée au moment de l'ajout du pt d'accès" +
                                      accesspoint + " à la zone " + zonedepart);
                    return false;
                }
            }
            return true;

        }

        public static void PlacerPoint(ZoneFinale zonedepart, ZoneFinale zonearrivee)
        {
            PlacerPoint(zonedepart, zonearrivee, true,null);
        }
    }

    
    public class AccessPointFinder
    {
        public List<Coordonnees> Find(ZoneFinale zonedepart, ZoneFinale zonearrivee)
        {
            var list = new List<Coordonnees>();
            if (zonedepart == null)
            {
                Console.WriteLine("AccessPointFinder: zonedepart is null");
                return list;
            }
            if (zonearrivee == null)
            {
                Console.WriteLine("AccessPointFinder: zonearrivee is null");
                return list;
            }

            list.AddRange(from coor1 in zonedepart.Cases from coor2 in zonearrivee.Cases where coor1.EstAdjacent(coor2) select coor1);
            return list;
        }

        public List<Coordonnees> FindAjacentOfZone(Coordonnees coor, ZoneFinale zone)
        {
            List<Coordonnees> list = new List<Coordonnees>();
            if (zone == null) return list;
            if (coor == null || !coor.EstValide()) return list;

            foreach (var adj in Coordonnees.ObtenirCasesAdjacentes(coor))
            {
                if(zone.ContientCoordonnees(adj)) list.Add(adj); 
            }
            return list;
        }
    }


}
