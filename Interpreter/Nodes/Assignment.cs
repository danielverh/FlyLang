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
    public class ArrayAssignment : Node
    {
        public ArrayAssignment(string name, Node position, Node expression)
        {
            Name = name;
            Expression = expression;
            Position = position;
        }
        public string Name { get; }
        public Node Position { get; }
        public Node Expression { get; }
        public override dynamic Invoke()
        {
            var value = Expression.Invoke(Parent);
            ActionTree.Variables[Name][Position.Invoke(Parent)] = value;
            return value;
        }
    }
}
