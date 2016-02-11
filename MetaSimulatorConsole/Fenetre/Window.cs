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

namespace MetaSimulatorConsole
{

    public class Window : GameWindow,IObservateurAbstrait
    {
        private Graphisme Graphismes;
        public Clavier Touches;
        private Grille tableau;
        public GameManager Gestionnaire;
        public bool ShowInterface = true;

        public Dictionary<string, string> TextMenu = new Dictionary<string, string>();

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
            List<IObservateurAbstrait> observers = new List<IObservateurAbstrait>();
            foreach (var o in Touches.Observers())
            {
                observers.Add(o);
            }
            return observers;
        }

        public void Update()
        {
            Tableau = Gestionnaire.TableauDeJeu;
        }
    }

}