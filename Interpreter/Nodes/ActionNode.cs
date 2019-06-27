using System;
using System.Collections.Generic;

namespace FlyLang.Interpreter.Nodes
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
            if (Parent is ActionNode node)
                foreach (var arg in node.Arguments)
                {
                    Arguments[arg.Key] = arg.Value;
                }

            object rr = null;
            foreach (var item in Nodes)
            {
                // Implement return here
                if (item is Return ret)
                {
                    rr = ret.Invoke();
                }
                var result = item.Invoke(this);
                if (result is Return r)
                {
                    rr = r.Invoke();
                }

            }
            return rr;
        }
    }
}
