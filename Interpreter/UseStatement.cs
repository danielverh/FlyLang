using System;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Interpreter
{
    public class UseStatement : Node
    {
        public UseStatement(params string[] uses)
        {
            Names = uses;
        }
        public string[] Names { get; set; }

        public override dynamic Invoke()
        {
            return null;
        }
    }
}
