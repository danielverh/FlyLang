using System;
using System.Collections.Generic;

namespace FlyLang.Interpreter
{
    public abstract class Node
    {
        public Node()
        {
            Nodes = new List<Node>();
        }
        public Node(params Node[] nodes)
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
            if (Parent != null && Parent is ActionNode)
            {
                var a = (Parent as ActionNode);
                if (a.Arguments.ContainsKey(name))
                    return a.Arguments[name].Invoke(Parent);
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