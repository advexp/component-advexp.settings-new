using System;
using Advexp;

namespace TDD
{
    public class NullValueSettings : Advexp.Settings<TDD.NullValueSettings>
    {
        [Setting]
        public static String NullValue {get; set;}

        [Setting]
        public static String NormalValue {get; set;}

        [Setting(Secure = true)]
        public static String SecureNullValue {get; set;}

        [Setting(Secure = true)]
        public static String SecureNormalValue {get; set;}
    }
}

