using MetaSimulatorConsole.Dijkstra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSimulatorConsole.Simulation
{
    abstract class LinkCaseTemplate // PATTE TEMPLATE
    {
         private Node<Case> GetNode(Coordonnees coor, Grille grille)
         {
             return (Node<Case>)grille[coor.X, coor.Y];     
         }

         protected abstract void AttachObject(Node<Case> node, SujetObserveAbstrait obj);

        protected virtual bool TestNullObject(SujetObserveAbstrait obj)
        {
            if (obj == null)
            {
                Console.WriteLine("LinkObject: Impossible car l'object est nul !");
                return true;
            }
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
             AttachObject(node, obj);
             if (obj != null) obj.Attach(node.Value);
             // else // Dettacher l'objet !
             else obj.DeAttachAll();
        }
    }

    class LinkCaseToZone : LinkCaseTemplate
    {
        protected override void AttachObject(Node<Case> node, SujetObserveAbstrait obj)
        {
            node.Value.SetZoneToObserve((ZoneFinale) obj);
        }

    }

    class UnLinkCaseFromZone : LinkCaseTemplate
    {
        protected override bool TestNullObject(SujetObserveAbstrait obj)
        {
            return false;
        }

        protected override void AttachObject(Node<Case> node, SujetObserveAbstrait obj)
        {
            node.Value.SetZoneToObserve(null);
        }
    }
    class LinkCaseToObject : LinkCaseTemplate
    {
        protected override void AttachObject(Node<Case> node, SujetObserveAbstrait obj)
        {
            node.Value.SetObjectToObserve((ObjetAbstrait)obj);
        }
    }

    class UnLinkCaseFromObject : LinkCaseTemplate
    {
        protected override bool TestNullObject(SujetObserveAbstrait obj)
        {
            return false;
        }

        protected override void AttachObject(Node<Case> node, SujetObserveAbstrait obj)
        {
            node.Value.SetObjectToObserve(null);
        }
    }

    class LinkCaseToPersonnage : LinkCaseTemplate
    {
        protected override void AttachObject(Node<Case> node, SujetObserveAbstrait obj)
        {
            node.Value.SetPersonnageToObserve((PersonnageAbstract)obj);
        }
    }

    class UnLinkCaseFromPersonnage: LinkCaseTemplate
    {
        protected override bool TestNullObject(SujetObserveAbstrait obj)
        {
            return false;
        }

        protected override void AttachObject(Node<Case> node, SujetObserveAbstrait obj)
        {
            node.Value.SetPersonnageToObserve(null);
        }
    }
}
