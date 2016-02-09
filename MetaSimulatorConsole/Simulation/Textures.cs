using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MetaSimulatorConsole.Simulation
{
    public enum NomTexture
    {
        Herbe, Herbe2, Ground1, Pikachu,
        Bee, Beehive, Flower,
        Plane1, Plane2,
        Ground2,
        Mozaic1,
    }
    [XmlInclude(typeof(TextureDecorator))]
    [XmlInclude(typeof(TextureHerbe))]
    [XmlInclude(typeof(TextureHerbe2))]
    [XmlInclude(typeof(TextureGround1))]
    [XmlInclude(typeof(TextureGround2))]
    [XmlInclude(typeof(TextureMozaic1))]

    public abstract class Texture // Texture Unique
    {
        public abstract List<NomTexture> Name();
    }

    public abstract class TextureDecorator : Texture // Assemblage de textures
    {
        protected Texture Decor;

        public void SetTextureDeBase(Texture decor)
        {
            Decor = decor;
        }
        public TextureDecorator(Texture decor)
        {
            SetTextureDeBase(decor);
        }

        public override List<NomTexture> Name()
        {
            if (Decor == null) return null;
            return Decor.Name();
        }
    }

    public class TextureHerbe : Texture // "herbe" est une texture de base
    {
        public override List<NomTexture> Name()
        {
            return new List<NomTexture>() { NomTexture.Herbe };
        }
    }

    public class TextureHerbe2 : Texture
    {
        public override List<NomTexture> Name()
        {
            return new List<NomTexture>() { NomTexture.Herbe2 };
        }

    }

    public class TextureGround1 : Texture
    {
        public override List<NomTexture> Name()
        {
            return new List<NomTexture>() { NomTexture.Ground1 };
        }
    }

    public class TextureGround2 : Texture
    {
        public override List<NomTexture> Name()
        {
            return new List<NomTexture>() { NomTexture.Ground2 };
        }
    }

    public class TextureMozaic1 : Texture
    {
        public override List<NomTexture> Name()
        {
            return new List<NomTexture>() { NomTexture.Mozaic1 };
        }
    }
    public class TexturePikachu : TextureDecorator
    {
        public TexturePikachu(Texture decor) : base(decor) { }
        public TexturePikachu() : base(null) { }
        public override List<NomTexture> Name()
        {
            if (Decor == null)
                return new List<NomTexture>() { NomTexture.Pikachu };

            var textures = Decor.Name();
            textures.Add(NomTexture.Pikachu);
            return textures;
        }
    }

    public class TexturePikachuSurHerbe : TexturePikachu
    {
        public TexturePikachuSurHerbe() : base(new TextureHerbe()) { }
    }

    public class TextureBee : TextureDecorator
    {
        public TextureBee() : base(null) { }

        public TextureBee(Texture decor) : base(decor){}
        public override List<NomTexture> Name()
        {
            if (Decor == null)
                return new List<NomTexture>() { NomTexture.Bee };

            var textures = Decor.Name();
            textures.Add(NomTexture.Bee);
            return textures;
        }
    }

    public class TexturePlane1 : TextureDecorator
    {
        public TexturePlane1() : base(null) { }

        public TexturePlane1(Texture decor) : base(decor) { }
        public override List<NomTexture> Name()
        {
            if (Decor == null)
                return new List<NomTexture>() { NomTexture.Plane1 };

            var textures = Decor.Name();
            textures.Add(NomTexture.Plane1);
            return textures;
        }
    }
}
