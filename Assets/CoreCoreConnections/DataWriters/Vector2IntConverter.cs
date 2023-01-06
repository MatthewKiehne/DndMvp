using Mirror;
using UnityEngine;

namespace MirrorConverter
{
    public static class Vector2IntConverter
    {
        public static void WriteVector2IntConverter(this NetworkWriter writer, Vector2Int vector2Int)
        {
            writer.WriteInt(vector2Int.x);
            writer.WriteInt(vector2Int.y);
        }

        public static Vector2Int ReadVector2IntConverter(this NetworkReader reader)
        {
            int x = reader.ReadInt();
            int y = reader.ReadInt();
           return new Vector2Int(x, y);
        }
    }
}
