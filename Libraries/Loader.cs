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
        public static Dictionary<string, Library> Libraries =
            new Dictionary<string, Library>();
        public static string[] LibraryNames;
        public static object Self { get; set; } = null;
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
                    var library = new Library(lib.Item2.Name);
                    var methods = lib.Item1.GetMethods().Distinct();
                    foreach (var method in methods)
                    {
                        var attr = method.GetCustomAttribute<PublicMethod>();
                        var arguments = method.GetGenericArguments();
                        var retType = method.ReturnType;
                        if (attr != null)
                        {
                            Method m = null;
                            if (retType == typeof(string))
                                m = new Method<string>(method);
                            else if (retType == typeof(int))
                                m = new Method<int>(method);
                            else if (retType == typeof(float))
                                m = new Method<float>(method);
                            else if (retType == typeof(bool))
                                m = new Method<bool>(method);
                            else if (retType == typeof(Dictionary<object, object>))
                                m = new Method<Dictionary<object, object>>(method);
                            else if (retType == typeof(object[]))
                                m = new Method<object[]>(method);
                            else if (retType == typeof(void))
                                m = new Method<object>(method);
                            m.Function = (args) => method.Invoke(null, new object[] { args });
                            library.Methods.Add(attr.Name, m);
                        }
                    }
                    Libraries.Add(lib.Item2.Name, library);
                }
            }
        }
    }
}
