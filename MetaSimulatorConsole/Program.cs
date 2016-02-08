using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MetaSimulatorConsole.Simulation.AgeOfKebab;

namespace MetaSimulatorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //new SimulationJeu();
            var manager = GameManager.Instance();
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
            var serveur = new KebabDirecteur();
            var menu = new KebabBuilderSimple();
            serveur.Construire(menu);
            var kebab = menu.RecupererKebab();
            Console.WriteLine(kebab.Composition());
            Console.WriteLine("prix = " + kebab.Cout());
            new CoordonneesSerializer().Serialize(new Coordonnees(5, 5), "Coordonnées");
            Coordonnees coor = (Coordonnees) new CoordonneesSerializer().Deserialize("Coordonnées");
            Console.WriteLine("Coordonnées deserializées = " + coor);


            manager.Fenetre.Run();
          // https://github.com/tibow91/MetaSimulatorConsole.git
        }
    }
}
