using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace MetaSimulatorConsole
{
    internal abstract class AppuiClavier // Chaine de Resp + invocateur de commandes
    {
        protected AppuiClavier CommandeSuivante;
        private Dictionary<EMenu,CommandGameManager> commandes = new Dictionary<EMenu,CommandGameManager>();
        protected Window Partie;

        public void SetCommandeSuivante(AppuiClavier suivant)
        {
            CommandeSuivante = suivant;
        }

        protected void AjouterCommande(CommandGameManager uneCommande, EMenu menu)
        {
            commandes.Add(menu,uneCommande);
        }
        protected void ExecuteCommandes()
        {
            foreach (var commande in commandes)
            {
                if (Partie.Gestionnaire.MenuCourant is MenuPrincipal)
                {
                    if(commandes.ContainsKey(EMenu.Principal))
                        commandes[EMenu.Principal].Execute();
                }
                else if (Partie.Gestionnaire.MenuCourant is MenuCreation)
                {
                    if (commandes.ContainsKey(EMenu.Creation))
                        commandes[EMenu.Creation].Execute();
                }
                else if (Partie.Gestionnaire.MenuCourant is MenuChargement)
                {
                    if (commandes.ContainsKey(EMenu.Chargement))
                        commandes[EMenu.Chargement].Execute();
                }
                else if (Partie.Gestionnaire.MenuCourant is MenuSimulation)
                {
                    if (commandes.ContainsKey(EMenu.Simulation))
                        commandes[EMenu.Simulation].Execute();
                }
            }
        }

        protected AppuiClavier(Window fenetre)
        {
            Partie = fenetre;
        }

        public abstract void Traitement();

        public static AppuiClavier ChaineDeCommandes(Window fenetre)
        {

            AppuiClavier touche0 = new AppuiClavierToucheNum0(fenetre);
            AppuiClavier touche1 = new AppuiClavierToucheNum1(fenetre);
            AppuiClavier touche2 = new AppuiClavierToucheNum2(fenetre);
            //AppuiClavier toucheEchap  = new AppuiClavierToucheEchap(fenetre);

            touche0.SetCommandeSuivante(touche1);
            touche1.SetCommandeSuivante(touche2);
            touche2.SetCommandeSuivante(null);
            //toucheEchap.SetCommandeSuivante(null);

            //fileLogger.setNextLogger(consoleLogger);

            return touche0;
        }

        public static AppuiClavier ChaineDeCommandesSpeciales(Window fenetre)
        {
            AppuiClavier toucheEchap = new AppuiClavierToucheEchap(fenetre);
            toucheEchap.SetCommandeSuivante(null);

            //fileLogger.setNextLogger(consoleLogger);

            return toucheEchap;
        }


    }


    class AppuiClavierToucheNum0 : AppuiClavier
    {
        public AppuiClavierToucheNum0(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new StopSimulation(fenetre.Gestionnaire),EMenu.Simulation);
            AjouterCommande(new PasserAuMenuPrincipal(fenetre.Gestionnaire),EMenu.Principal );
        }

        public override void Traitement()
        {
            if (Partie.Keyboard[Key.Keypad0])
            {
                ExecuteCommandes();
            }
            else if(CommandeSuivante != null)
                    CommandeSuivante.Traitement();
            
        }
    }

    class AppuiClavierToucheNum1 : AppuiClavier
    {
        public AppuiClavierToucheNum1(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new StartSimulation(fenetre.Gestionnaire));
        }
        public override void Traitement()
        {
            if (Partie.Keyboard[Key.Keypad1])
            {
                ExecuteCommandes();
            }
            else if (CommandeSuivante != null)
                CommandeSuivante.Traitement();
        }
    }

    class AppuiClavierToucheNum2 : AppuiClavier
    {
        public AppuiClavierToucheNum2(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new ChangerJeu(fenetre.Gestionnaire));
        }
        public override void Traitement()
        {
            if (Partie.Keyboard[Key.Keypad2])
            {
                ExecuteCommandes();
            }
            else if (CommandeSuivante != null)
                CommandeSuivante.Traitement();
        }
    }

    class AppuiClavierToucheEchap : AppuiClavier
    {
        public AppuiClavierToucheEchap(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new QuitterSimulation(fenetre.Gestionnaire));
        }
        public override void Traitement()
        {
            //Console.WriteLine("Traitement de l'appui clavier touche echap ...");
            if (Partie.Keyboard[Key.Escape])
            {
                Console.WriteLine("appui clavier touche echap Détecté !");

                ExecuteCommandes();
            }
            else if (CommandeSuivante != null)
                CommandeSuivante.Traitement();
        }
    }
}
