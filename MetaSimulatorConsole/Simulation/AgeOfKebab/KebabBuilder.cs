using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation.AgeOfKebab
{
    internal abstract class KebabBuilderAbstrait
    {
        public abstract void AjouterBasePain();
        public abstract void AjouterIngredientPrincipal();
        public abstract void AjouterGarnitures();
        public abstract KebabIngredient RecupererKebab();
    }
    class KebabBuilderSimple : KebabBuilderAbstrait
    {
        protected KebabIngredient Kebab;
        public override void AjouterBasePain()
        {
            Kebab = new Pain();
        }

        public override void AjouterIngredientPrincipal()
        {
            Kebab = new Viande(Kebab);
        }

        public override void AjouterGarnitures()
        {
            Kebab = new Salade(Kebab);
        }

        public override KebabIngredient RecupererKebab()
        {
            return Kebab;
        }
    }

    class KebabDirecteur
    {
        public void Construire(KebabBuilderAbstrait kebab)
        {
            kebab.AjouterBasePain();
            kebab.AjouterIngredientPrincipal();
            kebab.AjouterGarnitures();
        }
    }
}
