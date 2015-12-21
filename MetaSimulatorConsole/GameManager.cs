using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    enum NomJeu  { AgeOfKebab, CDGSimulator, HoneyWell };
    class GameManager 
    {
        public static List<NomJeu> ListeJeux = new List<NomJeu>()
        {
            { NomJeu.AgeOfKebab},
            { NomJeu.CDGSimulator},
            { NomJeu.HoneyWell}
        };
        public NomJeu JeuChoisi = ListeJeux[0];

        Dictionary<NomJeu, Game> Jeux = new Dictionary<NomJeu, Game>(){};

        protected GameManager()
        {
        }

        private Game creerJeu(NomJeu nomjeu) // Poids mouche
        {
             Game jeu = (Game)Jeux[nomjeu];

              if(jeu == null) {
                 jeu = GameFactorySelect.CreerJeu(nomjeu);
                 Console.WriteLine("Creation du jeu : " + nomjeu);
              }
              return jeu;
        }

        public Game CreerNouveauJeu()
        {
            return creerJeu(JeuChoisi);
        }

        private Game chargerJeu(NomJeu nomjeu) // + données XML en param
        {
            //Jeux[nomJeu] = new Game(DataXML)
            return null;
        }

        public void chosirJeu() { }
        public void chargerFichierXML(){} // Doit Permettre de restaurer ces instances de jeu depuis les fichiers XML

        public void save() { } // Sauvegarde les instances de jeu dans les XML

    }

}
