using Mirror;
using System;
using System.Collections.Generic;

namespace Connection.Request
{
    public struct GuidsMessage : NetworkMessage
    {
        public const string GetAbilityInputInstructions = "GetAbilityInputInstructions";

        public string MessageName;
        public Dictionary<string, Guid> Guids;
    }
}
