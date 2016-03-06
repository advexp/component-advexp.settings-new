using Android.Content;

namespace TDD
{
    public class ExternalSharedPreferencesFieldSerializer
        : Advexp.SharedPreferencesSerializer
    {
        static ISharedPreferences s_sharedPreferences = null;

        //------------------------------------------------------------------------------
        public ExternalSharedPreferencesFieldSerializer()
        {
            SetSharedPreferences(s_sharedPreferences);
        }

        //------------------------------------------------------------------------------
        public static void SetMySharedPreferences(ISharedPreferences sharedPreferences)
        {
            s_sharedPreferences = sharedPreferences;
        }
    }
}

