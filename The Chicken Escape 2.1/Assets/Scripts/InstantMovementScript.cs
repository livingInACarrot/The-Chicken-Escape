using UnityEngine;
using System.Collections;

public class InstantMovementScript : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 input;
    private Animator animator;
    private AudioManager audioManager;
    private Rigidbody2D rb;
    private ChickenInteractions chick;

    // Variable for smoothing the speed parameter
    private float currentSpeed;
    public float smoothTime = 0.1f;
    public float animationSpeed = 1f;

    // NPC variables
    private Vector2 way;
    private float range = 1;
    private float pauseDuration = 4;
    private float NPCmoveSpeed;
    private float timer = 0;
    private bool isMoving;
    public bool isChickenFree = false;
    public bool waypointOneReached = false;
    public bool waypointOneBackReached = false;
    public bool chickenStayed = false;

    private void Start()
    {
        isChickenFree = false;
        waypointOneReached = false;
        waypointOneBackReached = false;
        chickenStayed = false;

        moveSpeed = 8f;
        chick = GetComponent<ChickenInteractions>();
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

    private void Update()
    {
        if (chick.isSleeping || chick.isLayingEgg)
            return;

        // Set chicken free status based on current time
        int currentHour = TimerClock.Hours();
        isChickenFree = (currentHour >= 12 && currentHour < 21);

        if (CompareTag("Player"))
            PlayerUpdate();
        else if (CompareTag("NPC"))
            NPCUpdate();
        //Debug.Log(isChickenFree);
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

        Vector2 moveVector = new Vector2(input.x, input.y).normalized * moveSpeed;
        rb.velocity = moveVector;

        if (input.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (input.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        float targetSpeed = moveVector.magnitude;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, smoothTime / Time.fixedDeltaTime);
        animator.SetFloat("Speed", currentSpeed);
    }

    private void NPCUpdate()
    {
        if (isMoving)
        {
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
        int currentHour = TimerClock.Hours();
        int currentMinute = TimerClock.Minutes();

        if ((currentHour == 12 && (currentMinute > 0 && currentMinute < 59)) && (chickenStayed == false))
        {
            way = new Vector2(17, -4);

            waypointOneReached = true;
            chickenStayed = true;
        }
        else
        {
            if ((currentHour == 21 && (currentMinute > 0 && currentMinute < 59)) && (chickenStayed == true))
            {
                if (waypointOneBackReached && chickenStayed == true)
                {
                    way = new Vector2(17, -4);
                    isChickenFree = false;
                    waypointOneBackReached = false;
                    chickenStayed = false;
                }
                else
                {
                    way = new Vector2(13, -4);
                    waypointOneBackReached = true;
                }
            }
            else
            {
                if (waypointOneReached && chickenStayed == true)
                {
                    way = new Vector2(13, -4);
                    waypointOneReached = false;
                    isChickenFree = true;
                }
                else
                {
                    float minX, maxX, minY, maxY;

                    if (isChickenFree)
                    {
                        minX = -20;
                        maxX = 14;
                        minY = -15;
                        maxY = 25;
                    }
                    else
                    {
                        minX = 17;
                        maxX = 36;
                        minY = -8;
                        maxY = 3;
                    }
                    way = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                }
            }
        }
        if (way.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (way.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
