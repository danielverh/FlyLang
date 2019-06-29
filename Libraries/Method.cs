using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FlyLang.Libraries
{
    public class Method<T> : Method
    {
        public Method()
        {
        }

        public Method(MethodInfo info)
        {
            Arguments = info.GetGenericArguments();
            ReturnType = info.ReturnType;
        }

        public T Run(object[] arguments)
        {
            for (int i = 0; i < arguments.Length; i++)
            {
                object arg = (object)arguments[i];
            }
            return (T)Function.Invoke(arguments);
        }
        public Type ReturnType;
        public Type[] Arguments;
    }
    public abstract class Method
    {
        public Func<object[], dynamic> Function;
    }
}
