using System;
using System.Collections.Generic;
using System.Linq;

namespace FlyLang.Interpreter.Nodes
{
    public abstract class Node
    {
        protected Node()
        {
            Nodes = new List<Node>();
        }

        protected Node(params Node[] nodes)
        {
            Nodes = new List<Node>(nodes);
        }
        public bool HasChildren => Nodes.Count > 0;
        public Node Parent { get; set; } = null;
        public List<Node> Nodes { get; }
        public void AddRange(params Node[] nodes) => Nodes.AddRange(nodes);
        public void Add(Node n) => Nodes.Add(n);
        public dynamic GetValue(string name)
        {
            if (Parent is ActionNode node)
            {
                var a = node;
                if (a.Arguments.ContainsKey(name))
                    return a.Arguments[name].Invoke(node);
            }
            else if (Parent is ClassDefNode classDef)
                return classDef.Locals[name].Invoke(classDef);
            if (ActionTree.Variables.ContainsKey(name))
                return ActionTree.Variables[name];
            throw new Exception();
        }
        public dynamic Invoke(Node parentCall)
        {
            Parent = parentCall;
            return Invoke();
        }
        public abstract dynamic Invoke();
        public T Invoke<T>() => (T)Invoke();

        public virtual void Visualize(Node[] nodes = null, int level = 0, string label = "")
        {
            nodes = nodes ?? Nodes.ToArray();
            Console.Write(string.Concat(Enumerable.Repeat(" - ", level)));
            Console.Write(GetType().Name + (label == string.Empty ? "" : ": " + label));
            Console.Write("\n");
            if (HasChildren)
                foreach (var node in nodes)
                {
                    node?.Visualize(level: level + 1);
                }
        }
    }
}