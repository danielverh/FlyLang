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
    public class ArrayVarCall : Node
    {
        public string Name { get; }
        public Node Position { get; }
        public ArrayVarCall(string name, Node pos)
        {
            Name = name;
            Position = pos;
        }

        public override dynamic Invoke()
        {
            var p = Position.Invoke();
            return GetValue(Name)[p];
        }
    }
}
