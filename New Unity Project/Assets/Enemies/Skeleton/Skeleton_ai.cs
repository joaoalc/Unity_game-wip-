using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_ai : MonoBehaviour
{
    //Animator stuff
    Animator dan_sign_anim;

    FollowEnemy followEnemy;
    //ArrowScript arrowScript;


    GameObject player;
    GameObject arrow;
    GameObject danger_sign;
    [SerializeField] float max_shot_cd = 3;
    float cur_shot_cd;
    [SerializeField] float danger_cd = 1;
    Vector3 danger_sign_offset = new Vector3(0, 1.5f, 0);

    GameObject dan_sign;
    GameObject arr;

    [SerializeField] float speed = 1.5f;


    SpriteRenderer dan_sign_spr;


    bool danger_sign_up = false;
    // Start is called before the first frame update
    void Awake()
    {
        arrow = Resources.Load("Enemies/Skeleton/Arrow/Arrow")as GameObject;
        danger_sign = Resources.Load("Enemies/Skeleton/DangerSign") as GameObject;
        dan_sign = Instantiate(danger_sign);
        dan_sign_spr = dan_sign.GetComponent<SpriteRenderer>();
        FollowEnemy followEnemy = dan_sign.GetComponent<FollowEnemy>();
        followEnemy.SetSpawner(gameObject, danger_sign_offset);
        //dan_sign.SetActive(false);
        dan_sign_spr.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        cur_shot_cd = max_shot_cd;

        dan_sign_anim = dan_sign.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        cur_shot_cd-= Time.deltaTime;
        if(cur_shot_cd <= danger_cd && !danger_sign_up)
        {
            //dan_sign = Instantiate(danger_sign);
            //followEnemy.SetSpawner(gameObject, danger_sign_offset);
            //dan_sign.SetActive(true);
            dan_sign_spr.enabled = true;
            dan_sign.transform.position = gameObject.transform.position + danger_sign_offset;
            //followEnemy.SetStartPosition(gameObject, danger_sign_offset);
            danger_sign_up = true;
            dan_sign_anim.Play("DangerSign", -1, 0f);
        }
        else if(cur_shot_cd <= 0)
        {

            if(arr == null)
            { //Shoot arrow if there isn't one
                arrow.transform.position = transform.position;
                arr = Instantiate(arrow);
                //arr.transform.SetParent(gameObject.transform);
                arr.GetComponent<ArrowScript>().setTarget(player.transform.position);
                //dan_sign.SetActive(false);
                dan_sign_spr.enabled = false;
                cur_shot_cd = max_shot_cd;
                danger_sign_up = false;
                if (arr == null)
                {
                    Debug.Log("A");
                }
            }
            else //Stall shot if there's already one
            {
                cur_shot_cd = 0.01f * Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement_dir;
        Vector3 player_pos = player.transform.position;
        movement_dir = Vector3.Normalize(new Vector3(-player.transform.position.y + transform.position.y, +player.transform.position.x - transform.position.x, 0));
        movement_dir = new Vector3(movement_dir.x * 1.25f, movement_dir.y, movement_dir.z);
        transform.position += movement_dir * speed * Time.deltaTime;

    }

    //public void DestroySkeleton()
    //{
    //    if (dan_sign != null)
    //        Destroy(dan_sign);
    //    else
    //        Debug.LogError("Skeleton without danger sign destroyed.");
    //    Destroy(gameObject);
    //}
}
