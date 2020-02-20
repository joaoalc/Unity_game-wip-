using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlintScript : MonoBehaviour
{
    public Vector3 target;
    private float speed = 0.4f;
    private Vector3 direction;

    private int lifespan_left;
    // Start is called before the first frame update
    void Start()
    {
        target = new Vector3(target.x, target.y, transform.position.z);
        direction = target - transform.position;
        direction = new Vector3(direction.x / 1.25f, direction.y, 0f);
        direction.Normalize();
        direction = new Vector3(direction.x * 1.25f, direction.y, 0f);
        lifespan_left = (int) (Vector3.Magnitude(target - transform.position) / speed / direction.magnitude);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lifespan_left == 0)
        {
            transform.position = target;
            lifespan_left--;
        }
        else if (lifespan_left > 0)
        {
            transform.position += direction * speed;
            lifespan_left--;
        }
        else if(lifespan_left == -1)
        {
            Destroy(gameObject);
        }
    }

    bool destroyed = false;
    private void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.gameObject.CompareTag("Enemy") && !destroyed)
        {
            //foreach (Transform child in col.gameObject.transform)
            //{
            //    Debug.Log(child.gameObject.name);
            //    Destroy(child.gameObject);
            //}
            //Debug.Log("meme");
            Destroy(col.gameObject);
            destroyed = true;
            Destroy(gameObject);
        }
    }

    public void SetTarget(Vector3 tgt)
    {
        target = tgt;
    }
    
    
}
