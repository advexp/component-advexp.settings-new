using System;
using Advexp;
using Advexp.JSONSettings.Plugin;

namespace Sample.Assembly.PCL
{
    public static class AssemblyClass
    {
        //------------------------------------------------------------------------------
        static AssemblyClass()
        {
            BaseSettingsConfiguration.EnablePlugin<IJSONSettingsPlugin, JSONSettingsPlugin>();
        }

        //------------------------------------------------------------------------------
        public static string GetJSON()
        {
            var jsonPlugin = Settings.GetPlugin<IJSONSettingsPlugin>();

            jsonPlugin.JsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
            //jsonPlugin.JsonSerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonPlugin.JsonSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            return jsonPlugin.Settings;
        }

        //------------------------------------------------------------------------------
        public static string  StringValue
        {
            get
            {
                return Settings.String;
            }
            set
            {
                Settings.String = value;
            }
        }

        //------------------------------------------------------------------------------
        public static bool BoolValue 
        {
            get
            {
                return Settings.Bool;
            }
            set
            {
                Settings.Bool = value;
            }
        }

        //------------------------------------------------------------------------------
        public static SampleEnum EnumValue
        {
            get
            {
                return Settings.Enum;
            }
            set
            {
                Settings.Enum = value;
            }
        }

        //------------------------------------------------------------------------------
        public static void Load()
        {
            Settings.LoadSettings();
        }

        //------------------------------------------------------------------------------
        public static void Save()
        {
            Settings.SaveSettings();
        }
    }
}