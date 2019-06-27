using FlyLang.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FlyLang.Interpreter.Nodes;

namespace FlyLang.Libraries
{
    public class Loader
    {
        public static Dictionary<string, Dictionary<string, Func<object[], dynamic>>> Libraries =
            new Dictionary<string, Dictionary<string, Func<object[], dynamic>>>();
        public static string[] LibraryNames;
        public Loader(UseStatement[] usings)
        {
            string[] usable = usings.SelectMany(x => x.Names).Distinct().ToArray();
            // Basic library assemblies
            var assembly = Assembly.GetExecutingAssembly();
            var libs = assembly.GetTypes()
                .Select(x => (Assembly: x, (FlyLibrary)x.GetCustomAttribute(typeof(FlyLibrary))))
                .Where(x => x.Item2 != null).ToArray();
            foreach (var lib in libs)
            {
                if (usable.Contains(lib.Item2.Name.ToLower()))
                {
                    var methods = lib.Item1.GetMethods().Distinct();
                    var d = new Dictionary<string, Func<object[], dynamic>>();
                    foreach (var method in methods)
                    {
                        var attr = method.GetCustomAttribute<PublicMethod>();
                        if (attr != null)
                            d.Add(attr.Name, (o) =>
                            {
                                return method.Invoke(null, new object[] { o });
                            });
                    }
                    Libraries.Add(lib.Item2.Name.ToLower(), d);
                }
            }
        }
    }
}
