using System;

namespace FlyLang.Interpreter
{
    public static class Helper
    {
        public static bool IsNumeric(object o, object o1)
        {
            return IsNumeric(o) && IsNumeric(o1);
        }
        public static bool IsNumeric(object o)
        {
            return o is int || o is float;
        }
    }
}