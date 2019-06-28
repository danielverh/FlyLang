using System;
using System.Collections.Generic;

namespace FlyLang.Interpreter.Nodes
{
    public class ActionNode : Node
    {
        public Dictionary<string, Node> Arguments = new Dictionary<string, Node>();
        public string[] ArgNames;
        public Node Target { get; set; }
        public ActionNode(Node[] items)
        {
            AddRange(items);
        }

        public dynamic Invoke(Node parent, Node[] args, Node target = null)
        {
            Parent = parent;
            Target = target;
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
            if (Target != null)
                Arguments["self"] = Target.Invoke();
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
                    if (Parent == null)
                        return r.Invoke(this);
                    return r;
                }
                var result = item.Invoke(this);
                if (result is Return r2)
                {
                    if (Parent == null)
                        return r2.Invoke(this);
                    return r2;
                }

            }
            return null;
        }
    }
}
