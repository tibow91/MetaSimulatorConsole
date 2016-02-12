using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{
    public class ComportementEnAttenteDeFaim : PersonnageBehavior
    {
        public ComportementEnAttenteDeFaim(Client perso) : base(perso) { }

        /* ici on analyse si l'état du personnage convient selon ses paramètres
         * et on change l'état selon les conclusions. L'état se chargera
         * de changer le comportement à associer automatiquement */
        public override void AnalyserSituation()
        {
            if(Personnage == null) throw new NullReferenceException("Personnage is null !");
            if(EtatPersonnage == null) throw new NullReferenceException("EtatPersonnage is null !");
            if (Personnage.PointsDeVie <= 0)
            {
                // Faire un état mort
                Personnage.Etat = null;
                Personnage.Comportement = null;
                // Changer la texture du personnage
                return;
            }

            if (EtatPersonnage is EtatClientEnAttenteDeFaim)
            {
                if (Personnage.PointsDeVie > Personnage.SeuilCritique) return;
                if (EtatPersonnage == null)
                {
                    Personnage.Etat = new EtatClientVaCommander();
                }
            }
        }

        /* Ici on exécute le comportement paramétré, qui lui aura un algorithme de recherche 
         * différent prédéfini pour chaque état */
        public override void Execution()
        {

            if (EtatPersonnage is EtatClientEnAttenteDeFaim)
            {
     
                // Décrémenter points de vie
                // S'il est dans la zone externe, il doit atteindre le point de rassemblement
            }
        }
        protected override Coordonnees CaseSuivante()
        {
            throw new NotImplementedException();
        }
    }
}
