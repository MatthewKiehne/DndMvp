using System;
using Connection.Request;
using Connection.Response;
using DndCore.Ability;
using Mirror;
using UnityEngine;

public class Testing : NetworkBehaviour
{

    public GameObject EmptySpritePrefab;
    public GameObject ChildPrefab;

    private Guid testEntityId;

    void Start()
    {
        if (isClient)
        {
            NetworkClient.RegisterPrefab(EmptySpritePrefab);
            NetworkClient.RegisterPrefab(ChildPrefab);
            Debug.Log("Called register");

            NetworkClient.RegisterHandler<CharacterCreationResponse>(OnCharacterCreationResponse);
            NetworkClient.RegisterHandler<GetAbilitiesFromEntityResponse>(OnGetAbilitiesFromEntityResponse);

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

        if(isLocalPlayer && testEntityId != null && Input.GetKeyDown(KeyCode.A))
        {
            GetAbilitiesFromEntity message = new GetAbilitiesFromEntity()
            {
                EntityId = testEntityId
            };
            NetworkClient.Send(message);
        }
    }

    public void OnCharacterCreationResponse(CharacterCreationResponse response)
    {
        testEntityId = response.CharacterId;
        Debug.Log(testEntityId);
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
