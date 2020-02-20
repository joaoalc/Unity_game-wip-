using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    [SerializeField] Camera main_camera;
    //[SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        main_camera.orthographicSize += 2f;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
