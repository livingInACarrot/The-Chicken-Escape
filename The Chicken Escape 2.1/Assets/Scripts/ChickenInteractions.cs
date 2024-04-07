using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ChickenInteractions : MonoBehaviour
{
    private Animator chickenAnimator;
    public Button button;
    public Button button2;

    public bool isEating = false;
    public bool isDrinking = false;
    public bool isSleeping = false;
    public bool isLayingEgg = false;

    void Start()
    {
        chickenAnimator = GetComponent<Animator>();
        button.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!CompareTag("Player"))
        {
            return;
        }

        Vector3 otherCenter = other.bounds.center;
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        buttonRectTransform.position = otherCenter;
        buttonRectTransform.anchoredPosition += new Vector2(0, 300);

        if (!isEating && !isDrinking && !isSleeping)
        {
            if (other.CompareTag("Eat"))
            {
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<TMP_Text>().text = "eat";
                button.onClick.RemoveAllListeners(); // Clear existing listeners
                button.onClick.AddListener(delegate () {
                    button.GetComponentInChildren<TMP_Text>().text = "stop";
                    StartCoroutine(PlayEatAnimation());
                });

            }
            else if (other.CompareTag("Drink"))
            {
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<TMP_Text>().text = "drink";
                button.onClick.RemoveAllListeners(); // Clear existing listeners
                button.onClick.AddListener(delegate () {
                    button.GetComponentInChildren<TMP_Text>().text = "stop";
                    StartCoroutine(PlayDrinkAnimation());
                });
            }
            else if (other.CompareTag("Sleep"))
            {
                RectTransform button2RectTransform = button2.GetComponent<RectTransform>();
                button2RectTransform.position = otherCenter;
                button2RectTransform.anchoredPosition += new Vector2(0, 410);
                Transform chickenRectTransform = GetComponent<Transform>();

                button.gameObject.SetActive(true);
                button2.gameObject.SetActive(true);
                button.GetComponentInChildren<TMP_Text>().text = "sleep";
                button2.GetComponentInChildren<TMP_Text>().text = "lay egg";

                button.onClick.RemoveAllListeners(); // Clear existing listeners for sleep
                button2.onClick.RemoveAllListeners(); // Clear existing listeners for lay egg

                //sleep
                button.onClick.AddListener(delegate () {
                    Vector3 targetCenter = otherCenter + new Vector3(0, -0.12f, 0);
                    chickenRectTransform.position = targetCenter;
                    button.GetComponentInChildren<TMP_Text>().text = "stop";
                    StartCoroutine(PlaySleepAnimation());
                });
                //lay egg
                button2.onClick.AddListener(delegate () {
                    Vector3 targetCenter = otherCenter + new Vector3(0, -0.12f, 0);
                    chickenRectTransform.position = targetCenter;
                    button2.gameObject.SetActive(false);
                    StartCoroutine(LayEgg());
                });
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        button.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        Stop();
    }
    private void Stop()
    {
        isEating = false;
        isDrinking = false;
        isSleeping = false;
        chickenAnimator.Play("chicken_idle");
    }

    IEnumerator PlayEatAnimation()
    {
        isEating = true;
        chickenAnimator.Play("chicken_eat");
        button.onClick.AddListener(() => Stop());
        yield return new WaitForSeconds(4);
    }

    IEnumerator PlayDrinkAnimation()
    {
        isDrinking = true;
        chickenAnimator.Play("chicken_drink");
        button.onClick.AddListener(() => Stop());
        yield return new WaitForSeconds(5);
    }
    IEnumerator PlaySleepAnimation()
    {
        isSleeping = true;
        chickenAnimator.Play("chicken_sleep");
        button.onClick.AddListener(() => Stop());
        yield return new WaitForSeconds(30);
    }
    IEnumerator LayEgg()
    {
        isLayingEgg = true;
        chickenAnimator.Play("chicken_lay_egg");
        button.onClick.AddListener(() => Stop());
        yield return new WaitForSeconds(30);
    }
}