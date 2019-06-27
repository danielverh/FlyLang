namespace FlyLang.Interpreter.Nodes
{
    public class Literal : Node
    {
        public static Literal From(dynamic val) => new Literal(val);
        public Literal(dynamic value)
        {
            Value = value;
        }
        public dynamic Value { get; }
        public override dynamic Invoke()
        {
            return Value;
        }
    }
}
