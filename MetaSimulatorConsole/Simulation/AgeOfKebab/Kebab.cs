using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{
    abstract class KebabIngredient   // Composite + Decorator
    {
        public string Nom;
        protected double Prix;
        public KebabIngredient(string unNom, double prix)
        {
             Nom = unNom;
            Prix = prix;
        }
 
        public virtual string Composition() { return Nom;}
        public virtual double Cout() { return Prix; }
    }


    class KebabDecorator : KebabIngredient // Decorator
    {
        protected KebabIngredient Ingredients;

        public KebabDecorator(string unNom, double prix, KebabIngredient ingredient)
            : base(unNom, prix)
        {
            Ingredients = ingredient;
        }

        public override string Composition()
        {
            return Ingredients.Composition() + " " + Nom;
        }

        public override double Cout()
        {
            return Prix + Ingredients.Cout();
        }
    }

    class Pain : KebabIngredient
    {
        public Pain() : base("Pain", 4.00f){}

        public Pain(string nom, double prix) : base("Pain " + nom, 4.00f + prix){}

    }

    class Salade : KebabDecorator
    {
        public Salade(KebabIngredient ingredient): base("Salade", 0.50f,ingredient){}
        public Salade(string nom, double prix,KebabIngredient ingredient)
            : base("Salade " + nom, 0.50f + prix,ingredient)
        { }
    }

    class Viande : KebabDecorator
    {
        public Viande(KebabIngredient ingredient) : base("Viande", 1.00f, ingredient)
        {
        }

        public Viande(string nom, double prix, KebabIngredient ingredient)
            : base("Viande " + nom, 1.00f + prix,ingredient)
        { }
    }

    
}
