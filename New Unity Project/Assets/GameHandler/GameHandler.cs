using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] GameObject player;

    private int playerMaxHP;
    private int playerHP;
    private GameObject hpBar;
    // Start is called before the first frame update
    void Start()
    {
        playerMaxHP = player.GetComponent<Health>().getMaxHP();
        playerHP = player.GetComponent<Health>().getHP();
        hpBar = GameObject.Find("Bar");
        hpBar.transform.localScale = new Vector3 (playerHP / playerMaxHP, 1, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMaxHP = player.GetComponent<Health>().getMaxHP();
        playerHP = player.GetComponent<Health>().getHP();
        hpBar.transform.localScale = new Vector3 (((float)playerHP) / playerMaxHP, 1, 1);
        //hpBar.localScale = new Vector3(playerHP / playerMaxHP, 1, 1);
        //healthBar.SetSize(playerHP / playerMaxHP;
    }
}
