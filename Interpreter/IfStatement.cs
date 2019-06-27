using System;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Interpreter
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
                Action.Invoke(Parent);
                return null;
            }
            if (ElifExpressions != null)
                for (int i = 0; i < ElifExpressions.Length; i++)
                {
                    if (ElifExpressions[i].Invoke() == true)
                    {
                        ElifActions[i].Invoke();
                        return null;
                    }
                }
            if (ElseAction != null)
                ElseAction.Invoke();
            return null;
        }
    }
}
