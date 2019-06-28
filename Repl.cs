using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using CommandLine;
using FlyLang.Libraries;

namespace FlyLang
{
    public class Repl
    {
        public class Options
        {
            [Option('f', "srcFile", Required = false, HelpText = ".fly source input file")]
            public string SourceFile { get; set; }
            [Option('v', "verbose", Required = false, HelpText = "Output extra information")]
            public bool Verbose { get; set; }
        }
        public void Start(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    if (o.SourceFile != null)
                    {
                        RunFile(o.SourceFile);
                    }
                    else
                    {
                        Console.WriteLine(">>> Starting REPL...");
                        var done = false;
                        while (!done)
                        {
                            Console.Write(">>> ");
                            switch (Console.ReadLine()?.Trim())
                            {
                                case "q":
                                case "quit()":
                                    done = true;
                                    continue;
                                case "file()":
                                    Console.Write("Filename: ");
                                    RunFile(Console.ReadLine()?.Trim());
                                    break;
                            }
                        }
                    }
                });

            void RunFile(string o, bool verbose = false)
            {
                if(!File.Exists(o))
                    throw new FileNotFoundException(".fly file not found...", o);
                var b = new Interpreter.InterpreterBase();
                if (!verbose)
                {
                    var stopwatch = new Stopwatch();
                    b.InterpretingStarted += (sender, e) =>
                    {
                        Console.WriteLine($"Finished parsing");
                        Console.WriteLine($"Finished loading libraries");
                        Console.WriteLine($"Executing...");
                        Console.WriteLine($"----------");
                        stopwatch.Start();
                    };
                    b.InterpretingStopped += (sender, eventArgs) => { stopwatch.Stop();
                        Console.WriteLine("----------");
                        Console.WriteLine("Done interpreting");
                        Console.WriteLine($"Interpreting took: {stopwatch.ElapsedMilliseconds}ms");
                    };
                }
                b.RunFile(o);
                
            }
        }

    }
}
