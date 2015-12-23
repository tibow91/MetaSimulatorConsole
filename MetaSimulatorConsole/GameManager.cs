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
        private Window _fenetre;
        public Window Fenetre
        {
            get { return _fenetre; }
        }

        public Grille TableauDeJeu;
        private Game simulation;
        public Game Simulation
        {
            get { return simulation; }
        }

        public GameManager()
        {
            DemanderJeuAChoisir();
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

        private Game creerJeu(NomJeu nomjeu) // Poids mouche
        {
            if (Jeux.ContainsKey(nomjeu))
                return Jeux[nomjeu];
            CreerTableauDeJeu();
              Jeux[nomjeu] = GameFactorySelect.CreerJeu(nomjeu, TableauDeJeu);

            Console.WriteLine("Creation du jeu : " + nomjeu);
            simulation = Jeux[nomjeu];
            return Jeux[nomjeu];
        }
       
        public Game CreerNouveauJeu()
        {
            return creerJeu(JeuChoisi);
        }

        private void CreerTableauDeJeu()
        {
            if (Grille.HasInstance()) return;
            Console.WriteLine("Quelles Dimensions pour le tableau de jeu ? Longueur = ?");
            int longueur=0,largeur=0;
            var ans = Console.ReadLine();
            if (ans != null) longueur = Int32.Parse(ans);
            Console.WriteLine("Largeur ?");
            ans = Console.ReadLine();
            if (ans != null) largeur = Int32.Parse(ans);
            TableauDeJeu = Grille.Instance(longueur, largeur);
            _fenetre = new Window(600,600,this);
        }

        private Game chargerJeu(NomJeu nomjeu) // + données XML en param
        {
            //Jeux[nomJeu] = new Game(DataXML)
            return null;
        }

        public void ChoisirJeu(NomJeu nomjeu)
        {
            JeuChoisi = nomjeu;
            Console.WriteLine("Jeu choisi: {0}", JeuChoisi);
        }
        public void chargerFichierXML(){} // Doit Permettre de restaurer ces instances de jeu depuis les fichiers XML

        public void save() { } // Sauvegarde les instances de jeu dans les XML

    }

}
