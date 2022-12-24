using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEditor;
using UnityEngine;

public class MyNetManager : NetworkManager
{
    public Sprite sprite;

    public static Dictionary<string, Sprite> SpriteLookup = new Dictionary<string, Sprite>();

    public static readonly string PlayerInitObject = "thisIsATest";

    public override void OnStartServer()
    {
        SpriteLookup.Add(PlayerInitObject, sprite);
        base.OnStartServer();

        NetworkServer.RegisterHandler<TestMessage>(ConsumeMessage);
    }

    public void ConsumeMessage(NetworkConnectionToClient conn, TestMessage message){
        Debug.Log(message.value + " " + message.time.Hour + " " + message.id);
        foreach(var i in message.strings)
        {
            Debug.Log(i);
        }
        // foreach(var i in message.pairs)
        // {
        //     Debug.Log(i.Value);
        // }
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // add player at correct spawn position
        // Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
        GameObject player = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player);
        Debug.Log("added player control");

        // spawn ball if two players
        // if (numPlayers == 2)
        // {
        //     ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
        //     NetworkServer.Spawn(ball);
        // }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        // // destroy ball
        // if (ball != null)
        //     NetworkServer.Destroy(ball);

        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }
}
