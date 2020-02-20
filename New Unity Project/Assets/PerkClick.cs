using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PerkClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] string perkName = "";

    PerkVars perkVars;

    // Start is called before the first frame update
    void Start()
    {
        perkVars = GameObject.Find("PerkVars").GetComponent<PerkVars>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            perkVars.LevelPerkUp(perkName);
        }
        //if (eventData.button == PointerEventData.InputButton.Middle)
        //{
        //}
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            perkVars.LevelPerkDown(perkName);
        }
    }

}
