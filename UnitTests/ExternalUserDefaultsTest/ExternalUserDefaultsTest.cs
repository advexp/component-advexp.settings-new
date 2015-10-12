using System;
using Foundation;
using NUnit.Framework;

namespace TDD
{
    [TestFixture]
    public class ExternalUserDefaultsTest
    {
        [Test]
        public void Test()
        {
            const String stringValue = "external string value";
            const Int32 int32Value = 12345;

            const String stringKeyName = "string";
            const String intKeyName = "int";

            var classPrefs = new NSUserDefaults();

            classPrefs.SetString(stringValue, stringKeyName);
            ExternalUserDefaultsClassSerializer.SetMyUserDefaults(classPrefs);

            var fieldPrefs = new NSUserDefaults();

            fieldPrefs.SetInt(int32Value, intKeyName);
            ExternalUserDefaultsFieldSerializer.SetMyUserDefaults(fieldPrefs);

            ExternalUserDefaultsSettings.Load();

            Assert.AreEqual(ExternalUserDefaultsSettings.Int32Value, int32Value);
            Assert.AreEqual(ExternalUserDefaultsSettings.StringValue, stringValue);

        }
    }
}

