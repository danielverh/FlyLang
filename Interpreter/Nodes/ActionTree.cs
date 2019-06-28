using System.Collections.Generic;

namespace FlyLang.Interpreter.Nodes
{
    public class ActionTree : Node
    {
        public static Dictionary<string, dynamic> Variables = new Dictionary<string, dynamic>();
        public static Dictionary<string, ActionNode> Actions = new Dictionary<string, ActionNode>();
        public static List<UseStatement> UseStatements = new List<UseStatement>();
        public static Dictionary<string, ClassDefNode> ClassDefinitions = new Dictionary<string, ClassDefNode>();
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