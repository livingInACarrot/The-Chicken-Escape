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

    private void Start()
    {
        isChickenFree = false;
        waypointOneReached = false;
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
        isChickenFree = (currentHour >= 11 && currentHour < 22);

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
        Debug.Log(currentHour);
        Debug.Log(currentMinute);
        // If it's exactly 12:00, set destination to X15 Y-3.5
        if (currentHour == 11 && (currentMinute > 0 && currentMinute < 11))
        {
            Debug.Log("HELLLO");
            way = new Vector2(16, -3.5f);
            Debug.Log(gameObject.name + " WAYPOINT: " + way);
            isChickenFree = true;
            waypointOneReached = true;
        }
        else
        {
            if(waypointOneReached)
            {
                Debug.Log(gameObject.name + " WAYPOINT 2: " + way);
                way = new Vector2(12, -3.5f);
                waypointOneReached = false;
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
            //Debug.Log(way + gameObject.name);
        }

        // Adjust the facing direction based on the new destination
        if (way.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (way.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //Debug.Log(way);
    }
}
