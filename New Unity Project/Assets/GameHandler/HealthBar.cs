using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    //[SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        //Transform bar = transform.Find("Bar");
        //bar.localScale = new Vector3(.4f, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //SetSize(hp / mapHp)
    }
    //public void SetSize(float sizeNormalized)
    //{
    //    //private Transform hpbar = transform.Find("Bar");
    //    bar.localScale = new Vector3(sizeNormalized, 1f);
    //}
}
