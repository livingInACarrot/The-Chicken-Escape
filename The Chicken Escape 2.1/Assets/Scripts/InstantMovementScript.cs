using UnityEngine;
using System.Collections;
public class InstantMovementScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 input;
    private Animator animator;
    
    // Variable for smoothing the speed parameter
    private float currentSpeed;
    public float smoothTime = 0.1f;
    public float animationSpeed = 1f;
    //public int chickenNumber;

    // NPC variables
    private float maxDist = 6;    // Макс. расстояние, на которое ИИ курочки может отойти за раз
    private Vector2 way;
    private float range = 1;
    private float pauseDuration = 4;
    private float NPCmoveSpeed;
    private float timer = 0;
    private bool isMoving;

    private void Start()
    {
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
        if (CompareTag("Player"))
            PlayerUpdate();
        else if (CompareTag("NPC"))
            NPCUpdate();
    }
    private void PlayerUpdate()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        Vector2 moveVector = input.normalized * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(moveVector.x, moveVector.y, 0);

        // Mirror the sprite when moving left
        if (input.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (input.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (animator.speed != animationSpeed)
        {
            animator.speed = animationSpeed;
        }

        // Smoothly transition the speed parameter
        float targetSpeed = moveVector.magnitude;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, smoothTime / Time.deltaTime);
        animator.SetFloat("Speed", currentSpeed);

        //Debug.Log("Current Speed: " + currentSpeed + ", Target Speed: " + targetSpeed);
    }
    private void NPCUpdate()
    {
        if (animator.speed != animationSpeed)
        {
            animator.speed = animationSpeed;
        }

        if (isMoving)
        {
            if (Vector2.Distance(transform.position, way) > range)
            {
                transform.position = Vector2.MoveTowards(transform.position, way, NPCmoveSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetFloat("Speed", 0);
                isMoving = false;
            }
        }
        if (!isMoving)
        {
            timer += Time.deltaTime;
            if (timer >= pauseDuration)
            {
                animator.SetFloat("Speed", 1);
                isMoving = true;
                timer = 0;
                NewDestination();
            }
        }

    }
    private void NewDestination()
    {
        way = new Vector2(Random.Range(transform.position.x - maxDist, transform.position.x + maxDist),
            Random.Range(transform.position.y - maxDist, transform.position.y + maxDist));
        if (way.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (way.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("NPC"))
        {
            NewDestination();
        }
    }
}
