using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{

    List<GameObject> fish = new List<GameObject>();
    float time = 0;
    [SerializeField] int fish_spawn_time = 3;

    // Start is called before the first frame update
    void Start()
    {
        fish.Add(Resources.Load("Fishing_Minigame/Fish_1") as GameObject);

        //score = TextMeshProUGUI.;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= fish_spawn_time)
        {
            fish[0].transform.position = new Vector3(-6, -2, 0);
            Instantiate(fish[0]);
            time -= fish_spawn_time;
        }
    }
}
