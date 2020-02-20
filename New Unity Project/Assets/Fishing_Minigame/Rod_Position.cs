using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod_Position : MonoBehaviour
{
    //The purpose of the script is to make it so the center top of the rod does not move

    Vector2 rod_top;

    SpriteRenderer rod_spr;

    float y;
    float x;


    Vector2 hook_delta_base;

    Vector2 hook_delta;
    [SerializeField] GameObject hook;

    // Start is called before the first frame update
    void Start()
    {

        rod_spr = gameObject.GetComponent<SpriteRenderer>();
        rod_top = new Vector2(transform.position.x, transform.position.y + rod_spr.size.y / 2);

        hook_delta_base = new Vector2(0, - 1.75f);

        hook = GameObject.Find("Fishing_Hook");
    }

    // Update is called once per frame
    void Update()
    {
        rod_spr.size += new Vector2(0, -Input.GetAxis("Vertical") * Time.deltaTime * 7f);

        rod_spr.size = new Vector2(rod_spr.size.x, Mathf.Clamp(rod_spr.size.y, 0.45f, 9f));

        y = rod_top.y - (rod_spr.size.y / 2 * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.PI / 180.0f));
        x = rod_top.x + (rod_spr.size.y / 2 * Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.PI / 180.0f));
        transform.position = new Vector2(x, y);


        float rotationz = transform.rotation.eulerAngles.z + Input.GetAxis("Horizontal") * Time.deltaTime * 50;
        if(rotationz < 345f && rotationz > 300f)
        {
            rotationz = -15f;
        }
        else if (rotationz > 30f && rotationz < 345f)
        {
            rotationz = 30f;
        }
        //rotationz = Mathf.Clamp(rotationz, -30f, 30f);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationz));
        //transform.position = rod_top - rod_spr.size.y

        hook.transform.position = new Vector3(transform.position.x + (0.20f +  rod_spr.size.y / 2) * Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.PI / 180.0f), transform.position.y - (0.20f + rod_spr.size.y / 2) * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.PI / 180.0f), 0);
        hook.transform.rotation = rod_spr.transform.rotation;




    }
}
