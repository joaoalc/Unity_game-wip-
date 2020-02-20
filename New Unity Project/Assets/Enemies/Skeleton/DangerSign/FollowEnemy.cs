using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    GameObject spawner;
    Vector3 pos_offset;
    // Start is called before the first frame update
    void Start()
    {
        if (spawner == null)
        {
            Destroy(gameObject);
        }
        else
            transform.position = spawner.transform.position + pos_offset;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawner == null)
        {
            Destroy(gameObject);
        }
        else
            transform.position = spawner.transform.position + pos_offset;
    }

    //public void SetStartPosition(GameObject enemy, Vector3 offset)
    //{
    //    transform.position = enemy.transform.position + offset;
    //}

    public void SetSpawner(GameObject enemy, Vector3 offset)
    {
        spawner = enemy;
        pos_offset = offset;


    }
}
