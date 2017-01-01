using System;
using System.Collections.Generic;
using Advexp;
using NUnit.Framework;

namespace TDD
{
    [TestFixture]
    public class FormatMigrationTest
    {
        //------------------------------------------------------------------------------
        [TestFixtureSetUp]
        public void Setup()
        {
            #if __ANDROID__
            SettingsConfiguration.KeyStoreFileProtectionPassword = "password";
            SettingsConfiguration.KeyStoreFileName = "keystore";
            #endif // __ANDROID__
            SettingsConfiguration.DisableFormatMigration = false;
        }

        //------------------------------------------------------------------------------
        [Test]
        public void LocalTest_V1_V2()
        {
            DifferentTypesLocalSettings_V1.DeleteSettings();
            DifferentTypesLocalSettings.DeleteSettings();

            var savedSettings_V1 = new DifferentTypesLocalSettings_V1();
            savedSettings_V1.SaveObjectSettings();

            var loadedSettings_V2 = new DifferentTypesLocalSettings();
            loadedSettings_V2.LoadObjectSettings();

            DifferentTypesTest.CompareSettings(loadedSettings_V2, savedSettings_V1);
        }

        #if __IOS__
        //------------------------------------------------------------------------------
        [Test]
        public void SecureTest_V1_V2()
        {
            DifferentTypesSecureSettings_V1.DeleteSettings();
            DifferentTypesSecureSettings.DeleteSettings();

            var savedSettings_V1 = new DifferentTypesSecureSettings_V1();
            savedSettings_V1.SaveObjectSettings();

            var loadedSettings_V2 = new DifferentTypesSecureSettings();
            loadedSettings_V2.LoadObjectSettings();

            DifferentTypesTest.CompareSettings(loadedSettings_V2, savedSettings_V1);
        }
        #endif // __IOS__
    }
}

