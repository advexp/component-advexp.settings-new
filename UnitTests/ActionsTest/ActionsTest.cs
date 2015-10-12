using Advexp;
using Foundation;
using NUnit.Framework;
using Security;

namespace TDD
{
    [TestFixture]
    public class ActionsTest
    {
        //------------------------------------------------------------------------------
        string GetSecureLocalValue(string sKey, string sService)
        {
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
        }

        //------------------------------------------------------------------------------
        string GetLocalValue(string sKey)
        {
            var prefs = NSUserDefaults.StandardUserDefaults;
            string sValue = prefs.StringForKey(sKey);

            return sValue;
        }

        //------------------------------------------------------------------------------
        [Test]
        public void LocalTest()
        {
            const string key = "foo";
            const string value = "FooStringValue";

            ActionsTestSettings.LocalFooString = value;
            ActionsTestSettings.Save();

            string loadedValue = GetLocalValue(key);
            Assert.AreEqual(value, loadedValue);

            ActionsTestSettings.LocalFooString = null;
            ActionsTestSettings.Load();
            Assert.AreEqual(value, ActionsTestSettings.LocalFooString);

            ActionsTestSettings.Delete();
            string loadedNullNSUserDefaultsValue = GetLocalValue(key);
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
            ActionsTestSettings.Save();

            string loadedSecureValue = GetSecureLocalValue(secureKey, SettingsConfiguration.KeyChainServiceName);
            Assert.AreEqual(loadedSecureValue, ActionsTestSettings.SecureFooString);
            Assert.AreEqual(loadedSecureValue, secureValue);

            ActionsTestSettings.SecureFooString = null;
            ActionsTestSettings.Load();
            Assert.AreEqual(secureValue, ActionsTestSettings.SecureFooString);

            ActionsTestSettings.Delete();
            string loadedNullSecureValue = GetSecureLocalValue(secureKey, SettingsConfiguration.KeyChainServiceName);
            Assert.IsNull(loadedNullSecureValue);
            Assert.IsNull(ActionsTestSettings.SecureFooString);
        }
    }
}