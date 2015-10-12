using System;
using Advexp;

namespace TDD
{
    public class SimpleSettings : Advexp.Settings<TDD.SimpleSettings>
    {
        [Setting]
        public static Int32 Setting1 {get; set;}

        [Setting(Secure = true)]
        public static Int32 Setting2 {get; set;}
    }
}

