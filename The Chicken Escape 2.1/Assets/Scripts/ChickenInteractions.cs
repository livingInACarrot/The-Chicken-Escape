using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ChickenInteractions : MonoBehaviour
{
    private Animator chickenAnimator;
    public Button button;
    public Button button2;
    public Image progressBar;

    public bool isEating = false;
    public bool isDrinking = false;
    public bool isSleeping = false;
    public bool isLayingEgg = false;

    private float layingEggTime = 50; // seconds for laying an egg
    private int progress = 0;
    private float time = 0;

    void Start()
    {
        chickenAnimator = GetComponent<Animator>();
        button.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        progressBar.gameObject.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!CompareTag("Player"))
            return;
        if (isEating || isDrinking || isSleeping || isLayingEgg)
            return;
        Vector3 otherCenter = other.bounds.center;
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();
        buttonRectTransform.position = otherCenter;
        buttonRectTransform.anchoredPosition += new Vector2(0, 300);

        if (other.CompareTag("Eat"))
        {
            button.gameObject.SetActive(true);
            button.GetComponentInChildren<TMP_Text>().text = "eat";
            //button.onClick.RemoveAllListeners(); 
            button.onClick.AddListener(delegate () {
                button.GetComponentInChildren<TMP_Text>().text = "stop";
                StartCoroutine(PlayEatAnimation());
            });

        }
        else if (other.CompareTag("Drink"))
        {
            button.gameObject.SetActive(true);
            button.GetComponentInChildren<TMP_Text>().text = "drink";
            //button.onClick.RemoveAllListeners();
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

            RectTransform barRectTransform = progressBar.GetComponent<RectTransform>();
            barRectTransform.position = otherCenter;
            barRectTransform.anchoredPosition += new Vector2(0, 320);

            button.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            button.GetComponentInChildren<TMP_Text>().text = "sleep";
            button2.GetComponentInChildren<TMP_Text>().text = "lay egg";

            //button.onClick.RemoveAllListeners();
            //button2.onClick.RemoveAllListeners();

            //sleep
            button.onClick.AddListener(delegate () {
                Vector3 targetCenter = otherCenter + new Vector3(0, -0.12f, 0);
                chickenRectTransform.position = targetCenter;
                button2.gameObject.SetActive(false);
                button.GetComponentInChildren<TMP_Text>().text = "stop";
                StartCoroutine(PlaySleepAnimation());
            });
            //lay egg
            button2.onClick.AddListener(delegate () {
                Vector3 targetCenter = otherCenter + new Vector3(0, -0.12f, 0);
                chickenRectTransform.position = targetCenter;
                button.gameObject.SetActive(false);
                progressBar.gameObject.SetActive(true);
                button2.GetComponentInChildren<TMP_Text>().text = "stop";
                button2.onClick.AddListener(() => Stop());
                StartCoroutine(LayEgg());
                Egg egg = other.GetComponentInChildren<Egg>();
                egg.gameObject.SetActive(true);
            });
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
        progressBar.gameObject.SetActive(false);
        button.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        isEating = false;
        isDrinking = false;
        isSleeping = false;
        isLayingEgg = false;
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
        while (progress < 10)
        {
            time += Time.deltaTime;
            if (time >= layingEggTime)
            {
                time = 0;
                progress++;
                progressBar.sprite = ProgressController.SetProgress(progress);
            }
            yield return null;
        }
        progress = 0;
        progressBar.sprite = ProgressController.SetProgress(progress);
        button2.gameObject.SetActive(false);
        Stop();
    }
}