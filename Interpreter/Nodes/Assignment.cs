namespace FlyLang.Interpreter.Nodes
{
    public class Assignment : Node
    {
        public Assignment(string name, Node expression)
        {
            Name = name;
            Expression = expression;
        }
        public string Name { get; }
        public Node Expression { get; }
        public override dynamic Invoke()
        {
            var value = Expression.Invoke(Parent);
            ActionTree.Variables[Name] = value;
            return value;
        }
    }
}
