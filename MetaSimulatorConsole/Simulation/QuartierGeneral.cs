using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetaSimulatorConsole.Simulation
{
    public interface IPersonnageAMobiliser
    {
        void Mobiliser();
    }

    public abstract class QuartierGeneralAbstrait : IObservateurAbstrait
    {
        protected QuartierGeneralAbstrait() { }
        private List<IPersonnageAMobiliser> PersonnagesMobilises = new List<IPersonnageAMobiliser>();

        public void AttacherPersonnage(IPersonnageAMobiliser observer)
        {
            PersonnagesMobilises.Add(observer);
        }

        public void DeAttacherPersonnage(IPersonnageAMobiliser observer)
        {
            PersonnagesMobilises.Remove(observer);
        }

        public void MobiliserPersonnages()
        {
            foreach (IPersonnageAMobiliser o in PersonnagesMobilises)
            {
                o.Mobiliser();
            }
        }

        public List<IPersonnageAMobiliser> ObtenirPersonnageMobilises()
        {
            return PersonnagesMobilises;
        }

        public abstract void Update();
    }

    public class QuartierGeneralAOK : QuartierGeneralAbstrait
    {
        // Il doit prendre en charge une zone composite (Zone générale AOK de préférence)
        // Et charger automatiquement tous les personnages de type Client
        // Il faudra gérer également l'insertion des personnages dans la zone de jeu
        // Songer aussi à placer le module de statistique à l'intérieur
        private readonly  Game Simulation;
//        private Zone ZoneGenerale;
        public override void Update()
        {
//            if(Simulation != null) ZoneGenerale = (ZoneGeneraleAOK) Simulation.ZoneGenerale;
        }

        public QuartierGeneralAOK(Game simu)
        {
            Simulation = simu;
            Update(); // Pour mettre à jour la ZoneGénérale
        }
        
    }

}
