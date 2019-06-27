using System;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Interpreter
{
    public class WhileStatement : Node
    {
        public WhileStatement(Node expression, Node action)
        {
            Expression = expression;
            Action = action;
        }
        public Node Expression { get; }
        public Node Action { get; }
        public override dynamic Invoke()
        {
            while (Expression.Invoke(Parent) == true)
                Action.Invoke(Parent);
            return null;
        }
    }
}
