using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour {
    private float speed = .1f;

    void HandleMovement(){
        if(isLocalPlayer){
            float hor = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(hor, vert, 0);
            transform.position = transform.position + ( movement * speed);
        }
    }

    void Update(){
        HandleMovement();
    }
}