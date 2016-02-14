using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{
    
    public class Client: PersonnageMobilisable
    {
        public Client() : base("Client générique",EGame.AgeOfKebab,new TexturePlayer()) { }
        public Client(string nom,Coordonnees coor)
            : base(nom, EGame.AgeOfKebab, new TexturePlayer())
        {
            SetCoordonnees(coor);
            Etat = new EtatClientEnAttenteDeFaim();
        }

        /* ici on analyse si l'état du personnage convient selon ses paramètres
         * et on change l'état selon les conclusions, qui se chargera
         * de changer le comportement à associer automatiquement */
        public override void AnalyserSituation()
        {
            if (Comportement != null) Comportement.AnalyserSituation();

        }

        /* Ici on exécute le comportement paramétré, qui lui aura un algorithme de recherche 
         * différent prédéfini pour chaque état */
        public override void Execution()
        {
            if (Comportement != null) Comportement.Execution();
        }
        
        public Coordonnees AtteindreGatherPoint(ZoneFinale zone)
        {
            if(zone == null) throw new NullReferenceException("La zone de travail est nulle !");
            // Chercher les objets Gather Point dans la zone
            // Etablir les chemins pour y aller
            // Aller vers celui qui est le moins long
            return new ChercherCheminVersGatherPoint().CaseSuivante(this);
        }

        public Coordonnees SortirDuRestaurant(ZoneFinale zone)
        {
            return null;
        }
        public Coordonnees RentrerDansLeRestaurant(ZoneFinale zone)
        {
            return null;
        }

        public Coordonnees RejoindreCaisse(ZoneFinale zone)
        {
            return null;
        }

        public Coordonnees InteragirAvecCaissier(Serveur serveur)
        {
            return null;
        }
        public Coordonnees ChercherPlace(ZoneFinale zone)
        {
            return null;
        }

    }
}
