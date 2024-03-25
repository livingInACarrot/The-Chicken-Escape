using UnityEngine;
using UnityEngine.UI; // Required for UI components

public class ChickenInteractions : MonoBehaviour
{
    public Animator chickenAnimator;
    public Button eatButton; // Assign this in the inspector
    public Button drinkButton; // Assign this in the inspector
    public Canvas canvas; // Assign the main UI canvas in the inspector

    private void Start()
    {
        // Hide the buttons initially
        eatButton.gameObject.SetActive(false);
        drinkButton.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check for the food object by tag.
        if (other.CompareTag("Eat"))
        {
            // Show the eating button.
            PositionButtonAboveChicken(eatButton);
            eatButton.gameObject.SetActive(true);
            eatButton.onClick.AddListener(() => PlayEatAnimation());
        }
        // Check for the water object by tag.
        else if (other.CompareTag("Drink"))
        {
            // Show the drinking button.
            PositionButtonAboveChicken(drinkButton);
            drinkButton.gameObject.SetActive(true);
            drinkButton.onClick.AddListener(() => PlayDrinkAnimation());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Hide the buttons when the chicken leaves the trigger.
        if (other.CompareTag("Eat"))
        {
            eatButton.gameObject.SetActive(false);
            eatButton.onClick.RemoveListener(() => PlayEatAnimation());
        }
        else if (other.CompareTag("Drink"))
        {
            drinkButton.gameObject.SetActive(false);
            drinkButton.onClick.RemoveListener(() => PlayDrinkAnimation());
        }
    }

    private void PositionButtonAboveChicken(Button button)
    {
        // Convert the chicken's position to screen space and position the button.
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        button.transform.position = screenPosition + new Vector2(0, 50); // Offset by 50 pixels on the y-axis
    }

    private void PlayEatAnimation()
    {
        chickenAnimator.SetTrigger("Eat");
    }

    private void PlayDrinkAnimation()
    {
        chickenAnimator.SetTrigger("Drink");
    }
}