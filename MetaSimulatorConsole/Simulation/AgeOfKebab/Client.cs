using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{
    public class Client : PersonnageAbstract
    {
        public Client(string nom) : base(nom, EGame.AgeOfKebab, new TexturePlayer())
        {
        }

        public override void AnalyserSituation()
        {
            throw new NotImplementedException();
        }

        public override void Execution()
        {
            throw new NotImplementedException();
        }

        public override void Ajoute(PersonnageAbstract c)
        {
            throw new NotImplementedException();
        }

        public override void Retire(PersonnageAbstract c)
        {
            throw new NotImplementedException();
        }



    }
}
