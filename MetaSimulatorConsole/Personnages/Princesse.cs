using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    class Princesse : Personnage
    {
        public Princesse(Organisation o,string sonNom) : base(o,"Princesse " + sonNom)
        {
            Son = new ComportementParlerPrincesse();
        }
        public override string Afficher()
        {
            return Nom;
        }

        public override string Combattre()
        {
            return ComportementCombat.NePasCombattre();
        }

        public override string EmettreUnSon()
        {
            if (Son != null) return Son.EmettreUnSon();
            else return ComportementEmettreUnSon.NePasEmettreDeSon();
        }

        public override string SeDeplacer()
        {
            return Deplacement.NePasSeDeplacer();
        }
    }
}
