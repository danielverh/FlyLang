using System;
using System.Linq;
using FlyLang.Libraries;

namespace FlyLang.Interpreter.Nodes
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

        public override void Visualize(Node[] nodes = null, int level = 0, string label = "")
        {
            base.Visualize(nodes, level, Name + (Arguments.Length > 0 ? " (args:)" : ""));
            for (var index = 0; index < Arguments.Length; index++)
            {
                var argument = Arguments[index];

                base.Visualize(new Node[] { argument }, level + 1, "arg" + index);
            }
        }
    }
}
