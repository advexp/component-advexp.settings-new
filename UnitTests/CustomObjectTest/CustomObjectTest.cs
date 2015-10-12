using System;
using NUnit.Framework;

namespace TDD
{
    [TestFixture]
    public class CustomObjectTest
    {
        public CustomObjectTest()
        {
        }

        [Test]
        public void Test()
        {
            CustomObjectSettings.FooClassInstance = new FooClass();
            CustomObjectSettings.SecureFooClassInstance = new FooClass();

            Random rand = new MyRandom();

            Int32 fooValue = rand.NextInt32();
            Int32 secureFooValue = rand.NextInt32();

            CustomObjectSettings.FooClassInstance.Value = fooValue;
            CustomObjectSettings.SecureFooClassInstance.Value = secureFooValue;

            FooEnum fooEnumValue = (FooEnum)rand.Next((Int32)FooEnum.e_One, (Int32)FooEnum.e_Ten);
            FooEnum secureFooEnumValue = (FooEnum)rand.Next((Int32)FooEnum.e_One, (Int32)FooEnum.e_Ten);

            CustomObjectSettings.FooEnumValue = fooEnumValue;
            CustomObjectSettings.SecureFooEnumValue = secureFooEnumValue;

            CustomObjectSettings.Save();

            CustomObjectSettings.FooClassInstance = null;
            CustomObjectSettings.SecureFooClassInstance = null;
            CustomObjectSettings.FooEnumValue = FooEnum.e_Zero;
            CustomObjectSettings.SecureFooEnumValue = FooEnum.e_Zero;

            CustomObjectSettings.Load();

            Assert.IsNotNull(CustomObjectSettings.FooClassInstance);
            Assert.IsTrue(typeof(FooClass) == CustomObjectSettings.FooClassInstance.GetType());
            Assert.AreEqual(CustomObjectSettings.FooClassInstance.Value, fooValue);
            Assert.AreEqual(CustomObjectSettings.FooEnumValue, fooEnumValue);

            Assert.IsNotNull(CustomObjectSettings.SecureFooClassInstance);
            Assert.IsTrue(typeof(FooClass) == CustomObjectSettings.SecureFooClassInstance.GetType());
            Assert.AreEqual(CustomObjectSettings.SecureFooClassInstance.Value, secureFooValue);
            Assert.AreEqual(CustomObjectSettings.SecureFooEnumValue, secureFooEnumValue);
        }
    }
}

