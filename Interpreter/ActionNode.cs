using System;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Interpreter
{
    public class ActionNode : Node
    {
        public Dictionary<string, Node> Arguments = new Dictionary<string, Node>();
        public string[] ArgNames;
        public ActionNode(Node[] items)
        {
            AddRange(items);
        }
        public dynamic Invoke(Node[] args)
        {
            if (ArgNames.Length != args.Length)
                throw new Exception();
            Arguments.Clear();
            for (int i = 0; i < ArgNames.Length; i++)
            {
                Arguments.Add(ArgNames[i], args[i]);
            }
            return Invoke();
        }
        public override dynamic Invoke()
        {
            foreach (var item in Nodes)
            {
                var result = item.Invoke(this);
                // Implement return here
                if (item is Return)
                {
                    var r = (item as Return).Expression.Invoke(this);
                    return r;
                }
            }
            return null;
        }
    }
}
