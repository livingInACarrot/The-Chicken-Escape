using UnityEngine;
using System.Collections;

public class InstantMovementScript : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 input;
    private Animator animator;
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
    public bool isMoving;
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
        // If the chicken is sleeping or laying an egg, we should neither update the player controls nor NPC behavior.
        if (chick.isSleeping || chick.isLayingEgg)
        {
            // If the chicken is sleeping, ensure the sleeping animation plays endlessly.
            if (chick.isSleeping && animator.GetCurrentAnimatorStateInfo(0).IsName("chicken_sleep") == false)
            {
                animator.Play("chicken_sleep");
            }
            return; // Skip the rest of the update if sleeping or laying an egg.
        }
        /*
        // Check if it's past 11:00 AM and if the chicken has not laid an egg yet
        if (TimerClock.currentTime > 660 && !chick.eggLaidToday)
        {
            isChickenFree = false;  // The chicken is not free to leave if it hasn't laid an egg
        }
        
        else
        {
            int currentHour = TimerClock.Hours();
            isChickenFree = (currentHour >= 12 && currentHour < 21);  // Otherwise, follow the usual free-range hours
        }
        */
        if (CompareTag("Player"))
            PlayerUpdate();
        else if (CompareTag("NPC"))
            NPCUpdate();
    }

    /*
    private void FixedUpdate()
    {
        // FixedUpdate is typically used for physics updates. If the chicken is sleeping, it should not move.
        if (chick.isSleeping || chick.isLayingEgg)
        {
            rb.velocity = Vector2.zero; // This ensures the chicken stops moving.
            return; // Skip the rest of the FixedUpdate if sleeping or laying an egg.
        }

        // The rest of FixedUpdate is unchanged.
        if (CompareTag("Player"))
            PlayerUpdate();
        else if (CompareTag("NPC"))
            NPCUpdate();
    }
    */

    private void PlayerUpdate()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        Vector2 moveVector = new Vector2(input.x, input.y).normalized * moveSpeed;

        // Condition to prevent movement past x=16 to the left if no egg has been laid after 11:00 AM
        if (!chick.eggLaidToday && rb.position.x <= 16.7f && input.x < 0)
        {
            moveVector.x = 0; // Disallow leftward movement
        }

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
            if (chick.eggLaidToday)
            {
                isLeavingCoop = true;
                isChickenFree = true;
            }
        }
        else if (TimerClock.Hours() == 21 && TimerClock.Minutes() == 0)
        {
            if (chick.eggLaidToday)
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
