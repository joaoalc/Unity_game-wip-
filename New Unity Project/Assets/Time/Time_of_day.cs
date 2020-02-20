using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time_of_day : MonoBehaviour
{
    Image day_image;
    int counter = 0;
    int day = 0;
    [SerializeField] int frames_per_day;
    [SerializeField] int day_time;
    [SerializeField] GameObject clock_pointer;
    [SerializeField] GameObject clock;
    [SerializeField] GameObject day_num;
    [SerializeField] Sprite[] sprites;

    //Enemy spawning
    [SerializeField] int enemy_limit;
    [SerializeField] GameObject zombie_enemy;
    [SerializeField] List<GameObject> zombies = new List<GameObject>();
    GameObject current_zombie_enemy;
    bool zombieArrayAllNull = false;
    [SerializeField] GameObject player;

    [SerializeField] float min_range;
    [SerializeField] float max_range;

    //Possible enemy array
    [SerializeField] int numEnemies;
    [SerializeField] GameObject[] enemies;
    [SerializeField] int[] spawnWeights;
    [SerializeField] int fullWeight = 0;

    GameObject clock_ptr;

    //To reset time when all enemies are killed
    NightTintFollowPlayer nightTintScript;
    CameraFollow camFollow;
    // Start is called before the first frame update
    void Start()
    {
        day_num = GameObject.Find("day_number");
        day_image = day_num.GetComponent<Image>();

        nightTintScript = GameObject.Find("Night Tint").GetComponent<NightTintFollowPlayer>();
        camFollow = Camera.main.GetComponent<CameraFollow>();

        clock_ptr = Instantiate(clock_pointer);
        clock_ptr.transform.SetParent(clock.transform);
        clock_ptr.transform.localScale = new Vector3(0.75f, 1.5f, 1f);
        clock_ptr.transform.localPosition = new Vector3(0f, 0f, 12f);
        day = 1;

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;
        clock_ptr.transform.Rotate(0, 0, (-1.0f/frames_per_day) * 360f * Time.deltaTime * 50f);
        if (counter % frames_per_day == 0)
        {
            day = counter / frames_per_day + 1;
            day_image.sprite = sprites[day % 10];
            for (var i = 0; i < zombies.Count; i++)
            {
                Destroy(zombies[i]);
            }
            zombies.Clear();
        }
        //else if (counter % frames_per_day == day_time)
        //{
        //    for (int i = 0; i < enemy_limit + day; i++)
        //    {

        //        current_zombie_enemy = Instantiate(zombie_enemy);
        //        float enemy_spawn_distance = Random.Range(0.0f, 1.0f) * (max_range - min_range) + min_range;
        //        float enemy_spawn_angle = Random.Range(0.0f, 1.0f) * (2 * Mathf.PI);
        //        current_zombie_enemy.transform.position = new Vector3(enemy_spawn_distance * 1.25f * Mathf.Cos(enemy_spawn_angle), enemy_spawn_distance * Mathf.Sin(enemy_spawn_angle), 0);
        //        current_zombie_enemy.transform.position += player.transform.position;
        //        zombies.Add(current_zombie_enemy);
        //        zombieArrayAllNull = false;
        //    }
        //}
        else if (counter % frames_per_day == day_time)
        {
            for (int i = 0; i < enemy_limit + day; i++)
            {
                int current_roll = Random.Range(1, fullWeight);

                for(int j = 0; j < numEnemies; j++)
                {
                    if(spawnWeights[j] >= current_roll)
                    {
                        current_zombie_enemy = Instantiate(enemies[j]);
                        float enemy_spawn_distance = Random.Range(0.0f, 1.0f) * (max_range - min_range) + min_range;
                        float enemy_spawn_angle = Random.Range(0.0f, 1.0f) * (2 * Mathf.PI);
                        current_zombie_enemy.transform.position = new Vector3(enemy_spawn_distance * 1.25f * Mathf.Cos(enemy_spawn_angle), enemy_spawn_distance * Mathf.Sin(enemy_spawn_angle), 0);
                        current_zombie_enemy.transform.position += player.transform.position;
                        zombies.Add(current_zombie_enemy);
                        zombieArrayAllNull = false;
                        break;
                    }
                }

                //current_zombie_enemy = Instantiate(zombie_enemy);
                //float enemy_spawn_distance = Random.Range(0.0f, 1.0f) * (max_range - min_range) + min_range;
                //float enemy_spawn_angle = Random.Range(0.0f, 1.0f) * (2 * Mathf.PI);
                //current_zombie_enemy.transform.position = new Vector3(enemy_spawn_distance * 1.25f * Mathf.Cos(enemy_spawn_angle), enemy_spawn_distance * Mathf.Sin(enemy_spawn_angle), 0);
                //current_zombie_enemy.transform.position += player.transform.position;
                //zombies.Add(current_zombie_enemy);
                //zombieArrayAllNull = false;
            }
        }
        else if (counter % frames_per_day > day_time)
        {
            zombieArrayAllNull = true;
            for (int i = 0; i < zombies.Count; i++)
            {
                if (zombies[i] != null)
                {
                    zombieArrayAllNull = false;
                }
            }
                if (zombieArrayAllNull)
                {
                    day++;
                    day_image.sprite = sprites[day % 10];
                    clock_ptr.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                    counter = day * frames_per_day;
                    SetTimeAll(counter);
                }
        }
    }

    public int getTime()
    {
        return counter;
    }

    public int getTimeForDay()
    {
        return frames_per_day;
    }
    public int getDayTime()
    {
        return day_time;
    }
    public void SetTimeAll(int time)
    {
        nightTintScript.setTime(time);
        camFollow.setTime(time);
    }
}
