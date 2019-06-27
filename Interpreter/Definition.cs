using System;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Interpreter
{
    public class Definition : Node
    {
        public Definition(string name, ActionNode action)
        {
            Name = name;
            ActionNode = action;
        }
        public ActionNode ActionNode { get; }
        public string Name { get; }
        public override dynamic Invoke()
        {
            ActionTree.Actions.Add(Name, ActionNode);
            return null;
        }
    }
}
