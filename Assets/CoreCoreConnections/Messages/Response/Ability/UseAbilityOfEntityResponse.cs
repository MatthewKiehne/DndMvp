using Mirror;

namespace Connection.Response
{
    public struct UseAbilityOfEntityResponse : NetworkMessage
    {
        public int Status;
        public string Error;
    }
}
