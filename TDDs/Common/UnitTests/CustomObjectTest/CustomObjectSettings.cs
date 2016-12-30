using System;
using Advexp;

namespace TDD
{
    public class FooClass
    {
        public Int32 IntValue
        {
            get;
            set;
        }
    }

    public enum FooEnum
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
    }

    public class CustomObjectSettings : Advexp.Settings<TDD.CustomObjectSettings>
    {
        [Setting]
        public static FooClass FooClassInstance {get; set;}

        [Setting]
        public static FooEnum FooEnumValue {get; set;}

        [Setting(Secure = true)]
        public static FooClass SecureFooClassInstance {get; set;}

        [Setting(Secure = true)]
        public static FooEnum SecureFooEnumValue {get; set;}
    }
}

