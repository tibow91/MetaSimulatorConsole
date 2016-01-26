using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    enum NomJeu  { AgeOfKebab, CDGSimulator, Honeyland };
    enum EMenu { Principal, Creation, Chargement, Simulation  };

    class GameManager : SujetObserveAbstrait
    {

        private static GameManager instance;

        public static List<NomJeu> ListeJeux = new List<NomJeu>()
        {
            { NomJeu.AgeOfKebab },
            { NomJeu.CDGSimulator },
            { NomJeu.Honeyland }
        };

        public NomJeu JeuChoisi = ListeJeux[0];

        Dictionary<NomJeu, Game> Jeux = new Dictionary<NomJeu, Game>(){};
        public Window Fenetre { get; set; }

        public Grille TableauDeJeu;
        private Game _simulation;
        public Game Simulation
        {
            get { return _simulation; }
            set
            {
                _simulation = value;
                Update();
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
                if (Simulation.Started)
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
        }

        public void PasserAuMenuDeChargement()
        {
            if (MenuCourant is MenuPrincipal)
                PasserAuMenu(EMenu.Chargement);
        }

        public void PasserAuMenuDeSimulation()
        {
            if (MenuCourant is MenuCreation)
                PasserAuMenu(EMenu.Simulation);
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

        public void DemanderJeuAChoisir()
        {
            
            int k = 0,i;
            do
            {
                i = 1;
                foreach (var jeu in ListeJeux)
                {
                    Console.WriteLine(i + ": " + jeu);
                    ++i;
                }
                string answer = "";
                do
                {
                    Console.WriteLine("Entrez le numéro du jeu que vous souhaitez simuler");
                    answer = Console.ReadLine();
                } while (String.IsNullOrEmpty(answer));
                k = Int32.Parse(answer);
            } while (k <= 0 || k >  ListeJeux.Count);
            ChoisirJeu(ListeJeux[k-1]);            

        }

        private Game creerJeu() // Poids mouche
        {
            NomJeu nomjeu = JeuChoisi;
            if (Jeux.ContainsKey(nomjeu))
            {
                Simulation = Jeux[nomjeu];
                Simulation.AfficherGrille();
                return Jeux[nomjeu];
            }
            CreerTableauDeJeu();
              Jeux[nomjeu] = GameFactorySelect.CreerJeu(nomjeu, this,TableauDeJeu);

            Console.WriteLine("Creation du jeu : " + nomjeu);
            Simulation = Jeux[nomjeu];
            return Jeux[nomjeu];
        }
       
        public Game CreerJeu(NomJeu nomjeu)
        {
            ChoisirJeu(nomjeu);
            return creerJeu();
        }

        private void CreerTableauDeJeu()
        {
            if (Grille.HasInstance()) return;
            //Console.WriteLine("Quelles Dimensions pour le tableau de jeu ? Longueur = ?");
            int longueur=50,largeur=50;
            //var ans = Console.ReadLine();
            //if (ans != null) longueur = Int32.Parse(ans);
            //Console.WriteLine("Largeur ?");
            //ans = Console.ReadLine();
            //if (ans != null) largeur = Int32.Parse(ans);
            TableauDeJeu = (Grille)Grille.Instance(longueur, largeur);
            this.Fenetre = new Window(600,600,this);
        }

        private Game chargerJeu(NomJeu nomjeu) // + données XML en param
        {
            //Jeux[nomJeu] = new Game(DataXML)
            return null;
        }

        private void ChoisirJeu(NomJeu nomjeu)
        {
            JeuChoisi = nomjeu;
            Console.WriteLine("Jeu choisi: {0}", JeuChoisi);
        }
        public void chargerFichierXML(){} // Doit Permettre de restaurer ces instances de jeu depuis les fichiers XML

        public void save() { } // Sauvegarde les instances de jeu dans les XML


        internal void LancerSimulation()
        {
            if (Simulation == null)
            {
                Console.WriteLine("Pas de simulation chargée");
                return;
            }
            if (Simulation.Started)
            {
                Console.WriteLine("Il y a déja une simulation en cours !");
                return;
            }
            Thread workerThread = new Thread(Simulation.LancerSimulation);
            workerThread.Start();
        }
    }

}
