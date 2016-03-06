using System;
using System.Reflection;
using Advexp;
using NUnit.Framework;

namespace TDD
{
    [TestFixture]
    public class DifferentTypesTest
    {
        //------------------------------------------------------------------------------
        public static void CompareSettings(object settings, object refSettings)
        {
            var members = settings.GetType().GetMembers(
                BindingFlags.DeclaredOnly |
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static);


            foreach(var member in members)
            {
                var dummy = member.GetCustomAttribute<BaseSettingAttribute>();
                if (dummy == null)
                {
                    continue;
                }

                ProcessMember(member, settings, refSettings);
            }
        }

        //------------------------------------------------------------------------------
        static void ProcessMember(MemberInfo mi, object settings, object refSettings)
        {
            var value = GetValue(mi, settings);
            var refValue = GetValue(mi, refSettings);

            Assert.AreEqual(refValue, value);
        }

        //------------------------------------------------------------------------------
        static object GetValue(MemberInfo mi, object o)
        {
            var fi = mi as FieldInfo;
            if (fi != null)
            {
                return fi.GetValue(o);
            }

            var pi = mi as PropertyInfo;
            if (pi != null)
            {
                var getMethod = pi.GetGetMethod();

                return getMethod.Invoke(o, new object[0]);
            }

            return null;
        }

        //------------------------------------------------------------------------------
        void RandomizeLocalSettings()
        {
            DifferentTypesLocalSettings.Instance.RandomizeValues();
        }

        //------------------------------------------------------------------------------
        void RandomizeSecureSettings()
        {
            DifferentTypesSecureSettings.Instance.RandomizeValues();
        }

        //------------------------------------------------------------------------------
        [Test]
        public void LocalTest()
        {
            DifferentTypesLocalSettings.SaveSettings();
            RandomizeLocalSettings();
            DifferentTypesLocalSettings.LoadSettings();

            Assert.IsNull(DifferentTypesLocalSettings.Instance.m_NullString);

            CompareSettings(DifferentTypesLocalSettings.Instance, new DifferentTypesLocalSettings());
        }

        //------------------------------------------------------------------------------
        [Test]
        public void SecureTest()
        {
            DifferentTypesSecureSettings.SaveSettings();
            RandomizeSecureSettings();
            DifferentTypesSecureSettings.LoadSettings();

            Assert.IsNull(DifferentTypesSecureSettings.Instance.m_NullString);

            CompareSettings(DifferentTypesSecureSettings.Instance, new DifferentTypesSecureSettings());
        }
    }
}