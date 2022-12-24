using System;
using Mirror;

namespace MirrorConverter
{
    public static class DateTimeReaderWriter
    {
        public static void WriteDateTime(this NetworkWriter writer, DateTime dateTime)
        {
            writer.WriteLong(dateTime.Ticks);
        }

        public static DateTime ReadDateTime(this NetworkReader reader)
        {
            return new DateTime(reader.ReadLong());
        }
    }
}

