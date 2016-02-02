
namespace MetaSimulatorConsole.Simulation
{
    public abstract class EtatAbstract
    {
        public string Nom;
        protected EtatAbstract(string nom)
        {
            Nom = nom;
        }
        public abstract string ModifieEtat(PersonnageAbstract p);
        public override string ToString()
        {
            return Nom;
        }
    }
}
