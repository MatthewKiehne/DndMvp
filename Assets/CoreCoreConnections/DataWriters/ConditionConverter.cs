using Entities.Features;
using Mirror;

namespace MirrorConverter
{
    public static class ConditionConverter
    {
        public static void WriteConditionConverter(this NetworkWriter writer, Condition condition)
        {
            writer.WriteString(condition.Name);
            writer.WriteInt(condition.Value);
        }

        public static Condition ReadConditionConverter(this NetworkReader reader)
        {
           return new Condition(reader.ReadString(), reader.ReadInt());
        }
    }
}
