using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Gestion
{

    abstract class SaveGame 
    {
        protected GameManager Gestionnaire;
        private readonly string NomFichier;
        protected SaveGame(GameManager manager,string filename)
        {
            Gestionnaire = manager;
            NomFichier = filename;
        }
        public void Sauvegarder()
        {
            GameManager.TestInstance();
            if(Gestionnaire.Simulation == null) throw new NullReferenceException("Pas de simulation chargée !");
            new GameSerializer().Serialize(Gestionnaire.Simulation,NomFichier);
        }
    }

    class SaveGameAOK : SaveGame
    {
         public SaveGameAOK(GameManager manager) : base(manager,"savegame_AgeOfKebab"){}

    }

    class SaveGameCDGSimulator : SaveGame
    {
        public SaveGameCDGSimulator(GameManager manager) : base(manager, "savegame_CDGSimulator") { }
    }

    class SaveGameHoneyland : SaveGame
    {
        public SaveGameHoneyland(GameManager manager) : base(manager, "savegame_Honeyland") { }
    }
}
