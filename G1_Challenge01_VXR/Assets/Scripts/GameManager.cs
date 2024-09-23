using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject[] aliens;  // Array of alien prefabs
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

    void GenerateAliens() 
    {
        for (int i = 0; i < numberOfAliens; i++)
        {
            // Randomly select road and position for the alien
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

            // Randomly select the alien type and instantiate it
            int randomAlien = Random.Range(0, aliens.Length);
            GameObject alienInstance = Instantiate(aliens[randomAlien], spawnPos, Quaternion.identity);

            // Each alien prefab should already have the AudioSource with correct AudioClip set
        }
    }
}
