using System;
using Advexp;

namespace TDD
{
    public class NotLoadedValuesSettings : Advexp.Settings<TDD.NotLoadedValuesSettings>
    {
        [Setting]
        public static String StringValue {get; set;}

        [Setting]
        public static DateTime DateTimeValue {get; set;}

        [Setting]
        public static Int32 Int32Value {get; set;}

        [Setting(Secure = true)]
        public static String SecureStringValue {get; set;}

        [Setting(Secure = true)]
        public static DateTime SecureDateTimeValue {get; set;}

        [Setting(Secure = true)]
        public static Int32 SecureInt32Value {get; set;}
    }
}

