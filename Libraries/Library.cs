using System;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Libraries
{
    public class Library
    {
        public Library(string name)
        {
            Methods = new Dictionary<string, Method>();
            Name = name;
        }
        public string Name { get; }
        public Dictionary<string, Method> Methods { get; }
        public Method Find(string key)
        {
            return Methods[key];
        }
    }
}
