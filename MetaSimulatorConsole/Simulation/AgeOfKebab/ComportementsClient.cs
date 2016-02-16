using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{
    [XmlInclude(typeof(ComportementEnAttenteDeFaim)),XmlInclude(typeof(ComportementClientEtatMort))]
    [XmlInclude(typeof(ComportementVaCommander)), XmlInclude(typeof(ComportementClientEtatMort))]

    public abstract class ClientBehavior : PersonnageBehavior
    {
        protected Client PersonnageClient
        {
            get { return (Client) Personnage; }
            set { Personnage = value; }
        }
        public ClientBehavior() : base() { }
        protected ClientBehavior(Client perso) : base(perso)
        {

        }

        protected bool DetecterMortPersonnage()
        {
            if (PersonnageClient == null) throw new NullReferenceException("Personnage is null !");
            if (EtatPersonnage == null) throw new NullReferenceException("EtatPersonnage is null !");
            if (PersonnageClient.PointsDeVie <= 0)
            {
                Console.WriteLine("Le personnage " + PersonnageClient + " est mort !");
                PersonnageClient.Etat = new EtatMort();
                Personnage.Comportement = new ComportementClientEtatMort(PersonnageClient);
                return true;
            }
            return false;
        }
    }
    public class ComportementEnAttenteDeFaim : ClientBehavior
    {
        public ComportementEnAttenteDeFaim() : base() { }
        public ComportementEnAttenteDeFaim(Client perso) : base(perso) { }

        /* ici on analyse si l'état du personnage convient selon ses paramètres
         * et on change l'état selon les conclusions. L'état se chargera
         * de changer le comportement à associer automatiquement */
        public override void AnalyserSituation()
        {
            DetecterMortPersonnage();
            if (EtatPersonnage is EtatClientEnAttenteDeFaim)
            {
                if (PersonnageClient.PointsDeVie > PersonnageClient.SeuilCritique) return; // Pas de changement
                    PersonnageClient.Etat = new EtatClientVaCommander();
            }
            else throw new InvalidOperationException("L'état du personnage ne correspond pas !!");
        }

        /* Ici on exécute le comportement paramétré, qui lui aura un algorithme de recherche 
         * différent prédéfini pour chaque état */
        public override void Execution()
        {
//            Console.WriteLine("Le personnage s'apprête à effectuer une action ...");
            if (EtatPersonnage is EtatClientEnAttenteDeFaim)
            {
                // Décrémenter points de vie
                // S'il est dans la zone externe, il doit atteindre le point de rassemblement

                if(PersonnageClient.PointsDeVie > 0) --PersonnageClient.PointsDeVie;
                if (ZoneActuelle.nom == "Zone Externe")
                {
                    var coor = PersonnageClient.AtteindreGatherPoint(ZoneActuelle);
                    if (coor != null && coor.EstValide())
                    {
                        Console.WriteLine("Déplacement du personnage de " + PersonnageClient.Case +"  vers " + coor + ", direction pt de Rassemblement");
                        PersonnageClient.Case = coor;
                    }
                }
                else throw new InvalidOperationException("Le personnage n'est pas dans la zone externe ..");
            }
            else throw new InvalidCastException("Le comportement ne correspond pas à l'état du personnage !");
        }
    }

    public class ComportementVaCommander : ClientBehavior
    {
        public ComportementVaCommander() : base() { }
        public ComportementVaCommander(Client perso) : base(perso) { }
        public override void AnalyserSituation()
        {
            DetecterMortPersonnage();
            if (EtatPersonnage is EtatClientVaCommander)
            {
                // si le personnage est à la caisse, passer en l'état passe une commande
                return;
            }
            else throw new InvalidOperationException("L'état du personnage ne correspond pas !!");
        }

        public override void Execution()
        {
            if (EtatPersonnage is EtatClientVaCommander)
            {
                // Décrémenter points de vie
                // S'il est dans la zone externe, il doit atteindre le point de rassemblement

                if (PersonnageClient.PointsDeVie > 0) --PersonnageClient.PointsDeVie;
                if (ZoneActuelle.nom == "Zone Externe")
                {
                    var coor = PersonnageClient.RentrerDansLeRestaurant(ZoneActuelle);
                    //if (coor != null && coor.EstValide())
                    //{
                    //    Console.WriteLine("Déplacement du personnage de " + PersonnageClient.Case + "  vers " + coor + ", direction pt de Rassemblement");
                    //    PersonnageClient.Case = coor;
                    //}
                }
                else throw new InvalidOperationException("Le personnage n'est pas dans la zone externe ..");
            }
            else throw new InvalidCastException("Le comportement ne correspond pas à l'état du personnage !");
        }
    }
    public class ComportementClientEtatMort : ClientBehavior
    {
        public ComportementClientEtatMort() : base() { }
        public ComportementClientEtatMort(Client perso) : base(perso) { }
        public override void AnalyserSituation()
        {
            if (PersonnageClient == null) throw new NullReferenceException("Personnage is null !");
            if (EtatPersonnage == null) throw new NullReferenceException("EtatPersonnage is null !");
            if (PersonnageClient.PointsDeVie > 0) throw new InvalidOperationException("Le personnage a des points de vie > 0 !"); // Pas de changement

            if (EtatPersonnage is EtatMort)
            {
                return;
            }
            else throw new InvalidOperationException("L'état du personnage ne correspond pas au comportement !!");
        }


        public override void Execution()
        {
            if (PersonnageClient == null) throw new NullReferenceException("Personnage is null !");
            if (EtatPersonnage == null) throw new NullReferenceException("EtatPersonnage is null !");
            if (EtatPersonnage is EtatMort)  return;
            else throw new InvalidOperationException("L'état du personnage ne correspond pas au comportement !!");
            // rien à faire sinon puisqu'il est mort
        }
    }
}
