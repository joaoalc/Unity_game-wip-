using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerksMenu : MonoBehaviour
{

    [SerializeField] GameObject MainMenuObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BackButton()
    {

        MainMenuObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
