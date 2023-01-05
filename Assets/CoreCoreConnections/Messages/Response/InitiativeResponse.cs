using System;
using Mirror;

namespace Connection.Response
{
    public struct InitiativeResponse : NetworkMessage
    {
        // Default
        public int Status;
        public string Error;

        public Guid CharacterId;
        public int value;
    }
}
