using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Entities.Features;
using Entities;
using Connection.Response;
using DndCore.Ability;
using System;
using System.Linq;
using Connection.Request;

public class MyNetManager : NetworkManager
{
    public Sprite sprite;

    public static Dictionary<string, Sprite> SpriteLookup = new Dictionary<string, Sprite>();

    public static readonly string PlayerInitObject = "thisIsATest";

    private Dictionary<Guid, Entity> Entities = new Dictionary<Guid, Entity>();

    private readonly string AbilityScoreDex = "AbilityScore.Dex";
    private readonly string ProficiencyBonus = "proficiency.Bonus";

    public override void OnStartServer()
    {
        Debug.Log("on server start");
        SpriteLookup.Add(PlayerInitObject, sprite);
        base.OnStartServer();

        // NetworkServer.RegisterHandler<TestMessage>(ConsumeMessage);
        NetworkServer.RegisterHandler<RandomCharacterCreateMessage>(connectionId);
        NetworkServer.RegisterHandler<GetAbilitiesFromEntity>(OnGetAbilitiesFromEntity);



        // SearchEntity se = new SearchEntity();

        // List<string> searchTargets = new List<string>(){
        //     SearchEntity.FeatureSearchTerm
        // };
        // List<string> searchTerms = new List<string>(){
        //     oneString
        // };

        // EntitySearchQuery query = new EntitySearchQuery(searchTargets, searchTerms);

        // EntitySearchResult result = se.Search(TestEntity, query);

        // List<Condition> c = result[oneString];
        // int sum = c.Sum(x => x.Value);
        // Debug.Log("Sum: " + sum);
    }

    // public void ConsumeMessage(NetworkConnectionToClient conn, TestMessage message)
    // {
    //     Debug.Log(message.value + " " + message.time.Hour + " " + message.id);
    //     foreach (var i in message.strings)
    //     {
    //         Debug.Log(i);
    //     }
    //     foreach (var i in message.pairs)
    //     {
    //         Debug.Log(i.Value);
    //     }
    // }

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

    public void connectionId(NetworkConnectionToClient conn, RandomCharacterCreateMessage message)
    {
        Debug.Log("started entity creation");

        Entity testEntity = new Entity();
        Feature feature = new Feature("test");
        feature.Conditions.Add(new Condition(AbilityScoreDex, UnityEngine.Random.Range(6, 20)));
        feature.Conditions.Add(new Condition(ProficiencyBonus, 2));
        testEntity.Features.Add(feature);

        Ability ability = new Ability("Unarmed Attack", "A strick without a weapon", AbilityActionType.Action);
        ability.Instructions.Add(new AbilityInputInstruction(AbilityTargetType.Other, 5));
        testEntity.Abilities.Add(ability);

        CharacterCreationResponse response = new CharacterCreationResponse();
        response.CharacterId = testEntity.Id;

        Entities.Add(testEntity.Id, testEntity);
        Debug.Log(conn.connectionId);

        conn.Send(response);
    }

    public void OnGetAbilitiesFromEntity(NetworkConnectionToClient conn, GetAbilitiesFromEntity message)
    {
        GetAbilitiesFromEntityResponse response = new GetAbilitiesFromEntityResponse();
        if (message.EntityId == null || !Entities.ContainsKey(message.EntityId))
        {
            response.status = 404;
            response.Error = "Failed to find Entity";
            conn.Send(response);
        }

        Entity entity = Entities[message.EntityId];
        response.AbilityBriefs = entity.Abilities.Select(a => new AbilityBrief(a)).ToList();
        
        response.status = 200;
        conn.Send(response);
    }
}
