using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using MetaSimulatorConsole.Simulation;

namespace MetaSimulatorConsole
{
    
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
            UpdateDataFromPersonnage();
        }

        public virtual void SetObjectToObserve(ObjetAbstrait obj)
        {
            ObjectToObserve = obj;
            UpdateDataFromPersonnage();
        }

        public virtual void SetPersonnageToObserve(PersonnageAbstract perso)
        {
            PersonnageToObserve = perso;
            UpdateDataFromPersonnage();
        }

        public void UpdateDataFromPersonnage()
        {
            if (ZoneToObserve != null) ZoneTexture = ZoneToObserve.Texture;
            else ZoneTexture = null;
            
            if (ObjectToObserve != null) ObjectTexture = ObjectToObserve.Texture;
            else ObjectTexture = null;

            if (PersonnageToObserve != null) PersonnageTexture = PersonnageToObserve.Texture;
            else PersonnageTexture = null;

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
