
namespace MetaSimulatorConsole.Simulation
{
    public abstract class EtatAbstract
    {
        public string Nom;
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
