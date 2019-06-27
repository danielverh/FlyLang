namespace FlyLang.Interpreter.Nodes
{
    public class Definition : Node
    {
        public Definition(string name, ActionNode action)
        {
            Name = name;
            Add(action);
        }

        public ActionNode ActionNode => Nodes[0] as ActionNode;
        public string Name { get; }
        public override dynamic Invoke()
        {
            ActionTree.Actions.Add(Name, ActionNode);
            return null;
        }

        
    }
}
