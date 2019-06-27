using System;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Interpreter
{
    public class ActionTree : Node
    {
        public static Dictionary<string, dynamic> Variables = new Dictionary<string, dynamic>();
        public static Dictionary<string, ActionNode> Actions = new Dictionary<string, ActionNode>();
        public override dynamic Invoke()
        {
            foreach (var item in Nodes)
            {
                item.Invoke();
            }
           return null;
        }
    }
}