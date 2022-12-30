using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using Entities.Search;
using Entities.Features;

public class MyNetManager : NetworkManager
{
    public Sprite sprite;

    public static Dictionary<string, Sprite> SpriteLookup = new Dictionary<string, Sprite>();

    public static readonly string PlayerInitObject = "thisIsATest";

    private Entities.Entity TestEntity;

    public override void OnStartServer()
    {
        SpriteLookup.Add(PlayerInitObject, sprite);
        base.OnStartServer();

        NetworkServer.RegisterHandler<TestMessage>(ConsumeMessage);

        TestEntity = new Entities.Entity();

        string oneString = "one";

        Condition one = new Condition(oneString, 1);
        Feature feature = new Feature("featureName");
        feature.Conditions.Add(one);
        feature.Conditions.Add(one);
        TestEntity.Features.Add(feature);

        SearchEntity se = new SearchEntity();

        List<string> searchTargets  = new List<string>(){
            SearchEntity.FeatureSearchTerm
        };
        List<string> searchTerms = new List<string>(){
            oneString
        };

        EntitySearchQuery query = new EntitySearchQuery(searchTargets, searchTerms);

        EntitySearchResult result = se.Search(TestEntity, query);
        
        List<Condition> c = result[oneString];
        int sum = c.Sum( x => x.Value);
        Debug.Log("Sum: " + sum);
    }

    public void ConsumeMessage(NetworkConnectionToClient conn, TestMessage message){
        Debug.Log(message.value + " " + message.time.Hour + " " + message.id);
        foreach(var i in message.strings)
        {
            Debug.Log(i);
        }
        foreach(var i in message.pairs)
        {
            Debug.Log(i.Value);
        }
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
