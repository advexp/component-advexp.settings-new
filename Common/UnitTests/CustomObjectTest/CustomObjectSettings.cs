using System;
using Advexp;

namespace TDD
{
    [Serializable]
    public class FooClass
    {
        public Int32 Value;
    }

    public enum FooEnum
    {
        e_Zero,
        e_One,
        e_Two,
        e_Three,
        e_Four,
        e_Five,
        e_Six,
        e_Seven,
        e_Eight,
        e_Nine,
        e_Ten,
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

