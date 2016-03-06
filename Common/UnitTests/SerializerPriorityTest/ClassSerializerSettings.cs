using System;
using Advexp;

namespace TDD
{
    [Serializer(typeof(ClassSerializer))]
    public class ClassSerializerSettings : Advexp.Settings<TDD.ClassSerializerSettings>
    {
        [Setting]
        public static Int32 Setting1 {get; set;}

        [Setting(Secure = true)]
        public static Int32 Setting2 {get; set;}

    }
}

