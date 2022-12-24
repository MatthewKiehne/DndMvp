using System;
using Mirror;

namespace MirrorConverter
{
    public static class GUIDConverter
    {
        public static void WriteGuid(this NetworkWriter writer, Guid guid)
        {
            writer.WriteString(guid.ToString());
        }

        public static Guid ReadGuid(this NetworkReader reader)
        {
            return new Guid(reader.ReadString());
        }
    }
}

