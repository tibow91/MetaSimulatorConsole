using MetaSimulatorConsole.Simulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Gestion
{
    // ADAPTER
    public interface LoadGameAdapter
    {
        bool Load();
    }

    abstract class LoadGameAdapterAbstrait : LoadGameAdapter
    {
        protected readonly GameManager Gestionnaire;
        protected LoadGameAdapterAbstrait(GameManager manager)
        {
            Gestionnaire = manager;
        }
        public abstract bool Load();
        protected void TestInstance()
        {
            if (Gestionnaire == null) throw new NullReferenceException("Gestionnaire est null !");
        }
    }

    class LoadAOKAdapter : LoadGameAdapterAbstrait
    {
        public LoadAOKAdapter(GameManager manager) : base(manager)        {        }
        public override bool Load()
        {
            TestInstance();
           return Gestionnaire.chargerFichierXML(EGame.AgeOfKebab);
        }
    }

    class LoadCDGSimulatorAdapter : LoadGameAdapterAbstrait
    {
        public LoadCDGSimulatorAdapter(GameManager manager) : base(manager) { }

        public override bool Load()
        {
            TestInstance();
            return Gestionnaire.chargerFichierXML(EGame.CDGSimulator);
        }
    }

    class LoadHoneylandAdapter : LoadGameAdapterAbstrait
    {
        public LoadHoneylandAdapter(GameManager manager) : base(manager) { }

        public override bool Load()
        {
            TestInstance();
            return Gestionnaire.chargerFichierXML(EGame.Honeyland);
        }
    }

    // DESERIALIZATION
    interface LoadGameFromFile
    {
        void ChargerJeuDepuisXML();
    }
    abstract class LoadGameTemplateAdapterAbstrait
    {
        protected readonly GameManager Gestionnaire;

        protected readonly string NomFichier;
        protected void TestInstance()
        {
            if (Gestionnaire == null) throw new NullReferenceException("Gestionnaire est null !");
        }
        protected LoadGameTemplateAdapterAbstrait(GameManager manager,string filename)
        {
            Gestionnaire = manager;
            NomFichier = filename;
        }
        public bool ChargerJeuDepuisXML()
        {
            if(CheckSimulationRunning()) return false;
            if (!CheckFileExistence())  return false;
            var game = CastDeserialization();
            TestInstance();
            if (game != null)
            {
                Gestionnaire.Simulation = game;
                Gestionnaire.Simulation.RechargerLaPartie(Gestionnaire);
            }
            else throw new InvalidOperationException("Erreur rencontrée lors du chargement du jeu");
            return true;
        }

        protected virtual Game CastDeserialization()
        {
            return (Game)new GameSerializer().Deserialize(NomFichier);
        }

        private bool CheckSimulationRunning()
        {
            TestInstance();
            if (Gestionnaire.Simulation != null && Gestionnaire.Simulation.Running)
            {
                Console.WriteLine("Chargement du jeu impossible car une simulation est déjà en cours");
                return true;
            }
            return false;
        }
        private bool CheckFileExistence()
        {
            string path = @"C:\XML\" + NomFichier + ".xml";

            if (!File.Exists(path))
            {
                Console.WriteLine("Chargement du jeu AgeOfKebab Impossible car '" + NomFichier + ".xml : est inexistant");
                return false;
            }
            return true;
        }

    }


    class LoadGameAOK : LoadGameTemplateAdapterAbstrait
    {

        public LoadGameAOK(GameManager manager) : base(manager,"savegame_AgeOfKebab")        {        }
        protected override Game CastDeserialization()
        {
            return (GameAgeOfKebab)new GameSerializer().Deserialize(NomFichier);
        }
    }

    class LoadGameCDGSimulator : LoadGameTemplateAdapterAbstrait
    {

        public LoadGameCDGSimulator(GameManager manager) : base(manager, "savegame_CDGSimulator") { }

        protected override Game CastDeserialization()
        {
            return (GameCDGSimulator)new GameSerializer().Deserialize(NomFichier);
        }
    }

    class LoadGameHoneyland : LoadGameTemplateAdapterAbstrait
    {

        public LoadGameHoneyland(GameManager manager) : base(manager, "savegame_Honeyland") { }
        protected override Game CastDeserialization()
        {
            return (GameHoneyland)new GameSerializer().Deserialize(NomFichier);
        }
    }
}
