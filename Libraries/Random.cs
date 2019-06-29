using System;
using System.Collections.Generic;
using System.Text;

namespace FlyLang.Libraries
{
    [FlyLibrary("random")]
    public class Random
    {
        [PublicMethod("randomInt")]
        public static int RandomInt(object[] args)
        {
            return (new System.Random()).Next((int)args[0]);
        }
    }
}
