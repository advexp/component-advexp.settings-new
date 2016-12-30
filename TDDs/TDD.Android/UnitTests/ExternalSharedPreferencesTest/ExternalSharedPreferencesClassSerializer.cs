using Android.Content;

namespace TDD
{
    public class ExternalSharedPreferencesClassSerializer
        : Advexp.SharedPreferencesSerializer
    {
        static ISharedPreferences s_sharedPreferences = null;

        //------------------------------------------------------------------------------
        public ExternalSharedPreferencesClassSerializer()
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

