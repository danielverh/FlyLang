using System;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Interpreter.Nodes
{
    public class ClassDefNode : Node
    {
        public string Name { get; }
        public Dictionary<string, Node> Definitions { get; }
        public Dictionary<string, Node> Locals { get; }
        public ClassDefNode(string name, Node[] defs, Node[] assignments)
        {
            Name = name;
            Definitions = new Dictionary<string, Node>();
            Locals = new Dictionary<string, Node>();
            foreach (Definition item in defs)
            {
                Definitions[item.Name] = item;
            }
            foreach (Assignment item in assignments)
            {
                Locals[item.Name] = item;
            }
            ActionTree.ClassDefinitions.Add(Name, this);
        }
        public override dynamic Invoke()
        {
            return null;
        }
    }
}
