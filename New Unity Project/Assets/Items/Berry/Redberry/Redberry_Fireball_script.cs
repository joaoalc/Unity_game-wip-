using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redberry_Fireball_script : MonoBehaviour
{
    [SerializeField] CircleCollider2D col1;
    [SerializeField] CircleCollider2D col2;


    [SerializeField] int pierce = 2;
    [SerializeField] Animator animator;

    [SerializeField] float seconds_on_ground = 5;
    [SerializeField] float speed = 7f; //Speed of the fireball when moving
    float time_in_air;
    Vector3 target; //Aka: Mouse position at the time
    Vector3 startPos; //Starting position of this object
    Vector3 direction; //Where the fireball is going (normalized)
    // Start is called before the first frame update
    void Awake()
    {
        Vector2 worldInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target = new Vector3(worldInput.x, worldInput.y, transform.position.z);
        startPos = transform.position;
    }

    private void Start()
    {
        direction = target - startPos;
        //Vector2 dir_2d = Vector3.Normalize(new Vector2(direction.x, direction.y));
        //direction = new Vector3(dir_2d.x, dir_2d.y, 0);
        direction = new Vector3(direction.x / 1.25f, direction.y, 0);
        direction = Vector3.Normalize(direction);
        direction = new Vector3(direction.x * 1.25f, direction.y, 0);
        time_in_air = (target.y - startPos.y) / direction.y / speed;
    }

    bool ground_switch = false;
    // Update is called once per frame
    void Update()
    {
        if (time_in_air >= 0) //If the fireball is currently in the air
        {
            transform.position += direction * Time.deltaTime * speed;
            time_in_air -= Time.deltaTime;
        }
        else
        {
            if (!ground_switch)
            {
                //Put object on the ground
                animator.SetBool("Grounded", true);
                ground_switch = true; //For the animator (When it's done)
                col1.enabled = false;
                col2.enabled = true;
            }

            seconds_on_ground -= Time.deltaTime;
            if (seconds_on_ground <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            if (!ground_switch)
            {
                pierce--;
                if(pierce == 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ground_switch)
        {
            collision.gameObject.GetComponent<Health>().Hurt();
        }
    }
}
