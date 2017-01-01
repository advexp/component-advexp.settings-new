using System;
using Android.App;
using Android.Runtime;
using Advexp;
using Advexp.JSONSettings.Plugin;

namespace TDD.Android
{
    [Application]
    public class MainApplication : global::Android.App.Application
    {
        //------------------------------------------------------------------------------
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        //------------------------------------------------------------------------------
        public override void OnCreate()
        {
            base.OnCreate();

            SettingsConfiguration.KeyStoreFileProtectionPassword = "password";
            SettingsConfiguration.KeyStoreFileName = "keystore";
            SettingsConfiguration.EncryptionServiceID = "Advexp.Settings.TDD";

            SettingsBaseConfiguration.RegisterSettingsPlugin<IJSONSettingsPlugin, JSONSettingsPlugin>();
        }
    }
}

