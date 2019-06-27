namespace FlyLang.Interpreter.Nodes
{
    public class IfStatement : Node
    {
        public IfStatement(Node expression, Node action)
        {
            Add(expression);
            Add(action);
        }

        public Node Expression => Nodes[0];
        public Node Action => Nodes[1];
        public Node[] ElifExpressions { get; set; }
        public Node[] ElifActions { get; set; }
        public Node ElseAction { get; set; }

        public override dynamic Invoke()
        {
            if (Expression.Invoke(Parent) == true)
            {
                return Action.Invoke(Parent);
            }
            if (ElifExpressions != null)
                for (int i = 0; i < ElifExpressions.Length; i++)
                {
                    if (ElifExpressions[i].Invoke(Parent) == true)
                    {
                        return ElifActions[i].Invoke(Parent);
                    }
                }
            if (ElseAction != null)
                return ElseAction.Invoke(Parent);
            return null;
        }

        public override void Visualize(Node[] nodes = null, int level = 0, string label = "")
        {
            base.Visualize(null, level, "IF");
            for (int i = 0; i < ElifActions.Length; i++)
            {
                base.Visualize(new Node[] { ElifExpressions[i], ElifActions[i]}, level + 1, "ELIF");
            }
            base.Visualize(new Node[]{ElseAction}, level + 1, "ELSE");
        }
    }
}
