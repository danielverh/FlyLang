using System;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Interpreter
{
    public class VarCall : Node
    {
        public string Name { get; }
        public VarCall(string name)
        {
            Name = name;
        }
        public override dynamic Invoke()
        {
            if (ActionTree.Variables.ContainsKey(Name))
                return ActionTree.Variables[Name];
            if(Parent != null && Parent is ActionNode)
            {
                var p = Parent as ActionNode;
                if (p.Arguments.ContainsKey(Name))
                    return p.Arguments[Name].Invoke();
            }
            return null;
        }
    }
}
