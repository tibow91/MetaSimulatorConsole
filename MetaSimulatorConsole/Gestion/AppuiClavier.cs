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
            AppuiClavier toucheK0 = new AppuiClavierToucheKeypad0(fenetre);
            AppuiClavier toucheK1 = new AppuiClavierToucheKeypad1(fenetre);
            AppuiClavier toucheK2 = new AppuiClavierToucheKeypad2(fenetre);
            AppuiClavier toucheK3 = new AppuiClavierToucheKeypad3(fenetre);
            AppuiClavier toucheK4 = new AppuiClavierToucheKeypad4(fenetre);

            AppuiClavier toucheN0 = new AppuiClavierToucheNumber0(fenetre);
            AppuiClavier toucheN1 = new AppuiClavierToucheNumber1(fenetre);
            AppuiClavier toucheN2 = new AppuiClavierToucheNumber2(fenetre);
            AppuiClavier toucheN3 = new AppuiClavierToucheNumber3(fenetre);
            AppuiClavier toucheN4 = new AppuiClavierToucheNumber4(fenetre);

            toucheK0.SetCommandeSuivante(toucheK1);
            toucheK1.SetCommandeSuivante(toucheK2);
            toucheK2.SetCommandeSuivante(toucheK3);
            toucheK3.SetCommandeSuivante(toucheK4);
            toucheK4.SetCommandeSuivante(toucheN0);

            toucheN0.SetCommandeSuivante(toucheN1);
            toucheN1.SetCommandeSuivante(toucheN2);
            toucheN2.SetCommandeSuivante(toucheN3);
            toucheN3.SetCommandeSuivante(toucheN4);
            toucheN4.SetCommandeSuivante(null);

            //fileLogger.setNextLogger(consoleLogger);

            return toucheK0;
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

        public void Update()
        {
            foreach (var commande in commandes)
            {
                commande.Value.Update();
            }
            if(CommandeSuivante != null)
                CommandeSuivante.Update();
        }
    }

    class AppuiClavierToucheNumber0 : AppuiClavier
    {
        public AppuiClavierToucheNumber0(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new PasserAuMenuDeCreation(fenetre.Gestionnaire), EMenu.Principal);
            AjouterCommande(new CreerJeuAgeOfKebab(fenetre.Gestionnaire), EMenu.Creation);
            AjouterCommande(new StartSimulation(fenetre.Gestionnaire), EMenu.Simulation);
            AjouterCommande(new ChargerLeJeuAOK(fenetre.Gestionnaire), EMenu.Chargement);
        }

        public override void Traitement()
        {
            TraitementGenerique(Key.Number0);
        }
    }

    class AppuiClavierToucheKeypad0 : AppuiClavier
    {
        public AppuiClavierToucheKeypad0(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new PasserAuMenuDeCreation(fenetre.Gestionnaire), EMenu.Principal );
            AjouterCommande(new CreerJeuAgeOfKebab(fenetre.Gestionnaire), EMenu.Creation);
            AjouterCommande(new StartSimulation(fenetre.Gestionnaire), EMenu.Simulation);
            AjouterCommande(new ChargerLeJeuAOK(fenetre.Gestionnaire), EMenu.Chargement);
        }

        public override void Traitement()
        {
            TraitementGenerique(Key.Keypad0);            
        }
    }

    class AppuiClavierToucheNumber1 : AppuiClavier
    {
        public AppuiClavierToucheNumber1(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new PasserAuMenuDeChargement(fenetre.Gestionnaire), EMenu.Principal);
            AjouterCommande(new CreerJeuCDGSimulator(fenetre.Gestionnaire), EMenu.Creation);
            AjouterCommande(new StopSimulation(fenetre.Gestionnaire), EMenu.Simulation);
            AjouterCommande(new ChargerLeJeuCDGSimulator(fenetre.Gestionnaire), EMenu.Chargement);
        }

        public override void Traitement()
        {
            TraitementGenerique(Key.Number1);
        }
    }

    class AppuiClavierToucheKeypad1 : AppuiClavier
    {
        public AppuiClavierToucheKeypad1(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new PasserAuMenuDeChargement(fenetre.Gestionnaire), EMenu.Principal);
            AjouterCommande(new CreerJeuCDGSimulator(fenetre.Gestionnaire), EMenu.Creation);
            AjouterCommande(new StopSimulation(fenetre.Gestionnaire), EMenu.Simulation);
            AjouterCommande(new ChargerLeJeuCDGSimulator(fenetre.Gestionnaire), EMenu.Chargement);
        }

        public override void Traitement()
        {
            TraitementGenerique(Key.Keypad1);            
        }  
    }

    class AppuiClavierToucheNumber2 : AppuiClavier
    {
        public AppuiClavierToucheNumber2(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new CreerJeuHoneyland(fenetre.Gestionnaire), EMenu.Creation);
            AjouterCommande(new MontrerCacherInterface(fenetre.Gestionnaire), EMenu.Simulation);
            AjouterCommande(new ChargerLeJeuHoneyland(fenetre.Gestionnaire), EMenu.Chargement);
        }
        public override void Traitement()
        {
            TraitementGenerique(Key.Number2);
        }
    }

    class AppuiClavierToucheKeypad2 : AppuiClavier
    {
        public AppuiClavierToucheKeypad2(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new CreerJeuHoneyland(fenetre.Gestionnaire), EMenu.Creation);
            AjouterCommande(new MontrerCacherInterface(fenetre.Gestionnaire), EMenu.Simulation);
            AjouterCommande(new ChargerLeJeuHoneyland(fenetre.Gestionnaire), EMenu.Chargement);
        }
        public override void Traitement()
        {
            TraitementGenerique(Key.Keypad2);
        }
    }

    class AppuiClavierToucheNumber3 : AppuiClavier
    {
        public AppuiClavierToucheNumber3(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new LancerUnTourDeJeu(fenetre.Gestionnaire), EMenu.Simulation);
        }

        public override void Traitement()
        {
            TraitementGenerique(Key.Number3);
        }
    }


    class AppuiClavierToucheKeypad3 : AppuiClavier
    {
        public AppuiClavierToucheKeypad3(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new LancerUnTourDeJeu(fenetre.Gestionnaire), EMenu.Simulation);
        }
        public override void Traitement()
        {
            TraitementGenerique(Key.Keypad3);
        }
    }

    class AppuiClavierToucheNumber4 : AppuiClavier
    {
        public AppuiClavierToucheNumber4(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new SauvegarderSimulationEnCours(fenetre.Gestionnaire), EMenu.Simulation);
        }

        public override void Traitement()
        {
            TraitementGenerique(Key.Number4);
        }
    }


    class AppuiClavierToucheKeypad4 : AppuiClavier
    {
        public AppuiClavierToucheKeypad4(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new SauvegarderSimulationEnCours(fenetre.Gestionnaire), EMenu.Simulation);
        }
        public override void Traitement()
        {
            TraitementGenerique(Key.Keypad4);
        }
    }





    class AppuiClavierToucheEchap : AppuiClavier
    {
        public AppuiClavierToucheEchap(Window fenetre)
            : base(fenetre)
        {
            AjouterCommande(new QuitterSimulation(fenetre.Gestionnaire), EMenu.Principal);
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
            AjouterCommande(new PasserAuMenuPrincipal(fenetre.Gestionnaire), EMenu.Creation);
            AjouterCommande(new PasserAuMenuPrincipal(fenetre.Gestionnaire), EMenu.Chargement);
            AjouterCommande(new PasserAuMenuPrincipal(fenetre.Gestionnaire), EMenu.Simulation);    
        }

        public override void Traitement()
        {
            TraitementGenerique(Key.BackSpace);
        }
    }
}
