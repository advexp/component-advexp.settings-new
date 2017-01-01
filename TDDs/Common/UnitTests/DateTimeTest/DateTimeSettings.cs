using System;
using Advexp;

namespace TDD
{
    public class DateTimeSettings : Advexp.Settings<TDD.DateTimeSettings>
    {
        public const string DefaultDateTimeLocalStringValue = "2009-06-15T13:45:30.0000000-07:00";
        public const string DefaultDateTimeUtcStringValue = "2009-06-15T13:45:30.0000000Z";

        [Setting]
        public static DateTime DateTime;

        [Setting(Secure = true)]
        public static DateTime SecureDateTime;

        [Setting(Default = DefaultDateTimeLocalStringValue)]
        public static DateTime DateTimeLocalValue {get;set;}

        [Setting(Default = DefaultDateTimeUtcStringValue)]
        public static DateTime DateTimeUtcValue {get;set;}
    }
}