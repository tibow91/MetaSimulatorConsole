using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{

    abstract class EtatClient : EtatAbstract
    {
        public abstract override string ModifieEtat(PersonnageAbstract p);
        public abstract string AfficherEtat();
        protected EtatClient(string nom) : base(nom)
        {
        }
    }
    class EtatEnAttenteDeFaim : EtatClient
    {

        public override string AfficherEtat()
        {
            return "En Attente de Faim";
        }

        public override string ModifieEtat(PersonnageAbstract p)
        {
            throw new NotImplementedException();
        }

        public EtatEnAttenteDeFaim(string nom) : base(nom)
        {
        }
    }

    class EtatVaCommander : EtatClient
    {

        public override string AfficherEtat()
        {
            return "Va Passer une commande";
        }

        public override string ModifieEtat(PersonnageAbstract p)
        {
            throw new NotImplementedException();
        }

        public EtatVaCommander(string nom) : base(nom)
        {
        }
    }

    class EtatEnAttenteDeCommande : EtatClient
    {

        public override string ModifieEtat(PersonnageAbstract p)
        {
            throw new NotImplementedException();
        }

        public override string AfficherEtat()
        {
            return "En attente pour passer une commande";
        }

        public EtatEnAttenteDeCommande(string nom) : base(nom)
        {
        }
    }

    class EtatEstServi : EtatClient
    {

        public override string ModifieEtat(PersonnageAbstract p)
        {
            throw new NotImplementedException();
        }

        public override string AfficherEtat()
        {
            return "Est Servi";
        }

        public EtatEstServi(string nom) : base(nom)
        {
        }
    }
}
