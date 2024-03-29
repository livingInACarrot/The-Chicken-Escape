using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ChickenInteractions : MonoBehaviour
{
    private Animator chickenAnimator;
    public Button button;

    private void Start()
    {
        chickenAnimator = GetComponent<Animator>();
        button.gameObject.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        // ������ ������ ��� ��������
        Vector3 otherCenter = other.bounds.center;
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        buttonRectTransform.position = otherCenter;
        buttonRectTransform.anchoredPosition += new Vector2(0, 300);

        if (!NeedsController.isEating && !NeedsController.isDrinking && !NeedsController.isSleeping)
        {
            if (other.CompareTag("Eat"))
            {
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<TMP_Text>().text = "eat";
                button.onClick.AddListener(delegate() {
                    button.GetComponentInChildren<TMP_Text>().text = "stop";
                    StartCoroutine(PlayEatAnimation()); 
                });

            }
            else if (other.CompareTag("Drink"))
            {
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<TMP_Text>().text = "drink";
                button.onClick.AddListener(delegate () {
                    button.GetComponentInChildren<TMP_Text>().text = "stop";
                    StartCoroutine(PlayDrinkAnimation());
                });
            }
            else if (other.CompareTag("Sleep"))
            {
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<TMP_Text>().text = "sleep";
                Vector2 colliderPosition = other.transform.position;
                button.onClick.AddListener(delegate () {
                    transform.position = colliderPosition;
                    button.GetComponentInChildren<TMP_Text>().text = "stop";
                    //chickenAnimator.SetBool("isSleeping", true);
                    StartCoroutine(PlaySleepAnimation());
                });
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        button.gameObject.SetActive(false);
        Stop();
    }
    private void Stop()
    {
        NeedsController.isEating = false;
        NeedsController.isDrinking = false;
        NeedsController.isSleeping = false;
        chickenAnimator.Play("chicken_idle");
    }

    IEnumerator PlayEatAnimation()
    {
        NeedsController.isEating = true;
        chickenAnimator.Play("chicken_eat");
        button.onClick.AddListener(() => Stop());
        yield return new WaitForSeconds(4);
    }

    IEnumerator PlayDrinkAnimation()
    {
        NeedsController.isDrinking = true;
        chickenAnimator.Play("chicken_drink");
        button.onClick.AddListener(() => Stop());
        yield return new WaitForSeconds(5);
    }
    IEnumerator PlaySleepAnimation()
    {
        NeedsController.isSleeping = true;
        chickenAnimator.Play("chicken_sleep");
        button.onClick.AddListener(() => Stop());
        yield return new WaitForSeconds(30);
    }
}