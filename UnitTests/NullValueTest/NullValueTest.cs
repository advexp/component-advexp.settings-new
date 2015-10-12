using NUnit.Framework;

namespace TDD
{
    [TestFixture]
    public class NullValueTest
    {
        [Test]
        public void Test()
        {
            const string strValue = "test";

            NullValueSettings.NullValue = null;
            NullValueSettings.NormalValue = strValue;

            NullValueSettings.SecureNullValue = null;
            NullValueSettings.SecureNormalValue = strValue;

            NullValueSettings.Save();

            NullValueSettings.NullValue = strValue;
            NullValueSettings.NormalValue = null;

            NullValueSettings.SecureNullValue = strValue;
            NullValueSettings.SecureNormalValue = null;

            NullValueSettings.Load();

            Assert.IsTrue(strValue == NullValueSettings.NormalValue);
            Assert.IsNull(NullValueSettings.NullValue);

            Assert.IsTrue(strValue == NullValueSettings.SecureNormalValue);
            Assert.IsNull(NullValueSettings.SecureNullValue);
        }
    }
}

