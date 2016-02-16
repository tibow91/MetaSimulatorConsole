using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MetaSimulatorConsole.Simulation;
using MetaSimulatorConsole.Gestion;

namespace MetaSimulatorConsole
{
    abstract class CommandGameManager : IObservateurAbstrait // pattern commandes
    {
        protected GameManager gestionnaire;

        protected CommandGameManager(GameManager manager)
        {
            gestionnaire = manager;
        }
        public abstract void Execute();

        public virtual void Update()
        {
        }
    }

    class StopSimulation : CommandGameManager
    {
        public StopSimulation(GameManager manager) : base(manager){ Update();}
        private Game Simulation;
        public override void Execute()
        {
            Console.WriteLine("Demande d'arrêt de la simulation");
            if(Simulation != null)
                Simulation.Stop = true;
        }
        public override void Update()
        {
            if (gestionnaire != null )
                Simulation = gestionnaire.Simulation;
            //Console.WriteLine("Mise à jour de la commande Stop Simulation");
        }
    }

    class StartSimulation : CommandGameManager
    {
        public StartSimulation(GameManager manager): base(manager){}
        public override void Execute()
        {
            Console.WriteLine("Demande de lancement de la simulation");
            if (gestionnaire != null)
            {
                gestionnaire.LancerSimulation();
            }
        }
    }

    class QuitterSimulation : CommandGameManager
    {
        public QuitterSimulation(GameManager manager) : base(manager) { }

        public override void Execute()
        {
            Console.WriteLine("Vous avez demandé à quitter la simulation");
            if (gestionnaire != null)
            {
                new StopSimulation(gestionnaire).Execute();
                Console.WriteLine("Arrêt du programme ...");
                // Attendre un moment
                gestionnaire.Fenetre.Exit();
            }
        }
    }

    class PasserAuMenuPrincipal : CommandGameManager
    {
        public PasserAuMenuPrincipal(GameManager manager) : base(manager) { }

        public override void Execute()
        {
            Console.WriteLine("Vous avez demandé à passer au menu principal");
            if (gestionnaire != null)
            {
                gestionnaire.PasserAuMenuPrincipal();
            }
        }
    }

    class PasserAuMenuDeCreation : CommandGameManager
    {
        public PasserAuMenuDeCreation(GameManager manager) : base(manager) { }

        public override void Execute()
        {
            Console.WriteLine("Vous avez demandé à passer au menu de création de simulation");
            if (gestionnaire != null)
            {
                gestionnaire.PasserAuMenuDeCreation();
            }
        }
    }

    class PasserAuMenuDeChargement : CommandGameManager
    {
        public PasserAuMenuDeChargement(GameManager manager) : base(manager) { }

        public override void Execute()
        {
            Console.WriteLine("Vous avez demandé à passer au menu de chargement d'une simulation");
            if (gestionnaire != null)
            {
                gestionnaire.PasserAuMenuDeChargement();
            }
        }
    }

    class PasserAuMenuDeSimulation : CommandGameManager
    {
        public PasserAuMenuDeSimulation(GameManager manager) : base(manager) { }

        public override void Execute()
        {
            Console.WriteLine("Vous avez demandé à passer au menu de lancement d'une simulation");
            if (gestionnaire != null)
            {
                gestionnaire.PasserAuMenuDeSimulation();
            }
        }
    }

    class CreerJeuAgeOfKebab : CommandGameManager
    {
        public CreerJeuAgeOfKebab(GameManager manager) : base(manager) { }

        public override void Execute()
        {
            Console.WriteLine("Vous avez demandé à lancer le jeu Age Of Kebab");
            if (gestionnaire != null)
            {
                gestionnaire.CreerJeu(NomJeu.AgeOfKebab);
                new PasserAuMenuDeSimulation(gestionnaire).Execute();
            }
        }
    }

    class CreerJeuCDGSimulator : CommandGameManager
    {
        public CreerJeuCDGSimulator(GameManager manager) : base(manager) { }

        public override void Execute()
        {
            Console.WriteLine("Vous avez demandé à lancer le jeu CDGSimulator");
            if (gestionnaire != null)
            {
                gestionnaire.CreerJeu(NomJeu.CDGSimulator);
                new PasserAuMenuDeSimulation(gestionnaire).Execute();
            }
        }
    }
    class CreerJeuHoneyland : CommandGameManager
    {
        public CreerJeuHoneyland(GameManager manager) : base(manager) { }

        public override void Execute()
        {
            Console.WriteLine("Vous avez demandé à lancer le jeu Honeyland");
            if (gestionnaire != null)
            {
                gestionnaire.CreerJeu(NomJeu.Honeyland);
                new PasserAuMenuDeSimulation(gestionnaire).Execute();
            }
        }
    }

    class MontrerCacherInterface : CommandGameManager
    {
        public MontrerCacherInterface(GameManager manager) : base(manager) { Update(); }
        private bool _showInterface;

        private bool ShowInterface
        {
            get {  return _showInterface;}
            set
            {
                gestionnaire.Fenetre.ShowInterface = value;
                _showInterface = value;
            }
        }


        public override void Execute()
        {
            Console.WriteLine("Vous avez demandé à montrer l'interface du jeu");
            if (gestionnaire != null)
            {
                if(gestionnaire.MenuCourant is MenuSimulation)
                {
                    ShowInterface = !ShowInterface;
                }
            }
        }
        public override void Update()
        {
            if (gestionnaire != null && gestionnaire.Fenetre != null)
                ShowInterface = gestionnaire.Fenetre.ShowInterface;
            //Console.WriteLine("Mise à jour de la commande MontrerCacherInterface");
        }
    }

    class MontrerCacherStats : CommandGameManager
    {
        public MontrerCacherStats(GameManager manager) : base(manager) { }
        private bool _showStats;

        private bool ShowStats
        {
            get { return _showStats; }
            set
            {
                gestionnaire.Fenetre.ShowStats = value;
                _showStats = value;
            }
        }

        public override void Execute()
        {
            Console.WriteLine("Vous avez demandé à montrer les statistiques de la simulation");
            if (gestionnaire == null) throw new NullReferenceException("Gestionnaire is null !");            
            ShowStats = !ShowStats;        
        }

        public override void Update()
        {
            if (gestionnaire == null) throw new NullReferenceException("Gestionnaire is null !");
            if (gestionnaire.Fenetre == null) throw new NullReferenceException("La Fenêtre est null !");
            _showStats = gestionnaire.Fenetre.ShowStats;
            //Console.WriteLine("Mise à jour de la commande MontrerCacherInterface");
        }

    }

    class LancerUnTourDeJeu : CommandGameManager
    {
        protected Game Simulation;
        protected QuartierGeneralAbstrait QG;
        public LancerUnTourDeJeu(GameManager manager) : base(manager) { Update(); }

        public override void Execute()
        {
            if (QG != null)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Vous avez demandé à lancer un tour de jeu");
                QG.GererUnTour();
            }
            else throw new NullReferenceException("QG is Null");
        }


        public override void Update()
        {
            if (gestionnaire != null) Simulation = gestionnaire.Simulation;
            if (Simulation != null) QG = Simulation.QG;
            //Console.WriteLine("Mise à jour de la commande 'LancerUnTourDeJeu'");
        }
    }

    class ChargerLeJeuAOK : CommandGameManager
    {
        public ChargerLeJeuAOK(GameManager manager) : base(manager) {  }

        public override void Execute()
        {
            Console.WriteLine("Vous avez demandé à charger le jeu Age Of Kebab");
            if(new LoadAOKAdapter(gestionnaire).Load())
                new PasserAuMenuDeSimulation(gestionnaire).Execute();
        }
    }

    class ChargerLeJeuCDGSimulator : CommandGameManager
    {
        public ChargerLeJeuCDGSimulator(GameManager manager) : base(manager) { }

        public override void Execute()
        {
            Console.WriteLine("Vous avez demandé à charger le jeu CDG Simulator");
            if(new LoadCDGSimulatorAdapter(gestionnaire).Load())
                new PasserAuMenuDeSimulation(gestionnaire);
        }
    }

    class ChargerLeJeuHoneyland : CommandGameManager
    {
        public ChargerLeJeuHoneyland(GameManager manager) : base(manager) { }

        public override void Execute()
        {
            Console.WriteLine("Vous avez demandé à charger le jeu HoneyLand");
            if(new LoadHoneylandAdapter(gestionnaire).Load())
                new PasserAuMenuDeSimulation(gestionnaire);
        }
    }

    class SauvegarderSimulationEnCours : CommandGameManager
    {
        public SauvegarderSimulationEnCours(GameManager manager) : base(manager) { }
        public override void Execute()
        {
            Console.WriteLine("Vous avez demandé à sauvegarder la simulation en cours !");
            GameManager.TestInstance();
            if (gestionnaire != null) gestionnaire.SauvegarderSimulation();
        }
    }
}
