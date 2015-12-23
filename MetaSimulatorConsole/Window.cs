using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace MetaSimulatorConsole
{
    class Graphisme
    {
        private Window Partie;
        private Dictionary<NomTexture, int> Textures = new Dictionary<NomTexture, int>();

        public Graphisme(Window jeu)
        {
            this.Partie = jeu;
            InitialiserComposants();
        }

        private void InitialiserComposants()
        {
            Partie.Load += (sender, e) =>
            {
                // setup settings, load textures, sounds
                Partie.VSync = VSyncMode.On;
                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                ChargerTextures();
            };

            Partie.Resize += (sender, e) =>
            {
                GL.Viewport(0, 0, Partie.Width, Partie.Height);
            };


            Partie.RenderFrame += Partie.InitRender;
            Partie.RenderFrame += RenderQuads;
            Partie.RenderFrame += Partie.SwapBuffers;
        }

        public void RenderQuads(object sender, FrameEventArgs e)
        {
            int width = Partie.Tableau.Largeur;
            int height = Partie.Tableau.Longueur;
            float unitWidth = 2f/width;
            float unitHeight = 2f / height;
            var it = Partie.Tableau.CreateIterator();
            for (float i = 1f; i > -1f; i-=unitHeight,it.Bas())
            {
                var it2 = it.Clone(); 
                for (float j = -1f; j < 1f; j+=unitWidth,it2.Droite())
                {
                    var node = (Node<Case>) it2.CurrentItem();
                    if (node != null)
                    {
                        var textures = node.Value.Textures.Name();
                        foreach (var texture in textures)
                        {
                            AfficherQuad(j, i, unitHeight, unitWidth, Textures[texture]);
                        }
                        //AfficherQuad(j,i, unitHeight,unitWidth, node.Value.Textures);
                    }
                    else{} // Afficher Texture vide
                }
            }

            //float x1 = -1f, x2 = 1f, y1 = 0.5f, y2 = 0.5f;
            //AfficherTriangle(-1f, 1f, 0.5f, 0.5f, Partie.Couleur);
            //AfficherQuad(x1, x2, y1, y2, Textures["herbe"]);
            //AfficherQuad( x1 +y2 , x2 , y1, y2, Textures["pika_front"]);
        }

        private void AfficherTriangle(float posX, float posY, float height, float width, Color4 couleur)
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color4(couleur);
            //GL.Vertex2(-0.5f, 0.5f);
            //GL.Color4(couleur);
            //GL.Vertex2(0.0f, -0.5f);
            //GL.Color4(couleur);
            //GL.Vertex2(0.5f, 0.5f);
            GL.Vertex2(posX, posY);
            GL.Color4(couleur);
            GL.Vertex2(posX + width / 2, posY - height);
            GL.Color4(couleur);
            GL.Vertex2(posX + width, posY);
            GL.End();
        }

        private void AfficherQuad(float posX, float posY, float height, float width, int texture)
        {
            //GL.Color3(Color.Red);       //  <------- The lack of this line might be causing it to turn red!
            GL.BindTexture(TextureTarget.Texture2D, texture);
            //GL.Enable(EnableCap.Texture2D);

            GL.Begin(PrimitiveType.Quads);
            //GL.Color4(CouleurVerte);
            //GL.Vertex2(-0.5f, 0.5f);
            //GL.Color4(couleur);
            //GL.Vertex2(0.0f, -0.5f);
            //GL.Color4(couleur);
            //GL.Vertex2(0.5f, 0.5f);
            GL.TexCoord2(1, 0); GL.Vertex2(posX, posY);
            //GL.Color4(Couleur);
            GL.TexCoord2(0, 0); GL.Vertex2(posX + width, posY);
            //GL.Color4(Couleur);
            GL.TexCoord2(0, 1); GL.Vertex2(posX + width, posY - height);
            //GL.Color4(CouleurVerte);
            GL.TexCoord2(1, 1); GL.Vertex2(posX, posY - height);
            GL.End();
        }

        private int ChargerTexture(string filename)
        {
            if (filename == null) throw new ArgumentNullException("Nom de fichier null");
            if (filename.Equals("")) throw new ArgumentException("Chaîne de caractère du nom de fichier vide", "filename");
            if (!File.Exists(filename)) throw new FileNotFoundException("Fichier de texture introuvable");

            Bitmap bitmap = new Bitmap(filename);

            int textureId;
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out textureId);
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);


            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            return textureId;
        }

        public void ChargerTextures()
        {
            Textures.Add(NomTexture.Herbe, ChargerTexture("../res/grass.png"));
            Textures.Add(NomTexture.Pikachu, ChargerTexture("../res/m_front.png"));
        }
    }


    class ActionGraphique
    {
        public static void ActiverTranslation(float decX, float decY, float decZ)
        {
            Matrix4 TransMatrix = Matrix4.CreateTranslation(new Vector3(decX, decY, decZ));
            GL.LoadMatrix(ref TransMatrix);
        }
    }

    class Clavier
    {
        private Window Partie;
        private AppuiClavier CommandesClavier;
        private AppuiClavier CommandesSpeciales;

        public Clavier(Window jeu)
        {
            this.Partie = jeu;
            CommandesClavier = AppuiClavier.ChaineDeCommandes(Partie);
            CommandesSpeciales = AppuiClavier.ChaineDeCommandesSpeciales(Partie);
            ChargerConfiguration();
        }
        public void ChargerConfiguration()
        {
            Partie.KeyPress += RetroactionTouches;
            Partie.UpdateFrame += RetroactionTouchesSpeciales;
        }
        private void RetroactionTouches(object sender, KeyPressEventArgs e)
        {
            if(CommandesClavier != null)
                CommandesClavier.Traitement();
        }
        private void RetroactionTouchesSpeciales(object sender, FrameEventArgs e)
        {
            if (CommandesSpeciales != null)
                CommandesSpeciales.Traitement();
        }
    }
    class Window : GameWindow
    {
        private Graphisme Graphismes;
        public Clavier Touches;
        private Grille tableau;
        public GameManager Gestionnaire;

        public Grille Tableau
        {
           get { return tableau; }
        }

        public Window(int width, int height, GameManager manager)
            : base(width, height)
        {
            Gestionnaire = manager;
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
    }

}