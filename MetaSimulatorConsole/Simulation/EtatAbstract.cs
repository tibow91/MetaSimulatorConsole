
namespace MetaSimulatorConsole.Simulation
{
    public abstract class EtatAbstract
    {
        public string Nom;

        public abstract string ModifieEtat(PersonnageAbstract p);
    }
}
