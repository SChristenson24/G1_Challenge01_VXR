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
        //Getting the player object using tags
        player = GameObject.FindGameObjectWithTag("Player");

        alienName = transform.name.Replace("(Clone)", "");

        //Freaky Alien doesn't have animations, so this if statement makes sure we don't call on animator for it
        if (alienName != "Freaky Alien")
        {
            animator = GetComponent<Animator>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position; //Gets the direction alien has to move
        float distanceToAlien = Vector3.Distance(player.transform.position, transform.position);

        //Code to make the alien look towards the player
        Quaternion lookRotation = Quaternion.LookRotation(direction); 
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed*Time.deltaTime);

        //Moving towards alien if outside a certain radious, but inside another
        //Not moving if colliding because that creates issues with animations and collisions
        if (distanceToAlien <= moveRadious && distanceToAlien > 5f && !isColliding)
        {
            if (alienName != "Freaky Alien")
            {
                //Setting the animation true when moving towards player
                animator.SetBool("isMoving", true);
            }

            //Moving towards player
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            //Else statement when not moving
            //Alien is near the player now
            //------ Add the sounds here ------ 
            if (alienName != "Freaky Alien")
            {
                //Setting moving animations false
                animator.SetBool("isMoving", false);

                //This code block is for making the aliens have random animations and actions sometimes
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
        //Detecting Collisions
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
