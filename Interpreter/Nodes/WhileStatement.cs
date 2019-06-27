namespace FlyLang.Interpreter.Nodes
{
    public class WhileStatement : Node
    {
        public WhileStatement(Node expression, Node action)
        {
            Add(expression);
            Add(action);
        }

        public Node Expression => Nodes[0];
        public Node Action => Nodes[1];
        public override dynamic Invoke()
        {
            while (Expression.Invoke(Parent) == true)
                Action.Invoke(Parent);
            return null;
        }
    }
}
