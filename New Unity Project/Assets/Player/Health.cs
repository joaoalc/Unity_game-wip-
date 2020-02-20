using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    //For perks
    PerkVars perks;

    [SerializeField] private int maxHP = 4;
    [SerializeField] private int HP = 4;

    // Start is called before the first frame update
    void Start()
    {
        perks = GameObject.Find("PerkVars").GetComponent<PerkVars>();
        maxHP += perks.perks[1];
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getHP()
    {
        return HP;
    }
    public int getMaxHP()
    {
        return maxHP;
    }
    public void setHP(int newHP)
    {
        if(newHP > maxHP)
        {
            HP = maxHP;
        }
        HP = newHP;
    }
    public void setMaxHP(int newMaxHP)
    {
        maxHP = newMaxHP;
        if(HP > maxHP)
        {
            HP = maxHP;
        }
    }
    public void incrementHP()
    {
        if(HP != maxHP)
            HP++;
    }
    public int incrementMaxHP()
    {
        maxHP++;
        HP++;
        return maxHP;
    }
    public int DecrementHP()
    {
        HP--;
        if(HP < 0)
        {
            HP = 0;
            //Debug.Log("Extra ded.");
        }
        return HP;
    }

    public bool HPEqualsMaxHP()
    {
        if(HP == maxHP)
        {
            return true;
        }
        return false;
    }

    public void Hurt()
    {
        if (gameObject.GetComponent<Basic_Movement>().invincibility_frames <= 0)
        {
            gameObject.GetComponent<Health>().DecrementHP();
            gameObject.GetComponent<Basic_Movement>().invincibility_frames = gameObject.GetComponent<Basic_Movement>().max_invincility_frames;
        }
    }
}
