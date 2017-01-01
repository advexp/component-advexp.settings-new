﻿using Advexp;

namespace Sample.JSONSettings.iOS
{
    public enum SampleEnum
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
    }

    class Settings : Advexp.Settings<Settings>
    {
        [Setting]
        public static bool Bool {get; set;}

        [Setting(Secure = true, Default = "default text")]
        public static string String {get; set;}

        [Setting]
        public static SampleEnum Enum {get; set;}
    }
}
