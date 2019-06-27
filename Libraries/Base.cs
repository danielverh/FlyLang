using System;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Libraries
{
    [FlyLibrary("base")]
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
        [PublicMethod("first")]
        public static string First(object[] args)
        {
            return ((string) args[0])[0].ToString();
        }

        [PublicMethod("at")]
        public static string At(object[] args)
        {
            return ((string)args[0])[(int)args[1]].ToString();
        }
        [PublicMethod("len")]
        public static int Length(object[] args)
        {
            return ((string)args[0]).Length;
        }
        [PublicMethod("intToAscii")]
        public static string Ascii(object[] args)
        {
            var i = (int) args[0];
            return Encoding.ASCII.GetString(new byte[] {(byte) i});
        }
        [PublicMethod("sqrt")]
        public static float Sqrt(object[] args) => (float)Math.Sqrt(Convert.ToSingle(args[0]));
    }
}
