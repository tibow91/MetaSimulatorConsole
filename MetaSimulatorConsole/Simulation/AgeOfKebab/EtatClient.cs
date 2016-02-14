using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{
    [XmlInclude(typeof(EtatClientEnAttenteDeCommande)), XmlInclude(typeof(EtatClientEnAttenteDeFaim))]
    [XmlInclude(typeof(EtatClientVaCommander)), XmlInclude(typeof(EtatClientEstServi))]
    public abstract class EtatClient : EtatAbstract
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
            return "Etat " + Nom;
        }

        public EtatClient() : base("Etat Client non défini") { }
        protected EtatClient(string nom) : base(nom) { }
    }
    public class EtatClientEnAttenteDeFaim : EtatClient
    {
        public override void ModifieEtat(Client p)
        {
            if (p == null) throw new NullReferenceException("Personnage is null !");
            if (p.Comportement is ComportementEnAttenteDeFaim) return;
            p.Comportement = new ComportementEnAttenteDeFaim(p);
//            Console.WriteLine("Le comportement En attente de faim du personnage a été chargé");
        }

        public EtatClientEnAttenteDeFaim() : base("En Attente de faim") { }

    }

    public class EtatClientVaCommander : EtatClient
    {
        public override void ModifieEtat(Client p)
        {
            if (p == null) throw new NullReferenceException("Personnage is null !");
            if (p.Comportement is ComportementEnAttenteDeFaim) return;
            p.Comportement = new ComportementEnAttenteDeFaim(p);
        }
        public EtatClientVaCommander() : base("Va Passer une commande") { }

    }

    public class EtatClientEnAttenteDeCommande : EtatClient
    {
        public override void ModifieEtat(Client p)
        {
            throw new NotImplementedException();
        }
        public EtatClientEnAttenteDeCommande() : base("En attente pour passer une commande") { }

    }

    public class EtatClientEstServi : EtatClient
    {
        public override void ModifieEtat(Client p)
        {
            throw new NotImplementedException();
        }
        public EtatClientEstServi() : base("Est Servi") { }
    }
}
