using System;

namespace MetaSimulatorConsole.Simulation
{
    public abstract class PersonnageAbstract : SujetObserveAbstrait, IEquatable<PersonnageAbstract>
    {
        public EGame Simulation;
        public string Nom { get; set; }
        protected int PointsDeVie { get; set; }
        protected int SeuilCritique { get; set; }
        public EtatAbstract Etat { get; set; }

        public Coordonnees Case { get; set; }
        protected PersonnageAbstract(EGame simulation)
        {
            Simulation = simulation;
            Nom = "Personnage Sans nom";
            PointsDeVie = 100;
            SeuilCritique = 50;
            Etat = null;
        }

        public void BaisserPointsDeVie()
        {
            if(PointsDeVie > 0) --PointsDeVie;
        }

        public abstract void AnalyserSituation();
        public abstract void Execution();

        public bool Equals(PersonnageAbstract other)
        {
            if (other == null) return false;

            if (Simulation == other.Simulation) // Même Simulation
            {
                if (Case.Equals(other.Case))
                {
                    Console.WriteLine("Equals: Jeu " + Simulation + " Le personnage " + this + " et le personnage " + other +
                                      " partagent les mêmes coordonnées " + Case);
                    return true;
                }
            }
            return false;
        }
        
        public abstract void Ajoute(PersonnageAbstract c);
        public abstract void Retire(PersonnageAbstract c);
        public abstract bool EstValide();
        public override string ToString()
        {
            return "Personnage " + Nom + ", " + PointsDeVie + " XP, Etat " + Etat + " " + Case + " (Jeu " + Simulation.ToString() +")";
        }
    }
}
