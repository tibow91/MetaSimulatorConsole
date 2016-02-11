using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{

    abstract class EtatClient : EtatAbstract
    {
        public override void ModifieEtat(PersonnageMobilisable p)
        {
            if (p is Client)
            {
                ModifieEtat((Client)p);
            }
            else throw new InvalidCastException("Le personnage à modifier n'est pas de type client");

        }
        public abstract void ModifieEtat(Client p);

        public virtual string AfficherEtat()
        {
            return "Etat " + nom;
        }
        protected EtatClient(string nom) : base(nom)
        {
        }

        public string nom { get; set; }
    }
    class EtatClientEnAttenteDeFaim : EtatClient
    {

        public override void ModifieEtat(Client p)
        {
            if (p == null) throw new NullReferenceException("Personnage is null !");
            if (p.Comportement is ComportementEnAttenteDeFaim) return;
            p.Comportement = new ComportementEnAttenteDeFaim(p);
        }

        public EtatClientEnAttenteDeFaim()
            : base("En Attente de Faim")
        {
        }
    }

    class EtatClientVaCommander : EtatClient
    {



        public EtatClientVaCommander()
            : base("Va Passer une commande")
        {
        }

        public override void ModifieEtat(Client p)
        {
            if (p == null) throw new NullReferenceException("Personnage is null !");
            if (p.Comportement is ComportementEnAttenteDeFaim) return;
            p.Comportement = new ComportementEnAttenteDeFaim(p);
        }
    }

    class EtatClientEnAttenteDeCommande : EtatClient
    {


        public EtatClientEnAttenteDeCommande()
            : base("En attente pour passer une commande")
        {
        }

        public override void ModifieEtat(Client p)
        {
            throw new NotImplementedException();
        }
    }

    class EtatClientEstServi : EtatClient
    {


        public EtatClientEstServi()
            : base("Est Servi")
        {
        }

        public override void ModifieEtat(Client p)
        {
            throw new NotImplementedException();
        }
    }
}
