﻿using UIKit;
using Advexp.JSONSettings.Plugin;

namespace Sample.JSONSettings.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            Advexp.SettingsBaseConfiguration.SettingsNamePattern = "{ClassName}.{FieldName}";
            Advexp.SettingsBaseConfiguration.DisableFormatMigration = true;
            Advexp.SettingsBaseConfiguration.RegisterSettingsPlugin<IJSONSettingsPlugin, JSONSettingsPlugin>();

            JSONSettingsConfiguration.JsonSerializerSettings.Formatting = 
                Newtonsoft.Json.Formatting.Indented;

            JSONSettingsConfiguration.JsonSerializerSettings.Converters.
                Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            JSONSettingsConfiguration.PluginSettings.SkipSecureValues = false;

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
