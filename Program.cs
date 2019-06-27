using System;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Antlr4.Runtime;
using FlyLang.Libraries;
using FlyLang.Interpreter;
using FlyLang.Interpreter.Nodes;

namespace FlyLang
{
    class Program
    {
        static void Main(string[] args)
        {
            var repl = new Repl();
//            repl.Start(args);
            repl.Start(new string[]{"-f" + @"C:\Users\danie\Downloads\test2.fly" });

        }
    }
}
