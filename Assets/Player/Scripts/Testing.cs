using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Testing : NetworkBehaviour
{

    public GameObject EmptySpritePrefab;
    public GameObject ChildPrefab;

    private TestingChild t = null;

    void Start()
    {
        if (isServer)
        {
            NetworkClient.RegisterPrefab(EmptySpritePrefab);
            NetworkClient.RegisterPrefab(ChildPrefab);
        }
    }

    void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.W))
        {
            // loadGameObject(MyNetManager.PlayerInitObject);
            TestMessage message = new TestMessage()
            {
                value = "message string here",
                time = System.DateTime.UtcNow.AddHours(-2),
                id = Guid.NewGuid(),
                strings = new List<string>{
                    "here",
                    "is",
                    "a",
                    "list"
                },
                pairs = new Dictionary<string, string>(){
                    {"aKey", "aValue"}
                }
            };
            NetworkClient.Send(message);
        }
    }

    [Command]
    public void loadGameObject(string spriteName)
    {

        if (MyNetManager.SpriteLookup.ContainsKey(spriteName))
        {

            GameObject emptySprite = Instantiate(EmptySpritePrefab, new Vector2(0, 0), Quaternion.identity);
            NetworkServer.Spawn(emptySprite, connectionToClient);

            GameObject childGO = Instantiate(ChildPrefab, Vector3.zero, Quaternion.identity);
            NetworkServer.Spawn(childGO, connectionToClient);

            childGO.transform.SetParent(emptySprite.transform);
            SetParent(emptySprite, childGO);

            SetStuff(childGO);

            Sprite foundSprite = MyNetManager.SpriteLookup[spriteName];
            byte[] bytes = foundSprite.texture.EncodeToPNG();
            RegisterPrefab(bytes, emptySprite);
            Debug.Log("now spawning");
        }
    }

    [ClientRpc]
    public void SetStuff(GameObject childGO)
    {
        t = childGO.GetComponent<TestingChild>();
    }

    [ClientRpc]
    public void SetParent(GameObject parent, GameObject child)
    {
        child.transform.SetParent(parent.transform);
    }

    [ClientRpc]
    public void RegisterPrefab(byte[] imageBytes, GameObject spriteObject)
    {

        Texture2D loadTexture = new Texture2D(256, 256);
        loadTexture.LoadImage(imageBytes);
        Sprite sprite = Sprite.Create(loadTexture, new Rect(0, 0, loadTexture.width, loadTexture.height), Vector2.zero, 10f);

        SpriteRenderer sr = spriteObject.GetComponent<SpriteRenderer>();
        sr.sprite = sprite;
    }

    [ClientRpc]
    public void spawnVisibility(GameObject thingSpawned)
    {

        if (isOwned)
        {
            thingSpawned.SetActive(true);
        }
        else
        {
            thingSpawned.SetActive(false);
        }
    }
}
