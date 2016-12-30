using System;
using Advexp;

namespace TDD
{
    public class DifferentMemberTypesLocalSettings : Advexp.Settings<TDD.DifferentMemberTypesLocalSettings>
    {
        [Setting]
        public int m_memberField;
        [Setting]
        public static int m_staticField;
        [Setting]
        public int MemberProperty {get; set;}
        [Setting]
        public static int StaticProperty {get; set;}
    }
}

