using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    class Archer : OldPersonnage
    {
        public Archer(Organisation organisation, String unNom) : base(organisation,"Archer " + unNom)
        {
            Comportement = new ComportementAvecArc();
            Son = new ComportementCrier();      
        }
        public override string Afficher()
        {
            return Nom;
        }

        public override String Combattre()
        {
            if(Comportement != null)
                return Comportement.Combattre();
            else
                return Comportement.Combattre();

        }

        public override String EmettreUnSon()
        {
            if (Son != null)
                return Son.EmettreUnSon();

            else            
               return ComportementEmettreUnSon.NePasEmettreDeSon();
            
        }

        public override string SeDeplacer()
        {
            if (Deplacement != null) return Deplacement.SeDeplacer();
            else return Deplacement.NePasSeDeplacer();
        }
    }
}
