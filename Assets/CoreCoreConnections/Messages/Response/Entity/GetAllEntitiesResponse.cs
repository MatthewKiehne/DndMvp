using System.Collections.Generic;
using Entities;
using Mirror;

namespace Connection.Response
{
    public struct GetAllEntitiesResponse : NetworkMessage
    {
        // Default
        public int Status;
        public string Error;
        public List<EntityBrief> Entities;   
    }
}
