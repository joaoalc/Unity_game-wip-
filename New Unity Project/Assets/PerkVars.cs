using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PerkVars : MonoBehaviour
{
    public float perkSpeedModifierPerLevel = 0.15f;

    public List<int> perks; //Nível atual de cada perk
    public List<int> maxPerks; //Nível maximo de cada perk
    public List<int> minPerks; //Minimum level of each perk
    [SerializeField] List<int> perkCosts; //Cost of each perk 
    //[SerializeField] TextMeshProUGUI SpeedBoostPerkText;

    [SerializeField] TextMeshProUGUI[] PerkTexts;

    [SerializeField] int maxPerkPoints = 15;
    [SerializeField] int perkPoints;
    // Start is called before the first frame update
    void Awake()
    {
        perkPoints = maxPerkPoints;
        DontDestroyOnLoad(gameObject);
        //SpeedBoostPerkText = TextMeshProUGUI.Find("SpeedBoostPerkText");
    }

    public void LevelPerkUp(string perk_name)
    {
        int i = -1;

        switch (perk_name)
        {
            case "Speed Boost":
                i = 0;
                break;
            case "HP Boost":
                i = 1;
                break;
        }

        if(i < 0)
        {
            Debug.LogError("Negative perk number found.");
        }
        else if(perks[i] < maxPerks[i] && perkCosts[i] <= perkPoints)
        {
            perks[i]++;

            PerkTexts[i].text = perks[i] + "/" + maxPerks[i];


            perkPoints -= perkCosts[i];
        }
    }

    public void LevelPerkDown(string perk_name)
    {
        int i = -1;

        if (perk_name == "")
        {
            Debug.LogError("Invaliid perk name found (Probably didn't assign to the button in editor)");
            return;
        }

        switch (perk_name)
        {
            case "Speed Boost":
                i = 0;
                break;
            case "HP Boost":
                i = 1;
                break;
            default:
                Debug.LogError("Invalid perk name in perk button.");
                break;
        }

        if (i < 0)
        {
            Debug.LogError("Negative perk number found.");
        }
        else if (perks[i] > minPerks[i])
        {
            perks[i]--;
            PerkTexts[i].text = perks[i] + "/" + maxPerks[i];
            perkPoints += perkCosts[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
