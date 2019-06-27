using System;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Interpreter
{
    public class Return : Node
    {
        public Return(Node expr)
        {
            Expression = expr;
        }
        public Node Expression { get; }

        public override dynamic Invoke()
        {
            return Expression.Invoke(Parent);
        }
    }
}
