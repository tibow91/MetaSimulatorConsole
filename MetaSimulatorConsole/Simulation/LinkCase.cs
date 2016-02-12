using MetaSimulatorConsole.Dijkstra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation
{
    public abstract class LinkCaseTemplate // PATTE TEMPLATE
    {
         private Node<Case> GetNode(Coordonnees coor, Grille grille)
         {
             return (Node<Case>)grille[coor.X, coor.Y];     
         }

         protected abstract void AttachObjectToCase(Node<Case> node, SujetObserveAbstrait obj);

        private bool TestNullObject(SujetObserveAbstrait obj)
        {
            if (obj == null) throw  new NullReferenceException("L'objet à relier est null !");
            return false;
        }

        public void LinkObject(Coordonnees coor, SujetObserveAbstrait obj, Grille grille)
        {
            if (coor == null) return;
            if (!coor.EstValide()) return;
            if(grille == null) return;
            if (TestNullObject(obj)) return;
            var node = GetNode(coor,grille);
            if (node == null)
            {
                Console.WriteLine("LinkCaseToZone: Impossible de récupérer une node viable en " + coor);
                return;
            }
            AttachObjectToCase(node, obj);
            AttachCaseToObject(node, obj);
        }

        protected virtual void AttachCaseToObject(Node<Case> node, SujetObserveAbstrait obj)
        {
            if (obj != null) obj.Attach(node.Value);
        }
    }

    public abstract class UnLinkCaseTemplate : LinkCaseTemplate
    {
        protected abstract override void AttachObjectToCase(Node<Case> node, SujetObserveAbstrait obj);
        protected sealed override void AttachCaseToObject(Node<Case> node, SujetObserveAbstrait obj)
        {
            //obj.DeAttachAll(); // L'objet,La zone et le personnages ne doivent avoir que la case comme IObserverAbstrait !
            /* Retire les objets cases parmi les observers */
            var list = obj.GetObservers();
            int count = list.Count;
            for (int i=0; i< count; ++i)
            {
                if (list[i] is Case)
                {
                    list.RemoveAt(i);
                    --i;
                    --count;
                }
            }
        }

    }

    class LinkCaseToZone : LinkCaseTemplate
    {
        protected override void AttachObjectToCase(Node<Case> node, SujetObserveAbstrait obj)
        {
            node.Value.SetZoneToObserve((ZoneFinale) obj);
        }
    }

    class UnLinkCaseFromZone : UnLinkCaseTemplate
    {
        protected override void AttachObjectToCase(Node<Case> node, SujetObserveAbstrait obj)
        {
            node.Value.SetZoneToObserve(null);
        }
    }

    class LinkCaseToObject : LinkCaseTemplate
    {
        protected override void AttachObjectToCase(Node<Case> node, SujetObserveAbstrait obj)
        {
            node.Value.SetObjectToObserve((ObjetAbstrait)obj);
        }
    }

    class UnLinkCaseFromObject : UnLinkCaseTemplate
    {

        protected override void AttachObjectToCase(Node<Case> node, SujetObserveAbstrait obj)
        {
            node.Value.SetObjectToObserve(null);
        }
 
    }

    public class LinkCaseToPersonnage : LinkCaseTemplate
    {
        protected override void AttachObjectToCase(Node<Case> node, SujetObserveAbstrait obj)
        {
            node.Value.SetPersonnageToObserve((PersonnageAbstract)obj);
        }
    }

    public class UnLinkCaseFromPersonnage: UnLinkCaseTemplate
    {
        protected override void AttachObjectToCase(Node<Case> node, SujetObserveAbstrait obj)
        {
            node.Value.SetPersonnageToObserve(null);
        }

    }
}
