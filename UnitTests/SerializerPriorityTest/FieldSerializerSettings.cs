using System;
using Advexp;

namespace TDD
{
    [Serializer(typeof(ClassSerializer))]
    public class FieldSerializerSettings : Advexp.Settings<TDD.FieldSerializerSettings>
    {
        [Setting]
        [Serializer(typeof(FieldSerializer))]
        public static Int32 Setting1 {get; set;}

        [Setting(Secure = true)]
        [Serializer(typeof(FieldSerializer))]
        public static Int32 Setting2 {get; set;}
    }
}

