using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biomes : MonoBehaviour
{

    int game_Length = 42;
    int game_width = 32;

    int[,] biome;



    // Start is called before the first frame update
    void Start()
    {
        biome = new int[game_Length, game_width];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
