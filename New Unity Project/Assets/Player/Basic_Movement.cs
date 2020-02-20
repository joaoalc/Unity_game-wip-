using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Movement : MonoBehaviour
{
    //For perks
    PerkVars perks;
    public float perks_speed_mod = 1f; //Speed modifier from perks


    float hspeed = -1; //To fix vertical movement in animator
    [SerializeField] Animator anim;

    //For interactions with items and such; should probably make this an array later
    public float speed_mod = 1f;
    public float speed_multiplier = 1f;
    public float boost_duration = 0;

    //For inventory purposes
    GameObject canvas;
    GameObject item_interacted; //Item that are being interacted with (ex: blueberry bushes)

    
    [SerializeField] GameObject flintItem;
    [SerializeField] float flint_item_cd = 30f / 60;

    [SerializeField]
    private float item_use_cooldown = 0;

    [SerializeField]
    Camera main_camera;

    [SerializeField]
    float move_speed = 5f;
    [SerializeField]
    float x_y_speed_ratio = 1.25f;
    [SerializeField]
    float diagonal_mult = 0.625f;
    [SerializeField]
    float pickup_time = 150f/60;


    float frames_left;
    //[SerializeField]
    bool picking_item = false;
    //[SerializeField]
    bool moving = false;
    //[SerializeField]
    bool using_item = false;

    public float max_invincility_frames = 90/60;

    public float invincibility_frames = 0;

    [SerializeField] private Vector2 screenBounds;

    //To avoid using GetComponent in Update function
    Slots slots;

    // Start is called before the first frame update
    void Start()
    {
        perks = GameObject.Find("PerkVars").GetComponent<PerkVars>();
        perks_speed_mod += perks.perks[0] * perks.perkSpeedModifierPerLevel;

        frames_left = pickup_time;

        canvas = GameObject.Find("Canvas");
        slots = canvas.GetComponent<Slots>();
    }


    // Update is called once per frame
    void Update()
    {
        if(boost_duration <= 0)
        {
            speed_multiplier = 1f;
            boost_duration = -1;
        }
        else if (boost_duration > 0)
        {
            boost_duration-= Time.deltaTime;
        }


        transform.position += Vector3.zero;


        if (invincibility_frames > 0)
        {
            invincibility_frames -= Time.deltaTime;
            //Debug.Log("A");
        }

        //Starting item pickup


        //Moving in all 4 directions
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //transform.position += new Vector3(Input.GetAxis("Horizontal") * move_speed * speed_multiplier * speed_mod * x_y_speed_ratio * diagonal_mult * Time.deltaTime, Input.GetAxis("Vertical") * move_speed * diagonal_mult * Time.deltaTime, 0);
            moving = true;

        }
        //else if (Input.GetAxis("Horizontal") != 0)
        //{
        //    transform.position += new Vector3(Input.GetAxis("Horizontal") *move_speed * speed_multiplier * speed_mod * x_y_speed_ratio * Time.deltaTime, 0, 0);
        //    moving = true;
        //}
        //else if (Input.GetAxis("Vertical") != 0)
        //{
        //    transform.position += new Vector3(0, Input.GetAxis("Vertical") *move_speed * speed_multiplier * speed_mod * Time.deltaTime, 0);
        //    moving = true;
        //}
        else
        {
            moving = false;
        }

        if (moving == true || using_item == true)
        {
            picking_item = false;
            pickup_time = frames_left;
        }
        //if (moving == true || using_item == true)
        //{
        //    picking_item = false;
        //    pickup_time = frames_left;
        //}
        //else
        //{
        //    if (Input.GetKey("space"))
        //    {
        //        picking_item = true;
        //    }
        //}
        
        if (picking_item == true)
        {
            if (pickup_time <= 0) {
                picking_item = false;
                pickup_time = frames_left;
                slots.FindAndAddItem(item_interacted);
            }
            else {
                pickup_time-= Time.deltaTime;
            }
        }
        //Using items
        if (using_item == true)
        {
            picking_item = false;
        //}
        //else if(using_item == true)
        //{
            item_use_cooldown -= Time.deltaTime;
            if(item_use_cooldown <= 0)
            {
                using_item = false;
                item_use_cooldown = flint_item_cd;
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {
            using_item = true;
            flintItem.transform.position = transform.position;
            flintItem.GetComponent<FlintScript>().SetTarget(main_camera.ScreenToWorldPoint( new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)));
            Instantiate(flintItem);
            item_use_cooldown = flint_item_cd;

            //Use an item
            if(slots.getSelectedSlot() > 0)
                slots.useItem(slots.getSelectedSlot() - 1);
            //else
            //    slots.useItem(9);
        }


        //Hotbar slots
        for(int i = 0; i < 10; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                slots.setSelectedSlot(i);
                break;
            }
        }

        //Opening inventory itself
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            slots.toggleInventory();
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal") * move_speed * speed_multiplier * perks_speed_mod * speed_mod * x_y_speed_ratio * diagonal_mult * Time.deltaTime, Input.GetAxis("Vertical") * move_speed * diagonal_mult * Time.deltaTime * speed_multiplier * perks_speed_mod * speed_mod, 0);
            //moving = true;
            anim.SetFloat("Speed", Input.GetAxis("Horizontal"));
            if (Input.GetAxis("Horizontal") > 0)
            {
                hspeed = 1;
            }
            else
            {
                hspeed = -1;
            }
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            transform.position += new Vector3(Input.GetAxis("Horizontal") * move_speed * speed_multiplier * perks_speed_mod * speed_mod * x_y_speed_ratio * Time.deltaTime, 0, 0);
            //moving = true;
            anim.SetFloat("Speed", Input.GetAxis("Horizontal"));
            if (Input.GetAxis("Horizontal") > 0)
            {
                hspeed = 1;
            }
            else
            {
                hspeed = -1;
            }
        }
        else if (Input.GetAxis("Vertical") != 0)
        {
            transform.position += new Vector3(0, Input.GetAxis("Vertical") * move_speed * speed_multiplier * perks_speed_mod * speed_mod * Time.deltaTime, 0);
            //moving = true;
            anim.SetFloat("Speed", hspeed);
        }
        else
        {
            anim.SetFloat("Speed", 0);
            //moving = false;
        }
    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(transform.position.x, -Camera.main.orthographicSize * Screen.width / Screen.height + Camera.main.transform.position.x, Camera.main.orthographicSize * Screen.width / Screen.height + Camera.main.transform.position.x);
        viewPos.y = Mathf.Clamp(transform.position.y, -Camera.main.orthographicSize + Camera.main.transform.position.y, Camera.main.orthographicSize + Camera.main.transform.position.y);
        transform.position = viewPos;
    }
    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("ItemSpawn"))
        {
            if (Input.GetKey("space") && !moving && !using_item)
            {
                picking_item = true;
                item_interacted = col.gameObject;
            }
        }
        else if (col.gameObject.CompareTag("Enemy") && invincibility_frames <= 0)
        {
            gameObject.GetComponent<Health>().DecrementHP();
            invincibility_frames = max_invincility_frames;
        }
        else if (col.gameObject.CompareTag("EnemyProjectile") && invincibility_frames <= 0)
        {
            gameObject.GetComponent<Health>().DecrementHP();
            invincibility_frames = max_invincility_frames;
            Destroy(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("ItemSpawn"))
        {
            if (picking_item)
            {
                picking_item = false;
                pickup_time = frames_left;
            }
        }
    }
}
