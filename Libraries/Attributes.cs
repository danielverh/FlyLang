using System;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Libraries
{
    public class FlyLibrary : Attribute
    {
        public FlyLibrary(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
    public class PublicMethod : Attribute
    {
        public string Name { get; }
        public PublicMethod(string name)
        {
            Name = name;
        }
    }
}
