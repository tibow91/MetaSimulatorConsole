using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using MetaSimulatorConsole.Fenetre;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using MetaSimulatorConsole.Simulation;

namespace MetaSimulatorConsole
{

    public class Window : GameWindow,IObservateurAbstrait
    {
        private Graphisme Graphismes;
        public Clavier Touches;
        private Grille tableau;
        public GameManager Gestionnaire;
        public bool ShowInterface = true,ShowStats;
        public QuartierGeneralAbstrait Stats;
        public Dictionary<string, string> TextMenu = new Dictionary<string, string>();
        public Dictionary<string, int> TextMenuStats = new Dictionary<string, int>();

        private Menu _etatMenu;
        private Menu EtatMenu
        {
            get{ return _etatMenu;}
            set
            {
                _etatMenu = value;
                if (_etatMenu is MenuSimulation)
                    printStats = true;                
                else
                    printStats = false;
            }
        }
        public bool printStats;
        public Grille Tableau
        {
            get { return tableau; }
            set { tableau = value; }
        }

        public Window(int width, int height, GameManager manager)
            : base(width, height)
        {
            Gestionnaire = manager;
            manager.Attach(this);
            tableau = Gestionnaire.TableauDeJeu;
            Touches = new Clavier(this);
            Graphismes = new Graphisme(this);
            //this.
            //Run(60.0);  // Run the game at 60 updates per second

        }

        public void InitRender(object sender, FrameEventArgs e)
        {
            // render graphics
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1, 1, -1, 1, 0.0, 0);

            //Matrix4.LookAt()
        }

        public void SwapBuffers(object sender, FrameEventArgs e)
        {
            SwapBuffers();
        }

        public List<IObservateurAbstrait> Observers()
        {
            Console.WriteLine("Mise à jour des commandes de jeu");
            List<IObservateurAbstrait> observers = new List<IObservateurAbstrait>();
            foreach (var o in Touches.Observers())
            {
                observers.Add(o);
            }
            return observers;
        }

        public void Update()
        {
            if (Gestionnaire == null) throw new NullReferenceException("Le gestionnaire est null !");
            Tableau = Gestionnaire.TableauDeJeu;
            EtatMenu = Gestionnaire.MenuCourant;
            if (Gestionnaire.Simulation == null) Stats = null;
            Stats = Gestionnaire.Simulation.QG;
            UpdateStatsOnScreen();
        }

        private void UpdateStatsOnScreen()
        {
            TextMenuStats.Clear();
            if (!printStats) return;
            if (Stats == null) throw new NullReferenceException("Pas de stats à afficher car l'objet est null !");
            TextMenuStats.Add("Tour", Stats.Tour);
            TextMenuStats.Add("Personnages insérés", Stats.PersonnagesInseres);
            TextMenuStats.Add("Personnages à insérer", Stats.PersonnagesToInsert);
            TextMenuStats.Add("Personnages ayant atteint leur objectif", Stats.PersonnagesToInsert);
        }
    }

}