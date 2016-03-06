using System;
using Advexp;

namespace TDD
{
    [Serializer(typeof(ExternalSharedPreferencesClassSerializer))]
    public class ExternalSharedPreferencesSettings : Advexp.Settings<TDD.ExternalSharedPreferencesSettings>
    {
        [Setting(Name = "string")]
        public static String StringValue {get; set;}

        [Setting(Name = "int")]
        [Serializer(typeof(ExternalSharedPreferencesFieldSerializer))]
        public static Int32 IntValue {get; set;}
    }
}

