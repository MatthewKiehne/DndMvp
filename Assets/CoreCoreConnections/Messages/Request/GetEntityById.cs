using System;
using Mirror;

namespace DndCore.Message.Request
{
    public struct GetEntityById : NetworkMessage
    {
        public Guid EntityId;
    }
}
