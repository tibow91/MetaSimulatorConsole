using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    abstract class CommandGameManager // pattern commandes
    {
        protected GameManager gestionnaire;

        protected CommandGameManager(GameManager manager)
        {
            gestionnaire = manager;
        }
        public abstract void Execute();
    }

    class StopSimulation : CommandGameManager
    {
        public StopSimulation(GameManager manager) : base(manager){}
    
        public override void Execute()
        {
            Console.WriteLine("Demande d'arrêt de la simulation");
            if(gestionnaire != null)
                gestionnaire.Simulation.Stop = true;
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

    class ChangerJeu : CommandGameManager
    {
        private static bool Demande = false;
        public ChangerJeu(GameManager manager) : base(manager) { }

        public override void Execute()
        {
            Console.WriteLine("Demande de changement de Jeu");
            if (gestionnaire != null)
            {
                if (!Demande)
                {
                    Demande = true;
                    Thread workerThread = new Thread(this.operation);
                    workerThread.Start();
                }
                else
                {
                    Console.WriteLine("Demande De jeu déjà en cours !");
                }
            }
        }

        private void operation()
        {
            gestionnaire.DemanderJeuAChoisir();
            gestionnaire.CreerNouveauJeu();
            Demande = false;
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
}
