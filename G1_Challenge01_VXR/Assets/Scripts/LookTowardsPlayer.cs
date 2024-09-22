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

    string alienName;
    private bool isColliding = false;
    private Animator animator;
    private int doSomeAction = 0;

    public float cooldownTime = 10f;  
    private float timeSinceLastAction = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        alienName = transform.name.Replace("(Clone)", "");
        Debug.Log(alienName);
        if (alienName != "Freaky Alien")
        {
            animator = GetComponent<Animator>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distanceToAlien = Vector3.Distance(player.transform.position, transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed*Time.deltaTime);
        if (distanceToAlien <= moveRadious && distanceToAlien > 5f && !isColliding)
        {
            if (alienName != "Freaky Alien")
            {
                animator.SetBool("isMoving", true);
            }
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            if (alienName != "Freaky Alien")
            {
                animator.SetBool("isMoving", false);
                timeSinceLastAction += Time.deltaTime;

                if (timeSinceLastAction >= cooldownTime)
                {
                    if (Random.Range(0, 20) < 5)
                    {
                        randomMotion();
                        timeSinceLastAction = 0f;
                    }
                }

            }
        }


    }
    private void randomMotion()
    {
        doSomeAction = Random.Range(0, 4);
        animator.SetInteger("doSomeAction", doSomeAction);
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Alien"))
        {
            isColliding = true;
        }
        else
        {
            isColliding = false;
        }
    }
}
