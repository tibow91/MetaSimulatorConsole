using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    class Fantassin : OldPersonnage
    {

        public Fantassin(Organisation organisation, string unNom)
            : base(organisation,"Fantassin " + unNom)
        {
            Comportement = new ComportementApiedAvecHache();
            Son = new ComportementCrier();
        }

        public override string Afficher()
        {
            return Nom;
        }

        public override String Combattre()
        {
            if (Comportement != null)
                return Comportement.Combattre();
            else
                return ComportementCombat.NePasCombattre();
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
            else
                return Deplacement.NePasSeDeplacer();
        }
    }
}
