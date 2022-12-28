using System;
using System.Collections.Generic;
using Mirror;

public struct TestMessage : NetworkMessage
{
    public string value;
    public DateTime time;
    public Guid id;
    public List<string> strings;
    public Dictionary<string, string> pairs;
}
