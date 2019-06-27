using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using FlyLang.Interpreter.Nodes;
using FlyLang.Libraries;

namespace FlyLang.Interpreter
{
    public class InterpreterBase
    {
        public const bool Debug = true;
        public void RunFile(string file)
        {
            Run(new AntlrFileStream(file));
        }

        public void RunText(string text)
        {
            Run(new AntlrInputStream(text));
        }

        public event EventHandler InterpretingStarted;
        public event EventHandler InterpretingStopped;
        private void Run(AntlrInputStream stream)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            var tokenizer = new Parser.FlyLangLexer(stream);
            var parser = new Parser.FlyLangParser(new CommonTokenStream(tokenizer));
            var visitor = new Visitor();
            visitor.Visit(parser.program());
            if(Debug)
                visitor.Tree.Visualize();
            // Load libraries
            var loader = new Loader(visitor.Tree.Nodes.Where(x => x is UseStatement).Select(x => x as UseStatement).ToArray());
            // Run the program
            OnInterpretingStarted();
            visitor.Tree.Invoke();
            OnInterpretingStopped();
        }

        private void OnInterpretingStarted()
        {
            InterpretingStarted?.Invoke(null, EventArgs.Empty);
        }

        private void OnInterpretingStopped()
        {
            InterpretingStopped?.Invoke(null, EventArgs.Empty);
        }
    }
}
