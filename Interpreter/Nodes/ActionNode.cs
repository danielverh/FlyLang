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

        public dynamic Invoke(Node parent, Node[] args)
        {
            Parent = parent;
            return Invoke(args);
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

            foreach (var item in Nodes)
            {
                // Implement return here
                if (item is Return r)
                {
                    return r;
                    continue;
                }
                var result = item.Invoke(this);
                if (result is Return r2)
                {
                    return r2;
                }

            }
            return null;
        }
    }
}
