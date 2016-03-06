using Advexp;
using NUnit.Framework;

#if __IOS__
using Foundation;
using Security;
#endif // __IOS__

#if __ANDROID__
using Android.Content;
using Android.Preferences;
using Android.App;
#endif // __ANDROID__

namespace TDD
{
    [TestFixture]
    public class ActionsTest
    {
        #if __ANDROID__

        //------------------------------------------------------------------------------
        static string DecryptString(string value)
        {
            if (value != null)
            {
                value = Cryptography.Decrypt(value, SettingsConfiguration.SecureSettingsPassword);
            }

            return value;
        }

        #endif // __ANDROID__

        //------------------------------------------------------------------------------
        static public string GetSecureLocalStringValue(string sKey
                                                       #if __ANDROID__
                                                       , ISharedPreferences prefs = null
                                                       #endif // __ANDROID__
                                                      )
        {
            #if __IOS__

            string sService = SettingsConfiguration.KeyChainServiceName;

            SecStatusCode eCode;
            // Query the record.
            SecRecord oQueryRec = new SecRecord(SecKind.GenericPassword) { Service = sService, Label = sService, Account = sKey };
            oQueryRec = SecKeyChain.QueryAsRecord(oQueryRec, out eCode);

            // If found, try to get password.
            if (eCode == SecStatusCode.Success && oQueryRec != null && oQueryRec.Generic != null)
            {
                // Decode from UTF8.
                return NSString.FromData(oQueryRec.Generic, NSStringEncoding.UTF8);
            }

            // Something went wrong.
            return null;

            #endif // __IOS__

            #if __ANDROID__

            var value = GetLocalStringValue(sKey, prefs);
            if (value == null)
            {
                return null;
            }

            value = DecryptString(value);

            return value;

            #endif // __ANDROID__
        }

        //------------------------------------------------------------------------------
        static public string GetLocalStringValue(string sKey
                                          #if __ANDROID__
                                          , ISharedPreferences prefs = null
                                          #endif // __ANDROID__
                                         )
        {
            #if __ANDROID__

            if (prefs == null)
            {
                Context context = Application.Context;
                prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            }

            string sValue = null;
            if (prefs.Contains(sKey))
            {
                sValue = prefs.GetString(sKey, null);
            }

            return sValue;

            #endif // __ANDROID__

            #if __IOS__

            var prefs = NSUserDefaults.StandardUserDefaults;
            string sValue = prefs.StringForKey(sKey);

            return sValue;

            #endif // __IOS__
        }

        //------------------------------------------------------------------------------
        [Test]
        public void LocalTest()
        {
            const string key = "foo";
            const string value = "FooStringValue";

            ActionsTestSettings.LocalFooString = value;
            ActionsTestSettings.SaveSettings();

            string loadedValue = GetLocalStringValue(key);
            Assert.AreEqual(value, loadedValue);

            ActionsTestSettings.LocalFooString = null;
            ActionsTestSettings.LoadSettings();
            Assert.AreEqual(value, ActionsTestSettings.LocalFooString);

            ActionsTestSettings.DeleteSettings();
            string loadedNullNSUserDefaultsValue = GetLocalStringValue(key);
            Assert.IsNull(loadedNullNSUserDefaultsValue);
            Assert.IsNull(ActionsTestSettings.LocalFooString);
        }

        //------------------------------------------------------------------------------
        [Test]
        public void SecureTest()
        {
            const string secureKey = "secureFoo";
            const string secureValue = "SecureFooStringValue";

            ActionsTestSettings.SecureFooString = secureValue;
            ActionsTestSettings.SaveSettings();

            string loadedSecureValue = ActionsTest.GetSecureLocalStringValue(secureKey);
            Assert.AreEqual(loadedSecureValue, ActionsTestSettings.SecureFooString);
            Assert.AreEqual(loadedSecureValue, secureValue);

            ActionsTestSettings.SecureFooString = null;
            ActionsTestSettings.LoadSettings();
            Assert.AreEqual(secureValue, ActionsTestSettings.SecureFooString);

            ActionsTestSettings.DeleteSettings();
            string loadedNullSecureValue = ActionsTest.GetSecureLocalStringValue(secureKey);
            Assert.IsNull(loadedNullSecureValue);
            Assert.IsNull(ActionsTestSettings.SecureFooString);
        }
    }
}