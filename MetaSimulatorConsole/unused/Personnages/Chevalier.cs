using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    class Chevalier : OldPersonnage
    {
        public Chevalier(Organisation o,string sonNom)
            : base(o,"Chevalier " + sonNom)
        {
            Comportement = new ComportementACheval();
            Son = new ComportementParler();
        }
        public override string Afficher()
        {
            return Nom;
        }

        public override string Combattre()
        {
            if (Comportement != null) return Comportement.Combattre();
            else return ComportementCombat.NePasCombattre();
        }

        public override string EmettreUnSon()
        {
            if (Son != null) return Son.EmettreUnSon();
            else return ComportementEmettreUnSon.NePasEmettreDeSon();
        }

        public override string SeDeplacer()
        {
            if (Deplacement != null) return Deplacement.SeDeplacer();
            else return Deplacement.NePasSeDeplacer();
        }
    }
}
