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
    private CoopGate gate;

    // Variable for smoothing the speed parameter
    private float currentSpeed;
    public float smoothTime = 0.1f;
    public float animationSpeed = 1f;

    // NPC variables
    public Vector2 way;
    private float range = 1;
    private float maxDist = 7;
    private float pauseDuration = 7;
    private float NPCmoveSpeed;
    private float timer = 0;
    private bool isMoving;
    public bool isChickenFree = false;
    public bool chickenStayed = false;
    private bool isLeavingCoop = false;
    private bool isEnteringCoop = false;
    private int pointIndex = 0;

    private void Start()
    {
        moveSpeed = 8f;
        chick = GetComponent<ChickenInteractions>();
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
        gate = FindObjectOfType<CoopGate>();
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

        int currentHour = TimerClock.Hours();
        isChickenFree = (currentHour >= 12 && currentHour < 21);

        if (CompareTag("Player"))
            PlayerUpdate();
        else if (CompareTag("NPC"))
            NPCUpdate();
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
        if (TimerClock.Hours() == 12 && TimerClock.Minutes() == 0)
        {
            isLeavingCoop = true;
            isChickenFree = true;
        }
        else if (TimerClock.Hours() == 21 && TimerClock.Minutes() == 0)
        {
            isEnteringCoop = true;
            isChickenFree = false;
        }

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
            timer += Time.deltaTime;
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
        if (isLeavingCoop)
        {
            way = gate.LeavingCoopPoints[pointIndex].position;
            ++pointIndex;
            if (pointIndex == gate.LeavingCoopPoints.Length)
            {
                isLeavingCoop = false;
                pointIndex = 0;
            }
        }
        else if (isEnteringCoop)
        {
            way = gate.EnteringCoopPoints[pointIndex].position;
            ++pointIndex;
            if (pointIndex == gate.LeavingCoopPoints.Length)
            {
                isEnteringCoop = false;
                pointIndex = 0;
            }
        }
        else
        {
            float minX, maxX, minY, maxY;

            if (isChickenFree)
            {
                minX = transform.position.x - maxDist;
                maxX = transform.position.x + maxDist;
                minY = transform.position.y - maxDist;
                maxY = transform.position.y + maxDist;
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

        if (chickenStayed)
        {
            float minX, maxX, minY, maxY;
            minX = 17;
            maxX = 36;
            minY = -8;
            maxY = 3;
            way = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
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
