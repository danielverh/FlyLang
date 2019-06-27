using System;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Libraries
{
    [FlyLibrary("Base")]
    public class Base
    {
        [PublicMethod("print")]
        public static void Print(object[] args)
        {
            foreach(var a in args)
                Console.Write(a);
            Console.Write("\n");
        }
        [PublicMethod("input")]
        public static string Input(object[] args)
        {
            return Console.ReadLine();
        }
        [PublicMethod("sqrt")]
        public static float Sqrt(object[] args) => (float)Math.Sqrt(Convert.ToSingle(args[0]));
    }
}
