using System;
using System.Collections.Generic;
using System.Linq;
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
        [PublicMethod("printSingle")]
        public static void PrintSingle(object[] args)
        {
            foreach (var a in args)
                Console.Write(a);
        }
        [PublicMethod("input")]
        public static string Input(object[] args)
        {
            return Console.ReadLine();
        }
        [PublicMethod("inputOne")]
        public static string InputOne(object[] args)
        {
            return Console.Read().ToString();
        }
        [PublicMethod("first")]
        public static string First(object[] args)
        {
            return ((dynamic) args[0])[0].ToString();
        }

        [PublicMethod("at")]
        public static string At(object[] args)
        {
            return ((string)args[0])[(int)args[1]].ToString();
        }
        [PublicMethod("length")]
        public static int Length(object[] args)
        {
            if (args.Length == 0)
                return ((dynamic)Loader.Self).Length;
            return ((dynamic)args[0]).Length;
        }
        [PublicMethod("toAscii")]
        public static string Ascii(object[] args)
        {
            var a = (byte)(int)Loader.Self;
            var s = Encoding.ASCII.GetString(new byte[] { a});
            return s;
        }
        [PublicMethod("asciiToInt")]
        public static int AsciiToInt(object[] args)
        {
            var a = Loader.Self.ToString().First().ToString();
            return Encoding.ASCII.GetBytes(a)[0];
        }
        [PublicMethod("take")]
        public static object[] Take(object[] args)
        {
            var i = (int)args[0];
            var items = (object[])Loader.Self;
            return items.Take(i).ToArray();
        }
        [PublicMethod("sqrt")]
        public static float Sqrt(object[] args) => (float)Math.Sqrt(Convert.ToSingle(args[0]));
    }
}
