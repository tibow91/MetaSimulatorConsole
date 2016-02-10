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

    [XmlInclude(typeof(QuartierGeneralAOK))]
    public abstract class QuartierGeneralAbstrait : QuartierGeneralObserve
    {
        // Songer aussi à placer le module de statistique à l'intérieur
        private readonly  Game Simulation;
        protected ZoneGenerale ZonePrincipale;
        public int PersonnagesToInsert { get; set; }
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

        public void GererUnTour()
        {
            MobiliserPersonnages();
            InsererPersonnagesRestants();
        }

        protected abstract void InsererPersonnagesRestants();

    }

    public class QuartierGeneralAOK : QuartierGeneralAbstrait
    {
        public QuartierGeneralAOK(Game simu) : base(simu)
        {
        }


        protected override void InsererPersonnagesRestants()
        {
            throw new NotImplementedException();
        }
    }

}
