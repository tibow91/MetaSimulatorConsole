using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{
    public class Client: PersonnageAbstract,IPersonnageAMobiliser
    {
        public Client(string nom,Coordonnees coor)
            : base(nom, EGame.AgeOfKebab, new TexturePlayer())
        {
            SetCoordonnees(coor);
        }

        public override void AnalyserSituation()
        {
            throw new NotImplementedException();
        }

        public override void Execution()
        {
            throw new NotImplementedException();
        }

        public override void Ajoute(PersonnageAbstract c)
        {
            throw new NotImplementedException();
        }

        public override void Retire(PersonnageAbstract c)
        {
            throw new NotImplementedException();
        }

        public Coordonnees AtteindreGatherPoint(ZoneFinale zone)
        {
            return null;
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

        public void Mobiliser()
        {
            throw new NotImplementedException();
        }
    }
}
