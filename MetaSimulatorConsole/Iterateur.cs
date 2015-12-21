using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole
{

    internal abstract class IIterateur
    {
        public abstract object First();

        public abstract object Haut();
        public abstract object Bas();
        public abstract object Gauche();
        public abstract object Droite();

        public abstract object CurrentItem();
    }

    class Iterateur<T> : IIterateur
    {
        private Conteneur<T> container;
        private int X = 0,Y = 0;

        public Iterateur(Conteneur<T> conteneur)
        {
            this.container = conteneur;             
        }

        public override T First()
        {
            return container[0, 0];
        }
        public override T Haut()
        {
            return container[X,++Y];
        }

        public override object Bas()
        {
            return container[X,--Y];
        }

        public override object Gauche()
        {
            return container[--X, Y];
        }

        public override object Droite()
        {
            return container[++X, Y];
        }

        
        public override object CurrentItem()
        {
            return container[X, Y];
        }
    }

}
