using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace MetaSimulatorConsole
{
    internal abstract class AppuiClavier : IObservateurAbstrait  // Chaine de Resp + invocateur de commandes
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

        protected AppuiClavier(Window fenetre)
        {
            Partie = fenetre;
        }

        public abstract void Traitement();
        protected void TraitementGenerique(Key touche)
        {
            if (Partie.Keyboard[touche])
            {
                ExecuteCommandes();
            }
            else if (CommandeSuivante != null)
                CommandeSuivante.Traitement();
        }

        public static AppuiClavier ChaineDeCommandes(Window fenetre)
        {

            AppuiClavier touche0 = new AppuiClavierToucheNum0(fenetre);
            AppuiClavier touche1 = new AppuiClavierToucheNum1(fenetre);
            AppuiClavier touche2 = new AppuiClavierToucheNum2(fenetre);

            touche0.SetCommandeSuivante(touche1);
            touche1.SetCommandeSuivante(touche2);
            touche2.SetCommandeSuivante(null);

            //fileLogger.setNextLogger(consoleLogger);

            return touche0;
        }

        public static AppuiClavier ChaineDeCommandesSpeciales(Window fenetre)
        {
            AppuiClavier toucheEchap = new AppuiClavierToucheEchap(fenetre);
            AppuiClavier touchePrecedent = new AppuiClavierTouchePrecedent(fenetre);

            toucheEchap.SetCommandeSuivante(touchePrecedent);
            touchePrecedent.SetCommandeSuivante(null);

            //fileLogger.setNextLogger(consoleLogger);

            return toucheEchap;
        }

        public override void Update()
        {
            foreach (var commande in commandes)
            {
                commande.Value.Update();
            }
            if(CommandeSuivante != null)
                CommandeSuivante.Update();
        }
    }


    class AppuiClavierToucheNum0 : AppuiClavier
    {
        public AppuiClavierToucheNum0(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new PasserAuMenuDeCreation(fenetre.Gestionnaire),EMenu.Principal );
            AjouterCommande(new CreerJeuAgeOfKebab(fenetre.Gestionnaire), EMenu.Creation);
            AjouterCommande(new PasserAuMenuPrincipal(fenetre.Gestionnaire), EMenu.Chargement);
            AjouterCommande(new StartSimulation(fenetre.Gestionnaire), EMenu.Simulation);
        }

        public override void Traitement()
        {
            TraitementGenerique(Key.Keypad0);            
        }
    }

    class AppuiClavierToucheNum1 : AppuiClavier
    {
        public AppuiClavierToucheNum1(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new PasserAuMenuDeChargement(fenetre.Gestionnaire), EMenu.Principal);
            AjouterCommande(new CreerJeuCDGSimulator(fenetre.Gestionnaire),EMenu.Creation);
            AjouterCommande(new StopSimulation(fenetre.Gestionnaire), EMenu.Simulation);
        }

        public override void Traitement()
        {
            TraitementGenerique(Key.Keypad1);            
        }
        
    }

    class AppuiClavierToucheNum2 : AppuiClavier
    {
        public AppuiClavierToucheNum2(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new CreerJeuHoneywell(fenetre.Gestionnaire),EMenu.Creation);
            AjouterCommande(new MontrerCacherInterface(fenetre.Gestionnaire), EMenu.Simulation);
        }
        public override void Traitement()
        {
            TraitementGenerique(Key.Keypad2);
        }
    }

    class AppuiClavierToucheEchap : AppuiClavier
    {
        public AppuiClavierToucheEchap(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new QuitterSimulation(fenetre.Gestionnaire),EMenu.Principal);
            AjouterCommande(new QuitterSimulation(fenetre.Gestionnaire), EMenu.Creation);
            AjouterCommande(new QuitterSimulation(fenetre.Gestionnaire), EMenu.Chargement);
            AjouterCommande(new QuitterSimulation(fenetre.Gestionnaire), EMenu.Simulation);

        }

        public override void Traitement()
        {
            TraitementGenerique(Key.Escape);
        }
    }

    class AppuiClavierTouchePrecedent : AppuiClavier
    {
        public AppuiClavierTouchePrecedent(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new PasserAuMenuPrincipal(fenetre.Gestionnaire),EMenu.Creation);
            AjouterCommande(new PasserAuMenuPrincipal(fenetre.Gestionnaire), EMenu.Chargement);
            AjouterCommande(new PasserAuMenuPrincipal(fenetre.Gestionnaire), EMenu.Simulation);    
        }

        public override void Traitement()
        {
            TraitementGenerique(Key.BackSpace);
        }
    }
}
