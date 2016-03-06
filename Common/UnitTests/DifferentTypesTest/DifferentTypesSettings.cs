using System;
using System.Collections.Generic;
using Advexp;

namespace TDD
{
    public enum EEnumValues
    {
        e_Zero,
        e_One,
        e_Two,
        e_Three,
        e_Four,
        e_Five,
        e_Six,
        e_Seven,
        e_Eight,
        e_Nine,
        e_Ten,
    }

    public class DifferentTypesLocalSettings : Advexp.Settings<TDD.DifferentTypesLocalSettings>
    {
        [Setting]
        public Boolean m_Boolean = false;
        [Setting]
        public Char m_Char = Char.MaxValue;
        [Setting]
        public Byte m_Byte = Byte.MaxValue;
        [Setting]
        public SByte m_SByte = SByte.MaxValue;
        [Setting]
        public Int16 m_Int16 = Int16.MaxValue;
        [Setting]
        public UInt16 m_UInt16 = UInt16.MaxValue;
        [Setting]
        public Int32 m_Int32 = Int32.MaxValue;
        [Setting]
        public UInt32 m_UInt32 = UInt32.MaxValue;
        [Setting]
        public Int64 m_Int64 = Int64.MaxValue;
        [Setting]
        public UInt64 m_UInt64 = UInt64.MaxValue;
        [Setting]
        public Single m_Single = Single.MaxValue;
        [Setting]
        public Double m_Double = Double.MaxValue;
        [Setting]
        public Decimal m_Decimal = Decimal.MaxValue;
        [Setting]
        public DateTime m_DateTime = DateTime.MaxValue;
        [Setting]
        public String m_String = String.Empty;
        [Setting]
        public EEnumValues m_Enum = EEnumValues.e_Zero;
        [Setting]
        public Int32? m_NullableInt32 = null;
        [Setting]
        public String m_NullString = null;
        [Setting]
        public Dictionary<String, Object> m_Dict = new Dictionary<String, Object>();
        //[Setting]
        public List<Int32> m_List = new List<Int32> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        //------------------------------------------------------------------------------
        public void RandomizeValues()
        {
            Random rand = new MyRandom();

            m_Boolean = rand.NextBoolean();
            m_Char = (Char)rand.Next(Char.MinValue, Char.MaxValue);
            m_Byte = (Byte)rand.Next(Byte.MinValue, Byte.MaxValue);
            m_SByte = (SByte)rand.Next(SByte.MinValue, SByte.MaxValue);
            m_Int16 = (Int16)rand.Next(Int16.MinValue, Int16.MaxValue);
            m_UInt16 = (UInt16)rand.Next(UInt16.MinValue, UInt16.MaxValue);
            m_Int32 = rand.NextInt32();
            m_UInt32 = (UInt32)rand.Next(0, Int32.MaxValue);
            m_Int64 = rand.NextInt32();
            m_UInt64 = (UInt64)rand.Next(0, Int32.MaxValue);
            m_Single = rand.NextSingle();
            m_Double = rand.NextDouble();
            m_Decimal = rand.NextDecimal();
            m_DateTime = rand.NextDateTime();
            m_String = rand.NextString(8);
            m_Enum = (EEnumValues)rand.Next(0, 10);

            if (rand.NextBoolean())
            {
                m_NullableInt32 = null;
            }
            else
            {
                m_NullableInt32 = rand.NextInt32();
            }

            m_NullString = null;

            m_List = new List<Int32>()
            {
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
            };


            m_Dict = new Dictionary<String, Object>();
            m_Dict.Add("one", 1);
            m_Dict.Add("two", 2);
            m_Dict.Add("three", 3);
        }
    }

    public class DifferentTypesSecureSettings : Advexp.Settings<TDD.DifferentTypesSecureSettings>
    {
        [Setting(Secure = true)]
        public Boolean m_Boolean = false;
        [Setting(Secure = true)]
        public Char m_Char = Char.MaxValue;
        [Setting(Secure = true)]
        public Byte m_Byte = Byte.MaxValue;
        [Setting(Secure = true)]
        public SByte m_SByte = SByte.MaxValue;
        [Setting(Secure = true)]
        public Int16 m_Int16 = Int16.MaxValue;
        [Setting(Secure = true)]
        public UInt16 m_UInt16 = UInt16.MaxValue;
        [Setting(Secure = true)]
        public Int32 m_Int32 = Int32.MaxValue;
        [Setting(Secure = true)]
        public UInt32 m_UInt32 = UInt32.MaxValue;
        [Setting(Secure = true)]
        public Int64 m_Int64 = Int64.MaxValue;
        [Setting(Secure = true)]
        public UInt64 m_UInt64 = UInt64.MaxValue;
        [Setting(Secure = true)]
        public Single m_Single = Single.MaxValue;
        [Setting(Secure = true)]
        public Double m_Double = Double.MaxValue;
        [Setting(Secure = true)]
        public Decimal m_Decimal = Decimal.MaxValue;
        [Setting(Secure = true)]
        public DateTime m_DateTime = DateTime.MaxValue;
        [Setting(Secure = true)]
        public String m_String = String.Empty;
        [Setting(Secure = true)]
        public EEnumValues m_Enum = EEnumValues.e_Zero;
        [Setting(Secure = true)]
        public Int32? m_NullableInt32 = null;
        [Setting(Secure = true)]
        public String m_NullString = null;
        [Setting(Secure = true)]
        public List<Int32> m_List = new List<Int32> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        //------------------------------------------------------------------------------
        public void RandomizeValues()
        {
            Random rand = new MyRandom();

            m_Boolean = rand.NextBoolean();
            m_Char = (Char)rand.Next(Char.MinValue, Char.MaxValue);
            m_Byte = (Byte)rand.Next(Byte.MinValue, Byte.MaxValue);
            m_SByte = (SByte)rand.Next(SByte.MinValue, SByte.MaxValue);
            m_Int16 = (Int16)rand.Next(Int16.MinValue, Int16.MaxValue);
            m_UInt16 = (UInt16)rand.Next(UInt16.MinValue, UInt16.MaxValue);
            m_Int32 = rand.NextInt32();
            m_UInt32 = (UInt32)rand.Next(0, Int32.MaxValue);
            m_Int64 = rand.NextInt32();
            m_UInt64 = (UInt64)rand.Next(0, Int32.MaxValue);
            m_Single = rand.NextSingle();
            m_Double = rand.NextDouble();
            m_Decimal = rand.NextDecimal();
            m_DateTime = rand.NextDateTime();
            m_String = rand.NextString(8);
            m_Enum = (EEnumValues)rand.Next(0, 10);

            if (rand.NextBoolean())
            {
                m_NullableInt32 = null;
            }
            else
            {
                m_NullableInt32 = rand.NextInt32();
            }

            m_NullString = null;

            m_List = new List<Int32>()
            {
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
                rand.NextInt32(),
            };
        }
    }
}
