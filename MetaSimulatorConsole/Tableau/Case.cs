using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MetaSimulatorConsole.Simulation;

namespace MetaSimulatorConsole
{
    enum NomTexture { Herbe,Pikachu }

    abstract class Texture // Texture Unique
    {
        public abstract List<NomTexture> Name();
    }

    abstract class TextureDecorator : Texture // Assemblage de textures
    {
        protected Texture Decor;
        public void SetTexture(Texture decor)
        {
            Decor = decor;
        }
        public TextureDecorator(Texture decor)
        {
            SetTexture(decor);
        }

        public override List<NomTexture> Name()
        {
            if (Decor == null) return null;
            return Decor.Name();
        }
    }

    class TextureHerbe : Texture // "herbe" est une texture de base
    {
        public override List<NomTexture> Name()
        {
            return new List<NomTexture>() { NomTexture.Herbe };
        }
    }

    class TexturePikachu : TextureDecorator
    {
        public TexturePikachu(Texture decor) : base(decor){}
        public TexturePikachu() : base(null) { }
        public override List<NomTexture> Name()
        {
            if (Decor != null)
            {
                var textures = Decor.Name();
                textures.Add(NomTexture.Pikachu);
                return textures;
            }
            else return new List<NomTexture>(){ NomTexture.Pikachu};
        }
    }

    class TexturePikachuSurHerbe : TexturePikachu
    {
        public TexturePikachuSurHerbe() : base(new TextureHerbe()) { }
    }
    class Case 
    {
        public Texture Textures;

        public Case()
        {
            Textures = new TextureHerbe();
        }
        public Case(Texture textures)
        {
            if (textures != null) Textures = textures;
            else  Textures = new TextureHerbe();
        }
        public void SetTextures(Texture decor)
        {
            Textures = decor;
        }
    }

    abstract class CaseFactory
    {
        public abstract Case CreerCase();
    }

    class CaseAgeOfKebabFactory : CaseFactory
    {
        public override Case CreerCase()
        {
            return new CaseAgeOfKebab();
        }
    }

    class CaseCDGSimulatorFactory : CaseFactory
    {
        public override Case CreerCase()
        {
            return new CaseCDGSimulator();
        }
    }

    class CaseHoneylandFactory : CaseFactory
    {
        public override Case CreerCase()
        {
            return new CaseHoneyland();
        }
    }

}
