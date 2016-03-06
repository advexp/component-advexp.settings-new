using System;
using Advexp;

namespace TDD
{
    [Serializer(typeof(ExternalSharedPreferencesClassSerializer))]
    public class ExternalSecureSharedPreferencesSettings : Advexp.Settings<TDD.ExternalSecureSharedPreferencesSettings>
    {
        [Setting(Name = "secureString", Secure = true)]
        public static String SecureStringValue {get; set;}

        [Setting(Name = "secureInt", Secure = true)]
        [Serializer(typeof(ExternalSharedPreferencesFieldSerializer))]
        public static Int32 SecureIntValue {get; set;}
    }
}

