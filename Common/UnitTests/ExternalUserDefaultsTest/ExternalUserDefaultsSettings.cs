using System;
using Advexp;

namespace TDD
{
    [Serializer(typeof(ExternalUserDefaultsClassSerializer))]
    public class ExternalUserDefaultsSettings : Advexp.Settings<TDD.ExternalUserDefaultsSettings>
    {
        [Setting("string")]
        public static String StringValue {get; set;}

        [Setting("int")]
        [Serializer(typeof(ExternalUserDefaultsFieldSerializer))]
        public static Int32 IntValue {get; set;}
    }
}

