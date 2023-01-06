using System;
using Mirror;

namespace Connection.Response
{
    public struct CharacterCreationResponse : NetworkMessage
    {
        public Guid CharacterId;
    }
}
