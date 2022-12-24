using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class TestConnection
{

    [Command]
    public void TestLua()
    {
        LuaTest test = new LuaTest();
        Debug.Log("Command call");
        test.RunTest();
        Response();
    }

    [ClientRpc]
    public void Response()
    {
        Debug.Log("ClientRpc Call");
    }
}
