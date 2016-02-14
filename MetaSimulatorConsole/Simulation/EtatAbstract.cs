
using MetaSimulatorConsole.Simulation.AgeOfKebab;
using System.Xml.Serialization;
namespace MetaSimulatorConsole.Simulation
{
    [XmlInclude(typeof(EtatClient))]
    public abstract class EtatAbstract
    {
        [XmlIgnore]
        public string Nom;
        public EtatAbstract() { }
        protected EtatAbstract(string nom)
        {
            Nom = nom;
        }
        public abstract void ModifieEtat(PersonnageMobilisable p);
        public override string ToString()
        {
            return Nom;
        }
    }
}
