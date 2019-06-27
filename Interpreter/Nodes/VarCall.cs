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
    }
}
