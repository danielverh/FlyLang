using System;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Antlr4.Runtime;
using FlyLang.Libraries;
using FlyLang.Interpreter;

namespace FlyLang
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            var tokenizer = new Parser.FlyLangLexer(new AntlrFileStream(@"C:\Users\danie\Documents\Projecten\FlyLang\FlyLang.fly"));
            var parser = new Parser.FlyLangParser(new CommonTokenStream(tokenizer));
            var visitor = new Visitor();
            visitor.Visit(parser.program());
            // Load libraries
            var loader = new Loader(visitor.Tree.Nodes.Where(x => x is UseStatement).Select(x => x as UseStatement).ToArray());
            // Run the program
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            visitor.Tree.Invoke();
            stopwatch.Stop();
            Console.WriteLine("------");
            Console.WriteLine($"Took {stopwatch.ElapsedMilliseconds}ms");
            Console.ReadLine();
        }
    }
}
