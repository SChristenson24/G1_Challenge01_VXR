using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] aliens;

    public int numberOfAliens = 100;


    private float road1WidthX1 = -26f;
    private float road1WidthX2 = -43f;
    private float road1LengthZ1 = -32f;
    private float road1LengthZ2 = 66f;


    private float road2WidthX1 = -52f;
    private float road2WidthX2 = 5f;
    private float road2LengthZ1 = 29f;
    private float road2LengthZ2 = 35f;

    void Start()
    {
        GenerateAliens();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateAliens () 
    {
        for (int i = 0; i < numberOfAliens; i++)
        {
            int randomRoad = Random.Range(0, 2);
            Vector3 spawnPos = new Vector3(0, 0, 0);
            if (randomRoad == 0)
            {
                float randomX = Random.Range(road1WidthX2, road1WidthX1);
                float randomZ = Random.Range(road1LengthZ1, road1LengthZ2);
                spawnPos = new Vector3(randomX, 1, randomZ);
            }
            else
            {
                float randomX = Random.Range(road2WidthX2, road2WidthX1);
                float randomZ = Random.Range(road2LengthZ1, road2LengthZ2);
                spawnPos = new Vector3(randomX, 1, randomZ);
            }

            int randomAlien = Random.Range(0, aliens.Length);

            Instantiate(aliens[randomAlien], spawnPos, Quaternion.identity);

        }
    }
}
