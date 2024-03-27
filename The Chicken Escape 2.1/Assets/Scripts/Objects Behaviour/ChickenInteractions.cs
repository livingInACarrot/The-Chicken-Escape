using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ChickenInteractions : MonoBehaviour
{
    public Animator chickenAnimator;
    public Button eatButton;
    public Button drinkButton;
    private bool isFeeding = false;

    private void Start()
    {
        chickenAnimator = GetComponent<Animator>();
        eatButton.gameObject.SetActive(false);
        drinkButton.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isFeeding)
        {
            if (other.CompareTag("Eat"))
            {
                eatButton.gameObject.SetActive(true);
                eatButton.onClick.AddListener(delegate() {
                    eatButton.GetComponentInChildren<TMP_Text>().text = "stop";
                    StartCoroutine(PlayEatAnimation()); 
                });

            }
            else if (other.CompareTag("Drink"))
            {
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
        NeedsController.isEating = false;
        NeedsController.isDrinking = false;
        eatButton.GetComponentInChildren<TMP_Text>().text = "eat";
        drinkButton.GetComponentInChildren<TMP_Text>().text = "drink";
        chickenAnimator.Play("chicken_run");
    }

    IEnumerator PlayEatAnimation()
    {
        isFeeding = true;
        NeedsController.isEating = true;
        chickenAnimator.Play("chicken_eat");
        eatButton.onClick.AddListener(() => Stop());
        yield return new WaitForSeconds(4);
    }

    IEnumerator PlayDrinkAnimation()
    {
        isFeeding = true;
        NeedsController.isDrinking = true;
        chickenAnimator.Play("chicken_drink");
        drinkButton.onClick.AddListener(() => Stop());
        yield return new WaitForSeconds(5);
    }
}