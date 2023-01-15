using System;
using System.Collections.Generic;
using Connection.Request;
using Connection.Response;
using DndCore.Ability;
using Mirror;
using MoonSharp.Interpreter;
using UnityEngine;

public class Testing : NetworkBehaviour
{

    public GameObject EmptySpritePrefab;
    public GameObject ChildPrefab;

    private Guid TestEntityId;
    private Guid IdOfEntityToTarget;
    private Guid TestAbilityId;

    void Start()
    {
        if (isClient)
        {
            NetworkClient.RegisterPrefab(EmptySpritePrefab);
            NetworkClient.RegisterPrefab(ChildPrefab);

            NetworkClient.RegisterHandler<CharacterCreationResponse>(OnCharacterCreationResponse);
            NetworkClient.RegisterHandler<GetAbilitiesFromEntityResponse>(OnGetAbilitiesFromEntityResponse);
            NetworkClient.RegisterHandler<GetAllEntitiesResponse>(OnGetAllEntitiesResponse);
            NetworkClient.RegisterHandler<GetAbilityInputInstructionResponse>(OnGetAbilityInputInstructionResponse);

            // RandomCharacterCreateMessage r = new RandomCharacterCreateMessage();
            // Debug.Log(NetworkClient.connection);
            // NetworkClient.Send(r);

        }
    }

    void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.Q))
        {
            RandomCharacterCreateMessage r = new RandomCharacterCreateMessage();
            NetworkClient.Send(r);
        }

        if(isLocalPlayer && TestEntityId != null && Input.GetKeyDown(KeyCode.A))
        {
            GetAbilitiesFromEntity message = new GetAbilitiesFromEntity()
            {
                EntityId = TestEntityId
            };
            NetworkClient.Send(message);
        }
        if(isLocalPlayer && TestEntityId != null && Input.GetKeyDown(KeyCode.Z))
        {
            GetAllEntities message = new GetAllEntities();
            NetworkClient.Send(message);
        }
        if(isLocalPlayer && TestEntityId != null && Input.GetKeyDown(KeyCode.I))
        {
            GuidsMessage message = new GuidsMessage();
            message.MessageName = GuidsMessage.GetAbilityInputInstructions;
            message.Guids = new Dictionary<string, Guid>();
            message.Guids.Add("entityId", TestEntityId);
            message.Guids.Add("abilityId", TestAbilityId);

            NetworkClient.Send(message);
        }
    }

    public void OnCharacterCreationResponse(CharacterCreationResponse response)
    {
        TestEntityId = response.CharacterId;
        Debug.Log(TestEntityId);
    }

    public void OnGetAbilitiesFromEntityResponse(GetAbilitiesFromEntityResponse response)
    {
        if(response.status != 200)
        {
            Debug.Log("something went wrong");
            return;
        }

        foreach(AbilityBrief ability in response.AbilityBriefs)
        {
            Debug.Log("Ability: " + ability.Name);
        }

        TestAbilityId = response.AbilityBriefs[0].Id;
    }

    public void OnGetAllEntitiesResponse(GetAllEntitiesResponse response)
    {
        foreach(var i in response.Entities)
        {
            Debug.Log("Entity: " + i.Id);
            if(!i.Id.Equals(TestEntityId))
            {
                IdOfEntityToTarget = i.Id;
            }
        }
    }

    public void OnGetAbilityInputInstructionResponse(GetAbilityInputInstructionResponse response)
    {
        Debug.Log("Got Response");
    }

    // [Command]
    // public void loadGameObject(string spriteName)
    // {

    //     if (MyNetManager.SpriteLookup.ContainsKey(spriteName))
    //     {
    //         GameObject emptySprite = Instantiate(EmptySpritePrefab, new Vector2(0, 0), Quaternion.identity);
    //         NetworkServer.Spawn(emptySprite, connectionToClient);

    //         GameObject childGO = Instantiate(ChildPrefab, Vector3.zero, Quaternion.identity);
    //         NetworkServer.Spawn(childGO, connectionToClient);

    //         childGO.transform.SetParent(emptySprite.transform);

    //         Sprite foundSprite = MyNetManager.SpriteLookup[spriteName];
    //         byte[] bytes = foundSprite.texture.EncodeToPNG();
    //         RegisterPrefab(bytes, emptySprite);
    //         Debug.Log("now spawning");
    //     }
    // }

    // [ClientRpc]
    // public void RegisterPrefab(byte[] imageBytes, GameObject spriteObject)
    // {

    //     Texture2D loadTexture = new Texture2D(256, 256);
    //     loadTexture.LoadImage(imageBytes);
    //     Sprite sprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height), Vector2.zero, 10f);

    //     SpriteRenderer sr = spriteObject.GetComponent<SpriteRenderer>();
    //     sr.sprite = sprite;
    // }

    // [ClientRpc]
    // public void spawnVisibility(GameObject thingSpawned)
    // {

    //     if (isOwned)
    //     {
    //         thingSpawned.SetActive(true);
    //     }
    //     else
    //     {
    //         thingSpawned.SetActive(false);
    //     }
    // }
}
