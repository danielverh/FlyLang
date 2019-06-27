namespace FlyLang.Interpreter.Nodes
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
            return GetValue(Name);
        }

        public override void Visualize(Node[] nodes = null, int level = 0, string label = "")
        {
            base.Visualize(nodes, level, Name);
        }
    }
}
