using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    abstract class ComportementCombat
    {
        public abstract String Combattre();

        public static String NePasCombattre()
        {
            return "Je ne sais pas combattre";
        }
    }

    class ComportementApiedAvecHache : ComportementCombat
    {
        public override String Combattre()
        {
            return "Je combats avec une Hache";
        }
    }

    class ComportementAvecArc : ComportementCombat
    {
        public override String Combattre()
        {
            return "Je combats avec un Arc";
        }
    }

    class ComportementACheval : ComportementCombat
    {
        public override string Combattre()
        {
            return "Je combat à cheval";
        }
    }
}
