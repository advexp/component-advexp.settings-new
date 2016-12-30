using Android.App;
using Android.OS;
using Android.Preferences;

namespace Sample.JSONSettings.Android
{
    [Activity(Label = "LocalPreferencesActivity")]
    public class LocalPreferencesActivity : PreferenceActivity
    {
        //------------------------------------------------------------------------------
        protected override void OnCreate(Bundle savedInstanceState) 
        {
            base.OnCreate(savedInstanceState);

            var transaction = FragmentManager.BeginTransaction();
            var preferences = new LocalPreferenceFragment();
            transaction.Replace(global::Android.Resource.Id.Content, preferences);
            transaction.Commit();
        }

        //------------------------------------------------------------------------------
        public override void OnBackPressed()
        {
            base.OnBackPressed();

            MainActivity.UpdatePropertiesText();
        }

        //------------------------------------------------------------------------------
        class LocalPreferenceFragment : PreferenceFragment
        {
            //------------------------------------------------------------------------------
            public override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);

                AddPreferencesFromResource(Resource.Xml.preferences);
            }
        }
    }
}

