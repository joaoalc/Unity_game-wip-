using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //[SerializeField] int numItems;
    //[SerializeField] string[] overworldNames;
    //int selectedSlot;
    //[SerializeField] GameObject[] Currentitem;
    [SerializeField] GameObject corresponding_object; 
    void Start()
    {
       
        //for(int i = 0; i < numItems; i++)
        //{
        //    if(gameObject.name == overworldNames[i])
        //    {
        //        selectedSlot = i;
        //        break;
        //    }
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getItem()
    {

        return corresponding_object;
        //return Currentitem[selectedSlot];
    }
}
