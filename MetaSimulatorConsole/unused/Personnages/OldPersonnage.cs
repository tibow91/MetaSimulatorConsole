using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{

    abstract class OldPersonnage : IObservateurAbstrait
    {
        protected ComportementCombat Comportement = null;
        protected ComportementEmettreUnSon Son = null;
        protected Deplacement Deplacement = null;

        protected String Nom { get; set; }
        protected eMode EtatFonctionnement { get; set; }
        private Organisation Observer = null;
        public abstract String Afficher();
        public abstract String Combattre();
        public abstract String EmettreUnSon();

        public abstract String SeDeplacer();

        public OldPersonnage(Organisation _observer,String nom)
        {
            Observer = _observer;
            Nom = nom;
        }

        public void ModifierComportementCombat(ComportementCombat comportement)
        {
            Comportement = comportement;
        }
        
        public void ModifierComportementEmettreUnSon(ComportementEmettreUnSon son)
        {
            Son = son;
        }

        public void Update()
        {
            if (Observer != null) EtatFonctionnement = Observer.ModeFonctionnement;
            Console.WriteLine("Observeur {0} : nouvel Etat est {1}", Nom, EtatFonctionnement);
        }

        public String AfficherModeFonctionnement() { return "ModeFonctionnement = " + EtatFonctionnement; }

    }

}
