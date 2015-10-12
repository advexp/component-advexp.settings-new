using System;

namespace TDD
{
    public class ClassSerializer : BaseSerializer
    {
        public static Int32 s_CreationCount = 0;

        public ClassSerializer()
        {
            s_CreationCount++;
        }
    }
}
