using System;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Interpreter.Nodes
{
    public class ArrayNode : Node
    {
        public ArrayNode(Node[] nodes)
        {
            AddRange(nodes);
        }
        public override dynamic Invoke()
        {
            var items = new object[Nodes.Count];
            for (int i = 0; i < Nodes.Count; i++)
            {
                items[i] = Nodes[i].Invoke(Parent);
            }
            return items;
        }
    }
}
