using System;
using System.Collections.Generic;

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
    }
}