using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class LookTowardsPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float rotateSpeed = 5f;
    public float moveRadious = 25f;
    public float speed = 1f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distanceToAlien = Vector3.Distance(player.transform.position, transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed*Time.deltaTime);

        if (distanceToAlien <= moveRadious) 
        {
            if (distanceToAlien > 5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else 
            {
                Vector3 moveAwayPosition = transform.position - direction.normalized * speed * Time.deltaTime;
                transform.position = moveAwayPosition;
            }
        }
        
    }
}
