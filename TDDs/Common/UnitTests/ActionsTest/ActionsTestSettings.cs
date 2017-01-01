using System;
using Advexp;

namespace TDD
{
    class ActionsTestSettings : Advexp.Settings<TDD.ActionsTestSettings>
    {
        [Setting("foo")]
        public static String LocalFooString {get; set;}

        [Setting(Name = "secureFoo", Secure = true)]
        public static String SecureFooString {get; set;}
    }
}
