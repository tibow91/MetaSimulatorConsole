using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    public class SimulationJeu
    {
        private List<OldPersonnage> PersonnagesList = new List<OldPersonnage>();
        Organisation EtatMajor = new Organisation();


        public String AfficheTous()
        {
            String display = "";
            foreach (var p in PersonnagesList)
            {
                display += p.Afficher() + "\n";
            }
            return display;
        }

        public String LancerCombat()
        {
            string chaineCombat = "";
            foreach (OldPersonnage p in PersonnagesList)
            {
                chaineCombat += p.Combattre() + "\n";
            }
            return chaineCombat;
        }

        public String EmettreSonTous()
        {
            String son = "";
            foreach (var p in PersonnagesList)
            {
                son += p.EmettreUnSon() + "\n";
            }
            return son;
        }

        public void CreationPersonnages()
        {
            PersonnagesList.Add(new Fantassin(EtatMajor,"Furious"));
            PersonnagesList.Add(new Archer(EtatMajor,"Vert"));
            PersonnagesList.Add(new Chevalier(EtatMajor,"Arthur"));
            PersonnagesList.Add(new Princesse(EtatMajor,"Fiona"));
        }

        public void ChangerComportement()
        {
            throw new NotImplementedException();
        }

        public String SeDeplacerTous()
        {
            String dep = "";
            foreach (var p in PersonnagesList)
            {
                dep += p.SeDeplacer() + "\n";
            }
            return dep;
        }

        private void AjouterALaSimulation(OldPersonnage p)
        {
            EtatMajor.Attach(p);
        }

        private void AjouterTousALaSimulation()
        {
            foreach(var p in PersonnagesList)
            {
                AjouterALaSimulation(p);
            }
        }

        private String AfficherEtatFonctionnementTous()
        {
            String display = "";
            foreach (var p in PersonnagesList)
            {
                display += p.AfficherModeFonctionnement() + "\n";
            }
            return display;
        }

        private void EnvoyerOrdreGuerre()
        {
            EtatMajor.ModeFonctionnement = eMode.Guerre;
            EtatMajor.UpdateObservers();
        }

        private void EnvoyerOrdrePaix()
        {
            EtatMajor.ModeFonctionnement = eMode.Paix;
            EtatMajor.UpdateObservers();
        }
        public SimulationJeu()
        {
            CreationPersonnages();
            Console.WriteLine("Affichage de tous les personnages: \n" + AfficheTous());
            Console.WriteLine("Les Personnages émettent chacun un Son: \n" + EmettreSonTous());
            Console.WriteLine("Les Personnages se déplacent: \n" + SeDeplacerTous());
            Console.WriteLine("Les Personnages combattent: \n" + LancerCombat());
            AjouterTousALaSimulation();
            EnvoyerOrdreGuerre();
            EnvoyerOrdrePaix();
        }

 
    }

}
