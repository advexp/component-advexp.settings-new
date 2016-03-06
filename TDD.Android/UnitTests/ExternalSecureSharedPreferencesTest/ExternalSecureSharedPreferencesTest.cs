using System;
using Android.App;
using Android.Content;
using NUnit.Framework;

namespace TDD
{
    [TestFixture]
    public class ExternalSecureSharedPreferencesTest
    {
        [Test]
        public void Test()
        {
            const String secureStringKeyName = "secureString";
            const String secureIntKeyName = "secureInt";

            const String secureStringValue = "secure external string value";
            const Int32 secureIntValue = 54321;

            Context classContext = Application.Context;
            Context fieldContext = Application.Context;

            ISharedPreferences secureClassPrefs = classContext.GetSharedPreferences("secureClassPrefs", FileCreationMode.Private);
            ISharedPreferences secureFieldPrefs = fieldContext.GetSharedPreferences("secureFieldPrefs", FileCreationMode.Private);

            ExternalSharedPreferencesClassSerializer.SetMySharedPreferences(secureClassPrefs);
            ExternalSharedPreferencesFieldSerializer.SetMySharedPreferences(secureFieldPrefs);

            ExternalSecureSharedPreferencesSettings.SecureIntValue = secureIntValue;
            ExternalSecureSharedPreferencesSettings.SecureStringValue = secureStringValue;

            ExternalSecureSharedPreferencesSettings.SaveSettings();

            var secureString = ActionsTest.GetLocalStringValue(secureStringKeyName, secureClassPrefs);
            var secureInt = ActionsTest.GetLocalStringValue(secureIntKeyName, secureFieldPrefs);

            Assert.AreEqual(secureString, "obuJwLfAYXGNvaGQgH13b8e5MU59swtXbjbu6JtgMzg=");
            Assert.AreEqual(secureInt, "aL85DBFQaXgQS997qnXvpQ==");

            ExternalSecureSharedPreferencesSettings.SecureIntValue = 0;
            ExternalSecureSharedPreferencesSettings.SecureStringValue = null;

            ExternalSecureSharedPreferencesSettings.LoadSettings();

            Assert.AreEqual(ExternalSecureSharedPreferencesSettings.SecureIntValue, secureIntValue);
            Assert.AreEqual(ExternalSecureSharedPreferencesSettings.SecureStringValue, secureStringValue);
            }
    }
}
