using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VolumePercentage : MonoBehaviour
{
    public AudioSource src;
    TextMeshProUGUI percentageText;

    // Start is called before the first frame update
    void Start()
    {
        percentageText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void textUpdate(float value)
    {

        AudioListener.volume = value;
        percentageText.text = Mathf.RoundToInt(value * 100) + "%";
    }


    // Update is called once per frame
    void Update()
    {
    }
}
