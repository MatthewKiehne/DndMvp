using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class TestingAgain : NetworkBehaviour {


    [Command]
    public void serverCall(){
        Debug.Log("Command found");
        response();
    }

    [ClientRpc]
    public void response()
    {
        Debug.Log("Respond to client");
        // NetworkClient.RegisterPrefab()
    }
    
}

