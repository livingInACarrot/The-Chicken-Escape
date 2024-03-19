using UnityEngine;

public class InstantMovementScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 input;

    // Declare the Animator
    public Animator animator;

    // Variable for smoothing the speed parameter
    private float currentSpeed;
    public float smoothTime = 0.1f;
    public float animationSpeed = 1f;

    private void Start()
    {
        // Ensure that the Animator component is attached
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        animator.speed = animationSpeed;
    }

    private void Update()
    {
        // Get input from WASD keys
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        // Move the chicken instantly to the target position
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

        Debug.Log("Current Speed: " + currentSpeed + ", Target Speed: " + targetSpeed);
    }
}
