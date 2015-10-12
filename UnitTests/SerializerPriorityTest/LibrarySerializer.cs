using System;

namespace TDD
{
    public class LibrarySerializer : BaseSerializer
    {
        public static Int32 s_CreationCount = 0;

        public LibrarySerializer()
        {
            s_CreationCount++;
        }
    }
}

