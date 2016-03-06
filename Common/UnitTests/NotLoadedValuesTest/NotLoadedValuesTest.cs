using System;
using NUnit.Framework;

namespace TDD
{
    [TestFixture]
    public class NotLoadedValueTest
    {
        [Test]
        public void Test()
        {
            String refString = "Test";
            DateTime refDateTime = DateTime.Now;
            Int32 refInt32 = 10;

            NotLoadedValuesSettings.DeleteSettings();

            NotLoadedValuesSettings.StringValue = refString;
            NotLoadedValuesSettings.DateTimeValue = refDateTime;
            NotLoadedValuesSettings.Int32Value = refInt32;

            NotLoadedValuesSettings.SecureStringValue = refString;
            NotLoadedValuesSettings.SecureDateTimeValue = refDateTime;
            NotLoadedValuesSettings.SecureInt32Value = refInt32;

            NotLoadedValuesSettings.LoadSettings();

            Assert.AreEqual(NotLoadedValuesSettings.StringValue, refString);
            Assert.AreEqual(NotLoadedValuesSettings.DateTimeValue, refDateTime);
            Assert.AreEqual(NotLoadedValuesSettings.Int32Value, refInt32);

            Assert.AreEqual(NotLoadedValuesSettings.SecureStringValue, refString);
            Assert.AreEqual(NotLoadedValuesSettings.SecureDateTimeValue, refDateTime);
            Assert.AreEqual(NotLoadedValuesSettings.SecureInt32Value, refInt32);
        }
    }
}

