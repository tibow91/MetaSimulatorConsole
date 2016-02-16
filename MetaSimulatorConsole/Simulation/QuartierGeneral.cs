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
        void SetQG(QuartierGeneralAbstrait qg);
        void InformerObjectifAtteint();
        void InformerObjetRecupere();
        void InformerPersonnageMort();

    }

    public abstract class QuartierGeneralObserve :  SujetObserveAbstrait,IObservateurAbstrait
    {       
        protected QuartierGeneralObserve() { }
        private List<PersonnageMobilisable> PersonnagesMobilises = new List<PersonnageMobilisable>();

        public void AttacherPersonnage(PersonnageMobilisable observer)
        {
            PersonnagesMobilises.Add(observer);
        }

        public void DeAttacherPersonnage(PersonnageMobilisable observer)
        {
            PersonnagesMobilises.Remove(observer);
        }

        public void DeAttacherTousLesPersonnagesMobilises()
        {
            foreach(var perso in PersonnagesMobilises)            
                perso.SetQG(null);            
            PersonnagesMobilises.Clear();
        }


        public void MobiliserPersonnages()
        {
            for (int i = 0; i < PersonnagesMobilises.Count; ++i )
            {
                var perso = PersonnagesMobilises[i];
                if (perso == null) throw new NullReferenceException("Ne peut pas mobiliser un personnage non instancié");
                perso.Mobiliser();
            }
                //foreach (var o in PersonnagesMobilises)
                //{
                //    o.Mobiliser();
                //}
        }

        public List<PersonnageMobilisable> ObtenirPersonnageMobilises()
        {
            return PersonnagesMobilises;
        }

        public abstract void Update();
    }

    [XmlInclude(typeof(QuartierGeneralAOK))]
    public abstract class QuartierGeneralAbstrait : QuartierGeneralObserve
    {
        // Songer aussi à placer le module de statistique à l'intérieur
        private Game _simulation { get; set; }
        [XmlIgnore]
        public  Game Simulation
        {
            get { return _simulation;  }
            set { 
                    _simulation = value;
                     _simulation.Attach(this);
                }
        }
        protected ZoneGenerale ZonePrincipale;
        private int _tour;
        [XmlAttribute]
        public int Tour
        {
            get { return _tour; }
            set
            {
                _tour = value;
                if (Simulation != null) Simulation.UpdateObservers(); // pour mettre à jour les stats de la fenêtre
            }
        }
        [XmlAttribute]
        private int _persoInseres;
        public int PersonnagesInseres
        {
            get { return _persoInseres; }
            set 
            {
                _persoInseres = value;
                if (Simulation != null) Simulation.UpdateObservers(); // pour mettre à jour les stats de la fenêtre
            }
        }
        [XmlAttribute]
        private int _persoToInsert;
        public int PersonnagesToInsert
        {
            get { return _persoToInsert; }
            set
            {
                _persoToInsert = value;
                if (Simulation != null) Simulation.UpdateObservers(); // pour mettre à jour les stats de la fenêtre
            }
        }

        private int _persoQuiOntAtteintLeurBut;
        public int PersoQuiOntAtteintLeurBut
        {
            get { return _persoQuiOntAtteintLeurBut; }
            set
            {
                _persoQuiOntAtteintLeurBut = value;
                if (Simulation != null) Simulation.UpdateObservers(); // pour mettre à jour les stats de la fenêtre
            }
        }

        private int _persoQuiOntRecupereObjet;
        public int PersoQuiOntRecuperObjet
        {
            get { return _persoQuiOntRecupereObjet; }
            set
            {
                _persoQuiOntRecupereObjet = value;
                if (Simulation != null) Simulation.UpdateObservers(); // pour mettre à jour les stats de la fenêtre
            }
        }

        private int _persoQuiSontMorts;
        public int PersoQuiSontMorts
        {
            get { return _persoQuiSontMorts; }
            set
            {
                _persoQuiSontMorts = value;
                if (Simulation != null) Simulation.UpdateObservers(); // pour mettre à jour les stats de la fenêtre
            }
        }


        protected abstract PersonnageMobilisable PersonnageToInsertAt(Coordonnees coor);
        public QuartierGeneralAbstrait() { }
        protected QuartierGeneralAbstrait(Game simu)
        {
            PersonnagesToInsert = 5;
            Simulation = simu;
            Update(); // Pour mettre à jour la ZoneGénérale
        }

        public override void Update()
        {
            DeAttacherTousLesPersonnagesMobilises();
            if (Simulation != null) ZonePrincipale = Simulation.ZoneGenerale;
            ChargerPersonnagesAMobiliser();
        }
        protected void ChargerPersonnagesAMobiliser()
        {
            if (ZonePrincipale == null) return;
            var list = ZonePrincipale.ObtenirPersonnages();
            foreach (var perso in list)
            {
                var observer = perso as PersonnageMobilisable;
                if (observer != null)
                {
                    AttacherPersonnage(observer);
                    observer.SetQG(this);
                }
            }
        }

        public void GererUnTour(bool ignore)
        {
            if (Simulation == null) throw new NullReferenceException("Le QG n'est pas rattaché à une simulation !");
            // Si une simulation continue est en cours, impossible !
            if (!ignore && Simulation.Running)
            {
                Console.WriteLine("GererUnTour: Impossible de lancer un tour manuellement, car la simulation est en cours !");
                return;
            }
            if (!Simulation.EstValide())
            {
                Console.WriteLine("La simulation n'est pas valide, impossible donc de mobiliser les personnages");
                return;
            }
            ++Tour;
            MobiliserPersonnages();
            InsererPersonnagesRestants();
        }

        public void GererUnTour()
        {
            GererUnTour(false);
        }


        protected abstract void InsererPersonnagesRestants();


    }

    public class QuartierGeneralAOK : QuartierGeneralAbstrait
    {
        public QuartierGeneralAOK() : base()
        { }
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
