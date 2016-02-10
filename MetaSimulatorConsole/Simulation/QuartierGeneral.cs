using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MetaSimulatorConsole.Simulation.AgeOfKebab;

namespace MetaSimulatorConsole.Simulation
{
    public interface IPersonnageAMobiliser
    {
        void Mobiliser();
    }

    public abstract class QuartierGeneralObserve : IObservateurAbstrait
    {
        protected QuartierGeneralObserve() { }
        private List<IPersonnageAMobiliser> PersonnagesMobilises = new List<IPersonnageAMobiliser>();

        public void AttacherPersonnage(IPersonnageAMobiliser observer)
        {
            PersonnagesMobilises.Add(observer);
        }

        public void DeAttacherPersonnage(IPersonnageAMobiliser observer)
        {
            PersonnagesMobilises.Remove(observer);
        }

        public void DeAttacherTousLesPersonnagesMobilises()
        {
            PersonnagesMobilises.Clear();
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

    public abstract class QuartierGeneralAbstrait : QuartierGeneralObserve
    {
        // Il doit prendre en charge une zone composite (Zone générale de préférence)
        // Et charger automatiquement tous les personnages de type Client
        // Il faudra gérer également l'insertion des personnages dans la zone de jeu
        // Songer aussi à placer le module de statistique à l'intérieur
        private readonly  Game Simulation;
        protected ZoneGenerale ZonePrincipale;
        public override void Update()
        {
            DeAttacherTousLesPersonnagesMobilises();
            if(Simulation != null) ZonePrincipale = Simulation.ZoneGenerale;
            ChargerPersonnagesAMobiliser();
        }

        protected QuartierGeneralAbstrait(Game simu)
        {
            Simulation = simu;
            Update(); // Pour mettre à jour la ZoneGénérale
        }

        protected void ChargerPersonnagesAMobiliser()
        {
            if (ZonePrincipale == null) return;
            var list = ZonePrincipale.ObtenirPersonnages();
            foreach (var perso in list)
            {
                var observer = perso as IPersonnageAMobiliser;
                if (observer != null)
                {
                    AttacherPersonnage(observer);
                }
            }
        }

    }

    public class QuartierGeneralAOK : QuartierGeneralAbstrait
    {
        public QuartierGeneralAOK(Game simu) : base(simu)
        {
        }

    }

}
