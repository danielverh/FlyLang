namespace FlyLang.Interpreter.Nodes
{
    public class IfStatement : Node
    {
        public IfStatement(Node expression, Node action)
        {
            Expression = expression;
            Action = action;
        }
        public Node Expression { get; }
        public Node Action { get; }
        public Node[] ElifExpressions { get; set; }
        public Node[] ElifActions { get; set; }
        public Node ElseAction { get; set; }

        public override dynamic Invoke()
        {
            if (Expression.Invoke() == true)
            {
                return Action.Invoke(Parent);
            }
            if (ElifExpressions != null)
                for (int i = 0; i < ElifExpressions.Length; i++)
                {
                    if (ElifExpressions[i].Invoke() == true)
                    {
                        return ElifActions[i].Invoke();
                    }
                }
            if (ElseAction != null)
                return ElseAction.Invoke();
            return null;
        }
    }
}
