using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Feeding : MonoBehaviour
{
    public Animator chickenAnimator;
    public Button eatButton; // Assign this in the inspector
    public Button drinkButton; // Assign this in the inspector
    private bool isFeeding = false;
    public bool isWater = false;

    private void Start()
    {
        chickenAnimator = GetComponent<Animator>();
        // Hide the buttons initially
        eatButton.gameObject.SetActive(false);
        drinkButton.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isFeeding)
        {
            if (!isWater)
            {
                // Show the eating button.
                eatButton.gameObject.SetActive(true);
                eatButton.onClick.AddListener(delegate () {
                    eatButton.GetComponentInChildren<TMP_Text>().text = "stop";
                    StartCoroutine(PlayEatAnimation());
                });

            }
            // Check for the water object by tag.
            else if (isWater)
            {
                // Show the drinking button.
                drinkButton.gameObject.SetActive(true);
                drinkButton.onClick.AddListener(delegate () {
                    drinkButton.GetComponentInChildren<TMP_Text>().text = "stop";
                    StartCoroutine(PlayDrinkAnimation());
                });
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        eatButton.gameObject.SetActive(false);
        drinkButton.gameObject.SetActive(false);
        Stop();
    }
    private void Stop()
    {
        isFeeding = false;
        eatButton.GetComponentInChildren<TMP_Text>().text = "eat";
        drinkButton.GetComponentInChildren<TMP_Text>().text = "drink";
        chickenAnimator.Play("chicken_run");
    }

    IEnumerator PlayEatAnimation()
    {
        isFeeding = true;
        chickenAnimator.Play("chicken_eat");
        eatButton.onClick.AddListener(() => Stop());
        yield return new WaitForSeconds(4);
    }

    IEnumerator PlayDrinkAnimation()
    {
        isFeeding = true;
        chickenAnimator.Play("chicken_drink");
        drinkButton.onClick.AddListener(() => Stop());
        yield return new WaitForSeconds(5);
    }
}

