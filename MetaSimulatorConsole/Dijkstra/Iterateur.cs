using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaSimulatorConsole.Dijkstra;

namespace MetaSimulatorConsole
{

    internal abstract class IIterateur
    {
        public abstract object First();
        public abstract object Haut();
        public abstract object Bas();
        public abstract object Gauche();
        public abstract object Droite();
        public abstract IIterateur ItGauche();
        public abstract IIterateur ItDroite();
        public abstract IIterateur ItHaut();
        public abstract IIterateur ItBas();

        public abstract object CurrentItem();
        public abstract IIterateur Clone();
    }

    class Iterateur<T> : IIterateur
    {
        private Conteneur<T> container;
        private int X = 0,Y = 0;

        public Iterateur(Conteneur<T> conteneur)
        {
            container = conteneur;             
        }

        public override object First()
        {
            X = Y = 0;
            return container[0, 0];
        }
        public override object Haut()
        {
            return container[X,--Y];
        }

        public override object Bas()
        {
            return container[X,++Y];
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

        public override IIterateur Clone()
        {
            var it = new Iterateur<T>(container);
            it.X = X;
            it.Y = Y;
            return it;
        }

        public override string ToString()
        {
            return X.ToString() + "-" + Y.ToString();
        }
        
        public override IIterateur ItGauche()
        {
            var it = new Iterateur<T>(container);
            it.X = X - 1;
            it.Y = Y;
            return it;
        }

        public override IIterateur ItDroite()
        {
            var it = new Iterateur<T>(container);
            it.X = X+1;
            it.Y = Y;
            return it;
        }

        public override IIterateur ItHaut()
        {
            var it = new Iterateur<T>(container);
            it.X = X;
            it.Y = Y - 1;
            return it;
        }

        public override IIterateur ItBas()
        {
            var it = new Iterateur<T>(container);
            it.X = X;
            it.Y = Y + 1;
            return it;
        }
    }

}
