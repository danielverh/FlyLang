using System.Collections.Generic;

namespace FlyLang.Interpreter.Nodes
{
    public class ActionTree : Node
    {
        public static Dictionary<string, dynamic> Variables = new Dictionary<string, dynamic>();
        public static Dictionary<string, ActionNode> Actions = new Dictionary<string, ActionNode>();
        public static List<UseStatement> UseStatements = new List<UseStatement>();
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