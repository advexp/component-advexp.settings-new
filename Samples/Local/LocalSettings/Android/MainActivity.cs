using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Preferences;
using Android.Content;

namespace Sample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        static WeakReference s_TextView = new WeakReference(null);

        //------------------------------------------------------------------------------
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Advexp.SettingsModuleInitializer.Initialize();

            base.OnCreate(savedInstanceState);

            Context context = Android.App.Application.Context;
            PreferenceManager.SetDefaultValues(context, Resource.Xml.preferences, false);

            SetContentView(Resource.Layout.Main);

            s_TextView.Target = FindViewById(Resource.Id.textView);

            UpdatePropertiesText();

            Button button = FindViewById<Button>(Resource.Id.changePrefsButton);
            button.Click += delegate
            {
                StartActivity(typeof(LocalPreferencesActivity));
            };
        }

        //------------------------------------------------------------------------------
        public static void UpdatePropertiesText()
        {
            LocalSettings.LoadSettings();

            TextView textView = (TextView)s_TextView.Target;

            String propertiesText = String.Format(
                "Checkbox value is '{0}'\n" +
                "Switch value is '{1}'\n" +
                "TextPreference value is '{2}'\n" +
                "EnumPreference value is '{3}'", 
                    LocalSettings.Checkbox.ToString(),
                    LocalSettings.Switch.ToString(),
                    LocalSettings.TextPreference.ToString(),
                    LocalSettings.EnumPreference.ToString()
            );

            textView.Text = propertiesText;
        }
    }
}


