using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using MetaSimulatorConsole.Dijkstra;

namespace MetaSimulatorConsole.Simulation
{
    abstract class ChercherCheminTemplate
    {
        protected abstract List<ObjetAbstrait> RecupererListeObjets(ZoneFinale zone);

        private List<ArrayList> RecupererChemins(Grille grille,Coordonnees casedepart, List<ObjetAbstrait> objets)
        {
            if(grille == null) throw new NullReferenceException("La grille renseignée est nulle !");
            var list = new List<ArrayList>();
            foreach (var obj in objets)
            {
                var chemin = grille.Route(casedepart, obj.Case);
                grille.ReinitialiserMinDistances();
                list.Add(chemin);
            }
            return list; 
            return objets.Select(obj => grille.Route(casedepart, obj.Case)).ToList();

        }

        private int ChoisirLeCheminLePlusCourt(List<ArrayList> chemins)
        {
            int nbCases = int.MaxValue;
            ArrayList list = new ArrayList();
            if(chemins.Count == 0) Console.WriteLine("La liste de chemins est vide !");
            int item = 0,i = 0;
            foreach (var chemin in chemins)
            {

                if (chemin.Count < nbCases) // On prend le chemin le plus court
                {
                    nbCases = chemin.Count;
                    list = chemin;
                    item = i;
                }
                ++i;

            }
//            return list;
            return item;
        }
        public Coordonnees CaseSuivante(PersonnageMobilisable perso)
        {
            if(perso == null) 
                throw new NullReferenceException("Impossible de chercher le chemin pour un personnage null !");
            if(perso.ZoneActuelle == null)
                throw new NullReferenceException("Le personnage n'est assigné à aucune zone !");
            var objets = RecupererListeObjets(perso.ZoneActuelle);
            if (objets.Count == 0) return null;
            var chemins = RecupererChemins(perso.ZoneActuelle.Simulation.Tableau, perso.Case,objets);
            var item = ChoisirLeCheminLePlusCourt(chemins);
            if (chemins[item].Count == 0) return null;
            Node<Case> node = chemins[item][0] as Node<Case>;
            if(node == null) throw new InvalidCastException("La node n'est pas au bon format ! (Node<Case>)");
            if (TestEgaliteObjet(objets[item], node.Coor)) return null;
            Console.WriteLine("Case Obtenue: " + node.Coor);
            return node.Coor;
        }

        protected virtual bool TestEgaliteObjet(ObjetAbstrait obj, Coordonnees coor)
        {
            return false;
//            if (coor.Equals(obj.Case)) return true;
//            return false;
        }
    }

    class ChercherCheminVersGatherPoint : ChercherCheminTemplate
    {
        protected override List<ObjetAbstrait> RecupererListeObjets(ZoneFinale zone)
        {
            if(zone == null) throw new NullReferenceException("La zone de travail est nulle !");
            var list = new List<ObjetAbstrait>();
            foreach (var obj in zone.Objets)
            {
                if (obj is GatherPoint)
                {
//                    Console.WriteLine("Objet pt de rassemblement trouvé: " + obj);
                    list.Add(obj);
                }
            }
            return list;
//            return zone.Objets.OfType<GatherPoint>().Cast<ObjetAbstrait>().ToList();
        }

        protected override bool TestEgaliteObjet(ObjetAbstrait obj, Coordonnees coor)
        {
            if (coor.Equals(obj.Case)) return true;
            return false;
        }
    }
}
