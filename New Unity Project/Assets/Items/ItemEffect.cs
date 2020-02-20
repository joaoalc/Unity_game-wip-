using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int UseItem(string name)
    {

        if (name.StartsWith("Blueberry_item"))
        {
            Health healthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

            if(!healthScript.HPEqualsMaxHP())
            {
                healthScript.incrementHP();
            }
            else
            {
                return 10;
            }
            return 0;
        }
        else if (name.StartsWith("Yellowberry_item"))
        {
            Basic_Movement basic_mov = GameObject.FindGameObjectWithTag("Player").GetComponent<Basic_Movement>();

                if (basic_mov.speed_multiplier < 1.25f)
                {
                    basic_mov.speed_multiplier = 1.25f;
                    basic_mov.boost_duration = 240 / 60;
                    return 0;
                }
                else if (basic_mov.speed_multiplier == 1.25f)
                {
                    
                    if(basic_mov.boost_duration < 210 / 60)
                    {
                        basic_mov.boost_duration = 240 / 60;
                        return 0;
                    }
                }
                else
                {
                    return 10;
                }
        }
        else if (name.StartsWith("Redberry_item"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Basic_Movement basic_mov = player.GetComponent<Basic_Movement>();

            GameObject item_to_launch = (Resources.Load("Items/Berry/RedBerry/Redberry_Fireball") as GameObject);
            if(item_to_launch == null)
            {
                Debug.LogError("A");
            }
            item_to_launch.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, item_to_launch.transform.position.z);
            GameObject item_launched = Instantiate(item_to_launch);

            return 0;
        }



        return 10;
    }
}
