using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Interpreter.Nodes
{
    public class DictionaryNode : Node
    {
        public Dictionary<Node, Node> Dictionary;
        public DictionaryNode((Node key, Node value)[] items)
        {
            Dictionary = new Dictionary<Node, Node>(items.Length);
            foreach (var item in items)
            {
                Dictionary.Add(item.key, item.value);
            }
        }
        public override dynamic Invoke()
        {
            var dict = new Dictionary<object, object>();
            foreach (var item in Dictionary)
            {
                dict[item.Key.Invoke()] = item.Value.Invoke();
            }
            return dict;
        }
    }
}
