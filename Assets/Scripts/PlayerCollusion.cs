using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollusion : MonoBehaviour
{

    public PlayerMovement movement;
    public GameObject gameManager;

    
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Obstacle")
        {
            movement.enabled = false;
            FindObjectOfType<GameManager>().Endgame();

        }
    }
}
