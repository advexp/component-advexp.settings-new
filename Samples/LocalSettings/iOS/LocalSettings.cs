using System;
using Advexp;

namespace Sample
{
    enum AutoLock
    {
        e_never = 0,
        e_1minute = 1,
        e_2minutes = 2,
        e_3minutes = 3,
        e_4minutes = 4,
        e_5minutes = 5,
    }

    enum HomeButtonDoubleClick
    {
        e_home = 0,
        e_search = 1,
        e_phoneFavorite = 2,
        e_camera = 3,
        e_iPod = 4,
    }

    class LocalSettings : Advexp.Settings<Sample.LocalSettings>
    {
        [Setting(Name = "airplaneMode")]
        public static Boolean AirplaneMode {get; set;}

        [Setting(Name = "notifications")]
        public static Boolean Notifications {get; set;}

        [Setting(Name = "brightness")]
        public static int Brightness {get; set;}

        [Setting(Name = "autoBrightness")]
        public static bool AutoBrightness {get; set;}

        [Setting(Name = "login")]
        public static String Login {get; set;}

        // "Secure = true" mean, that setting value will be saved to the keychain
        [Setting(Name = "password", Secure = true)]
        public static String Password {get; set;}

        [Setting(Name = "time")]
        public static DateTime Time {get; set;}

        [Setting(Name = "date")]
        public static DateTime Date {get; set;}

        // In this case, the automatic setting name in storage will be
        // "Sample.LocalSettings.Bluetooth"
        // The name pattern can be changed using the SettingsConfiguration.SettingsNamePattern property
        // The default pattern name is: "{NamespaceName}.{ClassName}.{FieldName}"
        [Setting]
        public static bool Bluetooth {get; set;}

        [Setting(Name = "locationServices")]
        public static bool LocationServices {get; set;}

        [Setting(Name = "autoLock")]
        public static AutoLock AutoLock {get; set;}

        [Setting(Name = "homeButtonDoubleClick")]
        public static HomeButtonDoubleClick HomeButtonDoubleClick {get; set;}

        static LocalSettings()
        {
            AirplaneMode = false;
            Notifications = false;

            Brightness = 50;
            AutoBrightness = false;

            Login = String.Empty;
            Password = String.Empty;

            Time = DateTime.Now;
            Date = DateTime.Now;

            Bluetooth = false;

            LocationServices = true;

            AutoLock = AutoLock.e_never;

            HomeButtonDoubleClick = HomeButtonDoubleClick.e_home;
        }
    }
}
