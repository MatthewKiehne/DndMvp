using System.Collections.Generic;
using DndCore.Ability;
using Mirror;

namespace Connection.Response
{
    public struct GetAbilitiesFromEntityResponse : NetworkMessage
    {
        public int status;
        public string Error;
        public List<AbilityBrief> AbilityBriefs;
    }
}
