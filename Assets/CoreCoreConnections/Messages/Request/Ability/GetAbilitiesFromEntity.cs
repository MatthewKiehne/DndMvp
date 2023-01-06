using System;
using Mirror;

namespace Connection.Request
{
    public struct GetAbilitiesFromEntity : NetworkMessage
    {
        public Guid EntityId;
    }
}
