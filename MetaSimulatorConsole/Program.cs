using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //new SimulationJeu();
            var manager = new GameManager();
            //manager.CreerNouveauJeu();
            //Manager.CreerNouveauJeu();
            //Manager.ChoisirJeu(NomJeu.CDGSimulator);
            //Manager.CreerNouveauJeu();
            //var tableau = Manager.TableauDeJeu;
            //var path = tableau.Route(tableau[0, 0], tableau[4, 4]);
            //foreach (Vertex elem in path)
            //{
            //    Console.WriteLine(elem.ToString());
            //}
            //Thread workerThread = new Thread(Manager.Simulation.LancerSimulation);
            //workerThread.Start();
            manager.Fenetre.Run();
          // https://github.com/tibow91/MetaSimulatorConsole.git
        }
    }
}
