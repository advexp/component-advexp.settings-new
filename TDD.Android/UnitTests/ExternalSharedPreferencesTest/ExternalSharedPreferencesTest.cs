using System;
using Android.App;
using Android.Content;
using NUnit.Framework;

namespace TDD
{
    [TestFixture]
    public class ExternalSharedPreferencesTest
    {
        [Test]
        public void Test()
        {
            const String stringKeyName = "string";
            const String intKeyName = "int";

            const String secureStringKeyName = "secureString";
            const String secureIntKeyName = "secureInt";

            const String stringValue = "external string value";
            const Int32 intValue = 12345;

            const String secureStringValue = "secure external string value";
            const Int32 secureIntValue = 54321;

            Context classContext = Application.Context;
            Context fieldContext = Application.Context;

            ISharedPreferences classPrefs = classContext.GetSharedPreferences("classPrefs", FileCreationMode.Private);
            ISharedPreferences fieldPrefs = fieldContext.GetSharedPreferences("fieldPrefs", FileCreationMode.Private);

            var classPrefsEditor = classPrefs.Edit();
            classPrefsEditor.PutString(stringKeyName, stringValue);
            classPrefsEditor.PutString(secureStringKeyName, secureStringValue);
            classPrefsEditor.Commit();

            var fieldPrefsEditor = fieldPrefs.Edit();
            fieldPrefsEditor.PutInt(intKeyName, intValue);
            fieldPrefsEditor.PutInt(secureIntKeyName, secureIntValue);
            fieldPrefsEditor.Commit();

            ExternalSharedPreferencesClassSerializer.SetMySharedPreferences(classPrefs);
            ExternalSharedPreferencesFieldSerializer.SetMySharedPreferences(fieldPrefs);

            ExternalSharedPreferencesSettings.IntValue = 0;
            ExternalSharedPreferencesSettings.StringValue = String.Empty;

            ExternalSharedPreferencesSettings.LoadSettings();

            Assert.AreEqual(ExternalSharedPreferencesSettings.IntValue, intValue);
            Assert.AreEqual(ExternalSharedPreferencesSettings.StringValue, stringValue);
        }
    }
}

