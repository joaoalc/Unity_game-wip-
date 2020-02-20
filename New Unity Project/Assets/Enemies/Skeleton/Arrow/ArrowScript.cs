using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{


    Vector3 target;
    Vector3 direction;
    [SerializeField] float lifespan_left;
    [SerializeField] float speed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        target += (target - transform.position);
        direction = target - transform.position;
        direction.x /=  1.25f;
        direction.Normalize();
        direction.x *= 1.25f;
        lifespan_left = (float)(Vector3.Magnitude(target - transform.position) / speed / direction.magnitude);

        Vector2 angleVec = target - transform.position;
        transform.eulerAngles = new Vector3(0f, 0f, Vector2.Angle(angleVec, new Vector2(1f, 0f)));
        if(angleVec.y < 0)
        {
            transform.eulerAngles = -transform.eulerAngles;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lifespan_left <= 0 && lifespan_left > -10)
        {
            //transform.position = target;
            lifespan_left = -12;
        }
        else if (lifespan_left > 0)
        {
            transform.position += direction * speed * Time.deltaTime;
            lifespan_left-= Time.deltaTime;
        }
        else if (lifespan_left <= -10)
        {
            Destroy(gameObject);
        }
    }

    public void setTarget(Vector3 mouse_target)
    {
        target = mouse_target;
    }

}
