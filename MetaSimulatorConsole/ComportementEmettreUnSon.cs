using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    abstract class ComportementEmettreUnSon
    {
        public abstract string EmettreUnSon();

        public static string NePasEmettreDeSon()
        {
            return "Je suis muet";
        }
    }

    class ComportementCrier : ComportementEmettreUnSon
    {
        public override string EmettreUnSon()
        {
            return "Je crie très fort";
        }
    }

    class ComportementParler : ComportementEmettreUnSon
    {
        public override string EmettreUnSon()
        {
            return "Je parle";
        }
    }

    class ComportementParlerPrincesse : ComportementEmettreUnSon
    {

        public override string EmettreUnSon()
        {
            return "Je parle comme une princesse";
        }
    }
}
