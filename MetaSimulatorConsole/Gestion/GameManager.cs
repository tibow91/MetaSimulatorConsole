using MetaSimulatorConsole.Gestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    public enum NomJeu  { AgeOfKebab, CDGSimulator, Honeyland };
    public enum EMenu { Principal, Creation, Chargement, Simulation  };

    public class GameManager : SujetObserveAbstrait,IObservateurAbstrait
    {
        public static readonly int Longueur = 25;
        public static readonly int Largeur = 25;

        private static GameManager instance;
        public static void TestInstance()
        {
            if (instance == null) throw new NullReferenceException("Gestionnaire est null !");
        }
        public static List<NomJeu> ListeJeux = new List<NomJeu>()
        {
            { NomJeu.AgeOfKebab },
            { NomJeu.CDGSimulator },
            { NomJeu.Honeyland }
        };

        public NomJeu JeuChoisi = ListeJeux[0];

        Dictionary<NomJeu, Game> Jeux = new Dictionary<NomJeu, Game>(){};
        public Window Fenetre { get; set; }
        private Grille tableauDeJeu;
        public Grille TableauDeJeu
        {
            get
            {
                return tableauDeJeu;
            }
            set
            {
                tableauDeJeu = value;
                UpdateObservers();
            }
        }
        private Game _simulation;
        public Game Simulation
        {
            get { return _simulation; }
            set
            {
                _simulation = value;
                UpdateObservers();
            }
        }

        /*----------------------------------
        * GESTION DES ETATS (MENUS)
        *----------------------------------*/
        // Liste des états possibles du gestionnaire de jeu
        public static readonly Dictionary<EMenu, Menu> Menus = new Dictionary<EMenu, Menu> 
        {
             { EMenu.Principal, new MenuPrincipal() },
             { EMenu.Creation, new MenuCreation() },
             { EMenu.Chargement, new MenuChargement() },
             { EMenu.Simulation, new MenuSimulation() }
        };
        // Etat actuel de la machine
        public Menu MenuCourant{ get; set; }

        private void PasserAuMenu(EMenu menu)
        {
            Menus[menu].ModifieEtat(this);
        }

        public void PasserAuMenuPrincipal()
        {
            if (MenuCourant is MenuSimulation)
            {
                if (Simulation != null && Simulation.Running)
                {
                    Console.WriteLine("Veuillez arrêter la simulation pour revenir au menu principal");
                    return;
                }
            }
            PasserAuMenu(EMenu.Principal);
        }

        public void PasserAuMenuDeCreation()
        {
            if(MenuCourant is MenuPrincipal)
                PasserAuMenu(EMenu.Creation);
            else throw new InvalidCastException("Cette comande est indisponible dans le menu proposé");
        }

        public void PasserAuMenuDeChargement()
        {
            if (MenuCourant is MenuPrincipal)
                PasserAuMenu(EMenu.Chargement);
            else throw new InvalidCastException("Cette comande est indisponible dans le menu proposé");
        }

        public void PasserAuMenuDeSimulation()
        {
            if (MenuCourant is MenuCreation || MenuCourant is MenuChargement)
                PasserAuMenu(EMenu.Simulation);
            else throw new InvalidCastException("Cette comande est indisponible dans le menu proposé");
        }

        
        /*----------------------------------
        * CONSTRUCTEUR
        *----------------------------------*/
        protected GameManager()
        {
            //DemanderJeuAChoisir();
            CreerTableauDeJeu();
            AttachObservers();
            PasserAuMenuPrincipal();
        }

        public static GameManager Instance()
        {
            if (instance == null)
                instance = new GameManager();

            return instance;
        }

        private void AttachObservers()
        {
            foreach (var o in Fenetre.Observers())
            {
                Attach(o);
            }
        }

        private void creerJeu() // Poids mouche
        {
            NomJeu nomjeu = JeuChoisi;
            if (Jeux.ContainsKey(nomjeu))
            {
                Simulation = Jeux[nomjeu];
                Simulation.CreerUneNouvellePartie(this);
                return;
            }
            Jeux[nomjeu] = GameFactorySelect.CreerJeu(nomjeu, this,TableauDeJeu);
            Console.WriteLine("Creation du jeu : " + nomjeu);
            Simulation = Jeux[nomjeu];
            Simulation.CreerUneNouvellePartie(this);
        }
       
        public void CreerJeu(NomJeu nomjeu)
        {
            ChoisirJeu(nomjeu);
            creerJeu();
        }

        private void CreerTableauDeJeu()
        {
            if (Grille.HasInstance()) return;
            TableauDeJeu = (Grille)Grille.Instance(Longueur, Largeur);
            this.Fenetre = new Window(600,600,this);
        }


        private void ChoisirJeu(NomJeu nomjeu)
        {
            JeuChoisi = nomjeu;
            Console.WriteLine("Jeu choisi: {0}", JeuChoisi);
        }
        public bool chargerFichierXML(EGame game) // Doit Permettre de restaurer ces instances de jeu depuis les fichiers XML
        {
            bool success = false ;
            switch(game)
            {
                case EGame.AgeOfKebab: success = new LoadGameAOK(this).ChargerJeuDepuisXML(); break;
                case EGame.CDGSimulator: success = new LoadGameCDGSimulator(this).ChargerJeuDepuisXML(); break;
                case EGame.Honeyland: success = new LoadGameHoneyland(this).ChargerJeuDepuisXML(); break;
                default: break;
            }
            return success;
        }


        public void SauvegarderSimulation() // Sauvegarde les instances de jeu dans les XML
        {
            if (Simulation == null) throw new InvalidOperationException("La sauvegarde ne doit pouvoir s'effectuer que si une partie est en cours");
            if (Simulation.Running) new StopSimulation(this).Execute();
            switch(JeuChoisi)
            {
                case NomJeu.AgeOfKebab: new SaveGameAOK(this).Sauvegarder(); break;
                case NomJeu.CDGSimulator: new SaveGameCDGSimulator(this).Sauvegarder(); break;
                case NomJeu.Honeyland: new SaveGameHoneyland(this).Sauvegarder(); break;
                default: break;
            }

        } 


        internal void LancerSimulation()
        {
            if (Simulation == null)
            {
                Console.WriteLine("Pas de simulation chargée");
                return;
            }
            if (Simulation.Running)
            {
                Console.WriteLine("Il y a déja une simulation en cours !");
                return;
            }
            Thread workerThread = new Thread(Simulation.LancerSimulation);
            workerThread.Start();
        }


        public void Update()
        {
            UpdateObservers(); // Initialise la mise à jour de ses propres observers
        }
    }

}
