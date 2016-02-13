using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MetaSimulatorConsole.Dijkstra;
using MetaSimulatorConsole.Simulation.AgeOfKebab;

namespace MetaSimulatorConsole.Simulation
{
    public interface IPersonnageAMobiliser
    {
        void Mobiliser();
    }

    public abstract class QuartierGeneralObserve :  IObservateurAbstrait
    {
        protected QuartierGeneralObserve() { }
        private List<IPersonnageAMobiliser> PersonnagesMobilises = new List<IPersonnageAMobiliser>();

        public void AttacherPersonnage(IPersonnageAMobiliser observer)
        {
            PersonnagesMobilises.Add(observer);
        }

        public void DeAttacherPersonnage(IPersonnageAMobiliser observer)
        {
            PersonnagesMobilises.Remove(observer);
        }

        public void DeAttacherTousLesPersonnagesMobilises()
        {
            PersonnagesMobilises.Clear();
        }


        public void MobiliserPersonnages()
        {
            foreach (IPersonnageAMobiliser o in PersonnagesMobilises)
            {
                o.Mobiliser();
            }
        }

        public List<IPersonnageAMobiliser> ObtenirPersonnageMobilises()
        {
            return PersonnagesMobilises;
        }

        public abstract void Update();
    }

    [XmlInclude(typeof(QuartierGeneralAOK))]
    public abstract class QuartierGeneralAbstrait : QuartierGeneralObserve
    {
        // Songer aussi à placer le module de statistique à l'intérieur
        private readonly  Game Simulation;
        protected ZoneGenerale ZonePrincipale;
       
        public int PersonnagesInseres;
        public int PersonnagesToInsert { get; set; }
        public override void Update()
        {
            DeAttacherTousLesPersonnagesMobilises();
            if(Simulation != null) ZonePrincipale = Simulation.ZoneGenerale;
            ChargerPersonnagesAMobiliser();
        }

        protected abstract PersonnageMobilisable PersonnageToInsertAt(Coordonnees coor);
        protected QuartierGeneralAbstrait(Game simu)
        {
            PersonnagesToInsert = 5;
            Simulation = simu;
            Update(); // Pour mettre à jour la ZoneGénérale
        }

        protected void ChargerPersonnagesAMobiliser()
        {
            if (ZonePrincipale == null) return;
            var list = ZonePrincipale.ObtenirPersonnages();
            foreach (var perso in list)
            {
                var observer = perso as IPersonnageAMobiliser;
                if (observer != null)
                {
                    AttacherPersonnage(observer);
                }
            }
        }

        public void GererUnTour()
        {
            // Si une simulation continue est en cours, impossible !
            if (Simulation.Running)
            {
                Console.WriteLine("GererUnTour: Impossible de lancer un tour manuellement, car la simulation est en cours !");
                return;
            }
            else Simulation.Started = true;
            MobiliserPersonnages();
            InsererPersonnagesRestants();
        }

        protected abstract void InsererPersonnagesRestants();

    }

    public class QuartierGeneralAOK : QuartierGeneralAbstrait
    {
        public QuartierGeneralAOK(Game simu) : base(simu){}


        protected override PersonnageMobilisable PersonnageToInsertAt(Coordonnees coor)
        {
            return new Client("Client " + (PersonnagesInseres+1),coor);
        }

        protected override void InsererPersonnagesRestants()
        {
            if (PersonnagesToInsert <= 0)
            {
                Console.WriteLine("Il ne reste plus de personnages à insérer");
                return;
            }
            //Insertion Personnages: Trouver la zone et le lieu où se trouvent les SpawnPoints 
            foreach (var zone in ZonePrincipale.ObtenirZonesFinales())
            {
                foreach (var objet in zone.Objets) // Pour chaque objet de cette zone finale
                {
                    if (objet is SpawnPoint) // Si c'est un pt d'apparition
                    {
//                        Console.WriteLine("Objet SpawnPoint trouvé " + objet + " dans la zone " + zone);
                        var casesAdjacentes = Coordonnees.ObtenirCasesAdjacentes(objet.Case);
                        foreach (var coor in casesAdjacentes) // Pour chaque case adjacente à ce pt d'accès
                        {
                            if (zone.ContientCoordonnees(coor)) // De cette zone !
                            {
//                                Console.WriteLine("Case adjacente  trouvée: " + coor + " dans la " + zone);
                                var node = (Node<Case>)zone.Simulation.Tableau[coor.X, coor.Y];
                                CaseAgeOfKebab c = node.Value as CaseAgeOfKebab;
                                if (c == null)
                                    throw new InvalidCastException("La case n'est pas au format CaseAgeOfKebab");
                                if (c.Walkable) // Si un personnage peut aller dans cette case, alors ..
                                {
                                    if (PersonnagesToInsert > 0)
                                    {
                                        var perso = PersonnageToInsertAt(coor);
                                        if (zone.AjouterPersonnage(perso))
                                        {
                                            Console.WriteLine("Insertion du personnage " + perso);
                                            perso.SetZones(ZonePrincipale, zone);
                                            ++PersonnagesInseres;
                                            --PersonnagesToInsert;
                                            AttacherPersonnage((Client) perso);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Cette Case n'est pas Walkable !");
                                }
                            }
                        }
                    }
                }
            }
        }

    }

}
