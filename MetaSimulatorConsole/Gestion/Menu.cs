using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    public abstract class Menu
    {
        public abstract void ModifieEtat(GameManager manager);
        public abstract String AfficherEtat();
    }

    class MenuPrincipal : Menu
    {
        public override void ModifieEtat(GameManager manager)
        {
            manager.MenuCourant = this;
            manager.Fenetre.TextMenu.Clear();
            manager.Fenetre.TextMenu.Add("Créer une nouvelle simulation", "Touche 0");
            manager.Fenetre.TextMenu.Add("Charger une simulation", "Touche 1");
        }

        public override string AfficherEtat()
        {
            return "Menu Principal";
        }
    }

    class MenuCreation : Menu
    {
        public override void ModifieEtat(GameManager manager)
        {
            manager.MenuCourant = this;
            manager.Fenetre.TextMenu.Clear();
            manager.Fenetre.TextMenu.Add("Age Of Kebab", "Touche 0");
            manager.Fenetre.TextMenu.Add("CDGSimulator", "Touche 1");
            manager.Fenetre.TextMenu.Add("Honeyland", "Touche 2");
            manager.Fenetre.TextMenu.Add("Retour", "Touche Prec.");
        }

        public override string AfficherEtat()
        {
            return "Menu Création d'un nouveau jeu";
        }
    }

    class MenuChargement : Menu
    {
        public override void ModifieEtat(GameManager manager)
        {
            manager.MenuCourant = this;
            manager.Fenetre.TextMenu.Clear();
            manager.Fenetre.TextMenu.Add("Charger le jeu Age Of Kebab", "Touche 0");
            manager.Fenetre.TextMenu.Add("Charger le jeu CDG Simulator", "Touche 1");
            manager.Fenetre.TextMenu.Add("Charger le jeu Honeyland", "Touche 2");
            manager.Fenetre.TextMenu.Add("Retour", "Touche Prec.");
        }

        public override string AfficherEtat()
        {
            return "Menu Chargement d'un jeu";
        }   
    }

    class MenuSimulation : Menu
    {
        public override void ModifieEtat(GameManager manager)
        {
            manager.MenuCourant = this;
            manager.Fenetre.TextMenu.Clear();
            manager.Fenetre.TextMenu.Add("Lancer Un Tour De Jeu", "Touche 3");
            manager.Fenetre.TextMenu.Add("Lancer la simulation en continu", "Touche 0");
            manager.Fenetre.TextMenu.Add("Arrêter la simulation en continu", "Touche 1");
            manager.Fenetre.TextMenu.Add("Sauvegarder Simulation", "Touche 4");
            manager.Fenetre.TextMenu.Add("Cacher l'interface de commandes ", "Touche 2");
            manager.Fenetre.TextMenu.Add("Montrer statistiques", "Touche 5");
            manager.Fenetre.TextMenu.Add("Incrément. Nb de Personnages ", "Touche 6");
            manager.Fenetre.TextMenu.Add("Retour au menu principal", "Touche Prec.");
        }

        public override string AfficherEtat()
        {
            return "Menu Simulation d'un jeu";
        }    
    }

}
