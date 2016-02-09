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

        public void LinkObject(Coordonnees coor, SujetObserveAbstrait obj, Grille grille)
        {
            if (coor == null) return;
            if (!coor.EstValide()) return;
            if(grille == null) return;
            if (obj == null) 
            {
                Console.WriteLine("LinkObject: Impossible car l'object est nul !");
                return;
            }
            var node = GetNode(coor,grille);
             if (node == null)
             {
                 Console.WriteLine("LinkCaseToZone: Impossible de récupérer une node viable en " + coor);
                 return;
             }
             AttachObject(node, obj);
             obj.Attach(node.Value);
        }
    }

    class LinkCaseToZone : LinkCaseTemplate
    {
        protected override void AttachObject(Node<Case> node, SujetObserveAbstrait obj)
        {
            node.Value.SetZoneToObserve((ZoneFinale) obj);
        }
    }

    class LinkCaseToObject : LinkCaseTemplate
    {
        protected override void AttachObject(Node<Case> node, SujetObserveAbstrait obj)
        {
            node.Value.SetObjectToObserve((ObjetAbstrait)obj);
        }
    }

    class LinkCaseToPersonnage : LinkCaseTemplate
    {
        protected override void AttachObject(Node<Case> node, SujetObserveAbstrait obj)
        {
            node.Value.SetObjectToObserve((ObjetAbstrait)obj);
        }
    }
}
