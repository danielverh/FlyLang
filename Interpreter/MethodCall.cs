using FlyLang.Libraries;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Interpreter
{
    public class MethodCall : Node
    {
        public MethodCall(string name, Node[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }
        public string Name { get; }
        public Node[] Arguments { get; }
        public override dynamic Invoke()
        {
            // Look in libraries:
            if (Loader.Libraries["base"].ContainsKey(Name))
            {
                object[] args = Arguments.Select(x => (object)x.Invoke(Parent)).ToArray();
                if (!(args is object[]))
                    args = new object[] { args };
                var result = Loader.Libraries["base"][Name].Invoke(args);
                return result;
            }
            // Look in local:
            if (ActionTree.Actions.ContainsKey(Name))
                return ActionTree.Actions[Name].Invoke(Arguments);
            throw new Exception("Method not found.");
        }
    }
}
