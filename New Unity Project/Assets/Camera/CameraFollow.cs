using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    [SerializeField]
    Vector3 offset;
    float cameraPosition = 0;

    [SerializeField]
    private GameObject player;


    //To get when it's night time
    GameObject gameHandler;
    int time;
    int day_time;
    int frames_per_day;
    //Vector3 last_frame_player_position = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {

        //At night, clamp the player's position to inside the camera
        gameHandler = GameObject.Find("GameHandler");
        time = gameHandler.GetComponent<Time_of_day>().getTime();
        day_time = gameHandler.GetComponent<Time_of_day>().getDayTime();
        frames_per_day = gameHandler.GetComponent<Time_of_day>().getTimeForDay();

        cameraPosition = transform.position.z;
        //last_frame_player_position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    
    void FixedUpdate()
    {
        time++;

        if (time < day_time)
        {
            //offset = 10 * player.transform.position - 10 * transform.position;
            Vector3 desiredPosition = player.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed / 2);
            transform.position = smoothedPosition;
            transform.position = new Vector3(transform.position.x, transform.position.y, cameraPosition);
            //Debug.Log(transform.position - player.transform.position);
        }
        if(time > frames_per_day)
        {
            time = time % frames_per_day;
        }
    }

    public void setTime(int tim)
    {
        time = tim;
    }
}
