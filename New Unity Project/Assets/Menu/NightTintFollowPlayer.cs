using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightTintFollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float baseScale = 10f;
    [SerializeField] float modifier = 0.5f;
    [SerializeField] GameObject gameHandler;
    int counter = 0;
    int ingame_time_counter = 0;
    [SerializeField] int cycleTime = 80;


    int day_time = 0;
    int time_per_day = 0;
    SpriteRenderer sprRender;
    // Start is called before the first frame update
    void Start()
    {
        sprRender = GetComponent<SpriteRenderer>();
        ingame_time_counter = gameHandler.GetComponent<Time_of_day>().getTime();
        day_time = gameHandler.GetComponent<Time_of_day>().getDayTime();
        time_per_day = gameHandler.GetComponent<Time_of_day>().getTimeForDay();
        if(ingame_time_counter + 80 < day_time)
        {
            sprRender.color = new Color(sprRender.color.r, sprRender.color.g, sprRender.color.b, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        if (counter % cycleTime == 0)
        {
            counter = 1;
        }
        transform.position = player.transform.position;
        transform.localScale = new Vector3(baseScale + modifier * Mathf.Cos(counter * 2 * Mathf.PI / cycleTime), baseScale + modifier * Mathf.Cos(counter * 2 * Mathf.PI / cycleTime), 1f);
    }
    void FixedUpdate()
    {
        ingame_time_counter++;
        if(ingame_time_counter  + 60 > day_time && ingame_time_counter < day_time) //Se é quase noite
        {
            sprRender.color = new Color(sprRender.color.r, sprRender.color.g, sprRender.color.b, 1f / 90f * (90 - (day_time - ingame_time_counter)));
        }
        else if(ingame_time_counter >= time_per_day)
        {
            ingame_time_counter = 0;
            sprRender.color = new Color(1f, 1f, 1f, 0f);
        }
        else if (ingame_time_counter + 60 > time_per_day && ingame_time_counter < time_per_day) //Se é quase noite
        {
            sprRender.color = new Color(sprRender.color.r, sprRender.color.g, sprRender.color.b, 1f / 90f * (time_per_day - ingame_time_counter));
        }
    }
    public void setTime(int time)
    {
        ingame_time_counter = time;
    }
}
