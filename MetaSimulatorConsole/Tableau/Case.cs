using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using MetaSimulatorConsole.Simulation;

namespace MetaSimulatorConsole
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

    public class TextureMozaic1: Texture
    {
        public override List<NomTexture> Name()
        {
            return new List<NomTexture>() { NomTexture.Mozaic1 };
        }
    }
    public class TexturePikachu : TextureDecorator
    {
        public TexturePikachu(Texture decor) : base(decor){}
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

    public class Case : IObservateurAbstrait
    {
        public Texture Textures;
        private Texture ZoneTexture;
        private TextureDecorator ObjectTexture,PersonnageTexture;
        private ZoneFinale ZoneToObserve;
        private ObjetAbstrait ObjectToObserve;
        private PersonnageAbstract PersonnageToObserve;

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

        public void SetZoneToObserve(ZoneFinale zone)
        {
            ZoneToObserve = zone;
            Update();
        }

        public void SetObjectToObserve(ObjetAbstrait obj)
        {
            ObjectToObserve = obj;
            Update();
        }

        public void SetPersonnageToObserve(PersonnageAbstract perso)
        {
            PersonnageToObserve = perso;
            Update();
        }

        public override void Update()
        {
            if (ZoneToObserve != null)  ZoneTexture = ZoneToObserve.Texture;
            if (ObjectToObserve != null) ObjectTexture = ObjectToObserve.Texture;
            if (PersonnageToObserve != null) PersonnageTexture = PersonnageToObserve.Texture;

            if (PersonnageTexture == null)
            {
                // BASE
                if (ObjectTexture == null) SetTextures(ZoneTexture);
                else
                {
                    // OBJET + BASE
                    ObjectTexture.SetTextureDeBase(ZoneTexture);
                    this.SetTextures(ObjectTexture);
                }
            }
            else
            {
                if (ObjectTexture == null)
                {
                    // PERSO + BASE
                    PersonnageTexture.SetTextureDeBase(ZoneTexture);
                    this.SetTextures(PersonnageTexture);
                }
                else
                {
                    // PERSO + OBJET + BASE
                    ObjectTexture.SetTextureDeBase(ZoneTexture);
                    PersonnageTexture.SetTextureDeBase(ObjectTexture);
                    this.SetTextures(PersonnageTexture);
                }
            }
   
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
