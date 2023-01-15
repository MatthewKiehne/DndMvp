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
using Lua.Translation;
using MoonSharp.Interpreter;

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
        SpriteLookup.Add(PlayerInitObject, sprite);
        base.OnStartServer();

        // NetworkServer.RegisterHandler<TestMessage>(ConsumeMessage);
        NetworkServer.RegisterHandler<RandomCharacterCreateMessage>(OnCreateCharacter);
        NetworkServer.RegisterHandler<GetAbilitiesFromEntity>(OnGetAbilitiesFromEntity);
        NetworkServer.RegisterHandler<GetAllEntities>(OnGetAllEntities);
        NetworkServer.RegisterHandler<GuidsMessage>(OnGuidsMessage);

        List<LuaTranslate> translations = new List<LuaTranslate>(){
            new Vector2IntTranslate(),
            new GuidTranslate(),
            new AbilityTargetTranslate(),
            new AbilityInputInstructionTranslate()
        };

        foreach (LuaTranslate translate in translations)
        {
            translate.RegisterToLua();
            translate.RegisterFromLua();
        }



        // res.ToObject<List<AbilityInputInstruction>>();



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

    public Guid LogGuid(Guid value)
    {
        Debug.Log(value);
        return value;
    }

    public string LogString(string value)
    {
        Debug.Log(value);
        return value;
    }
    public double LogDouble(double value)
    {
        Debug.Log(value);
        return value;
    }

    public Table logTable(Table table)
    {
        Debug.Log(table);
        return table;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // add player at correct spawn position
        // Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
        GameObject player = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(conn, player);

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

    public void OnGuidsMessage(NetworkConnectionToClient conn, GuidsMessage message)
    {
        switch (message.MessageName)
        {
            case GuidsMessage.GetAbilityInputInstructions:
                GetAbilityInputInstructions(conn, message);
                break;
            default:
                throw new Exception(message.MessageName + " not supported");
        }

    }

    public void GetAbilityInputInstructions(NetworkConnectionToClient conn, GuidsMessage message)
    {
        Guid entityId = message.Guids["entityId"];
        Guid abilityId = message.Guids["abilityId"];

        Entity e = Entities[entityId];

        Ability ability = e.Abilities.Find(a => a.Id.Equals(abilityId));
        List<AbilityInputInstruction> instructions = ability.GetAbilityInputInstructions();

        GetAbilityInputInstructionResponse response = new GetAbilityInputInstructionResponse();
        response.Instructions = instructions;

        conn.Send(response);
    }

    public void OnCreateCharacter(NetworkConnectionToClient conn, RandomCharacterCreateMessage message)
    {
        Debug.Log("started entity creation");

        Entity testEntity = new Entity();
        Feature feature = new Feature("test");
        feature.Conditions.Add(new Condition(AbilityScoreDex, UnityEngine.Random.Range(6, 20)));
        feature.Conditions.Add(new Condition(ProficiencyBonus, 2));
        testEntity.Features.Add(feature);

        Ability ability = new Ability("Unarmed Attack", "A strick without a weapon", AbilityActionType.Action);

        // load the ability text
        TextAsset textAsset = Resources.Load<TextAsset>("LuaScripts/test");
        Script script = new Script();
        script.DoString(textAsset.text);

        // set up debug logic
        Table debugTable = new Table(script);
        debugTable["LogGuid"] = (Func<Guid, Guid>)LogGuid;
        debugTable["LogString"] = (Func<string, string>)LogString;
        debugTable["LogDouble"] = (Func<double, double>)LogDouble;
        debugTable["LogTable"] = (Func<Table, Table>)logTable;
        script.Globals["Debug"] = debugTable;

        // sets the ability's lua scripts
        Closure abilityInstructions = script.Globals.Get("GetAbilityInstructions").Function;
        Closure validateTargets = script.Globals.Get("ValidateAbilityTargets").Function;
        Closure useAbility = script.Globals.Get("UseAbility").Function;
        ability.SetAbilityInfo(abilityInstructions, validateTargets, useAbility);

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

    public void OnGetAllEntities(NetworkConnectionToClient conn, GetAllEntities message)
    {
        GetAllEntitiesResponse response = new GetAllEntitiesResponse();
        response.Entities = Entities.Select(x => new EntityBrief(x.Value)).ToList();
        conn.Send(response);
    }
}
