using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTowardsPlayer : MonoBehaviour
{
    public GameObject player;
    public float rotateSpeed = 5f;
    public float moveRadius = 25f;
    public float speed = 1f;

    private string alienName;
    private bool isColliding = false;
    private Animator animator;
    private int doSomeAction = 0;

    public float cooldownTime = 10f;  
    private float timeSinceLastAction = 0f;

    public AudioSource alienAudioSource;  // Reference to AudioSource

    private bool isPlayerLookingAtAlien = false;
    public float audioVolume = 0.3f;      // Set a reasonable volume level for alien audio

    void Start()
{
    // Automatically find the player by tag
    player = GameObject.FindGameObjectWithTag("Player");

    // Get the AudioSource component attached to the alien
    alienAudioSource = GetComponent<AudioSource>();
    if (alienAudioSource == null)
    {
        Debug.LogError("No AudioSource found on alien!");
    }
    else
    {
        alienAudioSource.volume = audioVolume;  // Set default volume
        alienAudioSource.spatialBlend = 0.3f;   // Less 3D effect, closer to 2D audio
        alienAudioSource.minDistance = 1f;      // Set the min distance to avoid quick drop in volume
        alienAudioSource.maxDistance = 20f;     // Set a reasonable max distance
        alienAudioSource.dopplerLevel = 0f;     // Disable Doppler effect to avoid sound shifts
        alienAudioSource.loop = true;           // Ensure the audio loops
    }

    // Handle animator and other alien setup logic
    alienName = transform.name.Replace("(Clone)", "");
    if (alienName != "Freaky Alien")
    {
        animator = GetComponent<Animator>();
    }
}


    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distanceToAlien = Vector3.Distance(player.transform.position, transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Rotate towards the player
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);

        // Move towards player if within range and not colliding
        if (distanceToAlien <= moveRadius && distanceToAlien > 5f && !isColliding)
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

        // Check if the player is looking at the alien
        isPlayerLookingAtAlien = IsPlayerLookingAtAlien(direction);

        // Control audio playback based on whether the player is looking at the alien
        if (isPlayerLookingAtAlien && !alienAudioSource.isPlaying)
        {
            alienAudioSource.Play();
        }
        else if (!isPlayerLookingAtAlien && alienAudioSource.isPlaying)
        {
            alienAudioSource.Pause();  // Pause audio if player looks away
        }
    }

    // Checks if the player is looking at the alien
    private bool IsPlayerLookingAtAlien(Vector3 directionToAlien)
    {
        Vector3 playerForward = player.transform.forward;
        float angle = Vector3.Angle(playerForward, directionToAlien);

        // Check if the angle between the player's forward direction and the alien is within a threshold (e.g., 60 degrees)
        return angle < 60f;
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
