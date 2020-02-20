﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderPlaySound : MonoBehaviour, IPointerUpHandler
{
    public AudioSource src;
    public void OnPointerUp(PointerEventData eventData)
    {

        src.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
