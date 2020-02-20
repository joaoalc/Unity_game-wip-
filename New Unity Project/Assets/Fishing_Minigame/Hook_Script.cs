using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hook_Script : MonoBehaviour
{
    float time = 0;

    [SerializeField] TextMeshProUGUI score;
    int score_num = 0;

    SpriteRenderer hook_spr;
    // Start is called before the first frame update
    void Start()
    {
        hook_spr = gameObject.GetComponent<SpriteRenderer>();
        hook_spr.color = new Color(0.5f, 0.5f, 0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(hook_spr.color.r != 0.5f)
        {
            hook_spr.color -= new Color(Time.deltaTime, 0, 0, 0);
            //hook_spr.color -= Color.Lerp(hook_spr.color, new Color(0.5f, 0.5f, 0.5f, 1), Time.deltaTime / 2);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        hook_spr.color = new Color(0, 1, 0, 1);
        //Debug.Log("Catching fish!");
        time += Time.deltaTime;
        if(time >= 1)
        {
            //Debug.Log("Fish caught");
            Destroy(collision.gameObject);
            score_num++;
            score.text = score_num.ToString();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        hook_spr.color = new Color(1, 0, 0, 1);
        //Debug.Log("Fish escaped!");
        time = 0;
    }
}
