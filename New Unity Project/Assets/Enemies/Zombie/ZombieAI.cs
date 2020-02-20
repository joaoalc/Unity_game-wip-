using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    private float speed = 0.03f;
    GameObject player;
    Vector3 playerPos = new Vector3(0f, 0f, 0f);
    Vector3 movement = new Vector3(0f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerPos = player.transform.position;
        movement = playerPos - transform.position;
        movement = new Vector3(movement.x, movement.y, 0);

        movement.Normalize();
        movement *= speed;
        movement = new Vector3(movement.x * 1.25f, movement.y, 0);
        transform.position += movement;
    }
}
