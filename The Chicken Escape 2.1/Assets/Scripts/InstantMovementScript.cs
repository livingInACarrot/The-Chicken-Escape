using UnityEngine;
using System.Collections;
public class InstantMovementScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 input;
    private Animator animator;
    private AudioManager audioManager;
    private Rigidbody2D rb;

    // Variable for smoothing the speed parameter
    private float currentSpeed;
    public float smoothTime = 0.1f;
    public float animationSpeed = 1f;

    // NPC variables
    private float maxDist = 6;
    private Vector2 way;
    private float range = 1;
    private float pauseDuration = 4;
    private float NPCmoveSpeed;
    private float timer = 0;
    private bool isMoving;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
        animator.speed = animationSpeed;
        NPCmoveSpeed = moveSpeed / 3;

        if (CompareTag("NPC"))
        {
            NewDestination();
            animator.SetFloat("Speed", 1);
            isMoving = true;
        }
    }

    private void FixedUpdate()
    {
        if (CompareTag("Player"))
            PlayerUpdate();
        else if (CompareTag("NPC"))
            NPCUpdate();
    }
    private void PlayerUpdate()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // Create a movement vector based on the input and the move speed
        Vector2 moveVector = new Vector2(input.x, input.y) * moveSpeed;

        // Apply the movement to the Rigidbody2D component
        rb.velocity = moveVector;

        // Mirror the sprite when moving left
        if (input.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (input.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // No need to use Time.fixedDeltaTime here as we're setting velocity, not manually moving the transform
        // Smoothly transition the speed parameter
        float targetSpeed = rb.velocity.magnitude;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, smoothTime / Time.fixedDeltaTime);
        animator.SetFloat("Speed", currentSpeed);
    }
    private void NPCUpdate()
    {
        if (animator.speed != animationSpeed)
        {
            animator.speed = animationSpeed;
        }

        if (isMoving)
        {
            // Move the NPC towards the destination using Rigidbody2D.MovePosition for smooth movement
            if (Vector2.Distance(transform.position, way) > range)
            {
                rb.MovePosition(Vector2.MoveTowards(rb.position, way, NPCmoveSpeed * Time.fixedDeltaTime));
            }
            else
            {
                animator.SetFloat("Speed", 0);
                isMoving = false;
            }
        }
        else
        {
            timer += Time.fixedDeltaTime;
            if (timer >= pauseDuration)
            {
                NewDestination();
                animator.SetFloat("Speed", 1);
                isMoving = true;
                timer = 0;
            }
        }
    }

    private void NewDestination()
    {
        float minX = 17;
        float maxX = 36;
        float minY = -8;
        float maxY = 3;

        way = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        if (way.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (way.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void NewDestination(Vector2 oldway)
    {
        way = new Vector2(transform.position.x - oldway.x, transform.position.y - oldway.y);
        if (way.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (way.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    /*
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("NPC") && CompareTag("NPC") && isMoving)
        {
            NewDestination(way);
        }
    }
    */
}
