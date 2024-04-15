using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ChickenInteractions : MonoBehaviour
{
    private Animator chickenAnimator;

    public bool isEating = false;
    public bool isDrinking = false;
    public bool isSleeping = false;
    public bool isLayingEgg = false;

    private float layingEggTime = 50;
    private int progress = 0;
    private float time = 0;

    void Start()
    {
        chickenAnimator = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!CompareTag("Player"))
        {
            return;
        }

        ButtonsController.button.transform.position = other.bounds.center + new Vector3(0, 3);
        
        //Vector3 otherCenter = other.bounds.center;
        //button.transform.position = otherCenter + new Vector3(0, 1);

        //if (isEating || isDrinking || isSleeping || isLayingEgg)
        //    return;

        if (other.CompareTag("Eat"))
        {
            ButtonsController.button.gameObject.SetActive(true);
            ButtonsController.button.GetComponentInChildren<TMP_Text>().text = "eat";
            ButtonsController.button.onClick.RemoveAllListeners();
            ButtonsController.button.onClick.AddListener(delegate () {
                ButtonsController.button.GetComponentInChildren<TMP_Text>().text = "stop";
                StartCoroutine(PlayEatAnimation());
            });

        }
        else if (other.CompareTag("Drink"))
        {
            ButtonsController.button.gameObject.SetActive(true);
            ButtonsController.button.GetComponentInChildren<TMP_Text>().text = "drink";
            ButtonsController.button.onClick.RemoveAllListeners();
            ButtonsController.button.onClick.AddListener(delegate () {
                ButtonsController.button.GetComponentInChildren<TMP_Text>().text = "stop";
                StartCoroutine(PlayDrinkAnimation());
            });
        }
        else if (other.CompareTag("Sleep"))
        {
            ButtonsController.button2.transform.position = other.bounds.center + new Vector3(0, 4);
            ButtonsController.progressBar.transform.position = other.bounds.center + new Vector3(0, 3.5f);
            ButtonsController.button.gameObject.SetActive(true);
            ButtonsController.button2.gameObject.SetActive(true);
            ButtonsController.button.GetComponentInChildren<TMP_Text>().text = "sleep";
            ButtonsController.button2.GetComponentInChildren<TMP_Text>().text = "lay egg";

            //button.onClick.RemoveAllListeners();
            //button2.onClick.RemoveAllListeners();
            //sleep
            ButtonsController.button.onClick.AddListener(delegate () {
                transform.position = other.bounds.center + new Vector3(0, -0.12f, 0);
                ButtonsController.button2.gameObject.SetActive(false);
                ButtonsController.button.GetComponentInChildren<TMP_Text>().text = "stop";
                StartCoroutine(PlaySleepAnimation());
            });
            //lay egg
            ButtonsController.button2.onClick.AddListener(delegate () {
                transform.position = other.bounds.center + new Vector3(0, -0.12f, 0);
                ButtonsController.button.gameObject.SetActive(false);
                ButtonsController.progressBar.gameObject.SetActive(true);
                ButtonsController.button2.GetComponentInChildren<TMP_Text>().text = "stop";
                ButtonsController.button2.onClick.AddListener(() => Stop());
                StartCoroutine(LayEgg(other));
            });
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        ButtonsController.Hide();
        Stop();
    }
    private void Stop()
    {
        ButtonsController.progressBar.gameObject.SetActive(false);
        ButtonsController.button.onClick.RemoveAllListeners();
        ButtonsController.button2.onClick.RemoveAllListeners();
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
        ButtonsController.button.onClick.AddListener(() => Stop());
        yield return new WaitForSeconds(4);
    }

    IEnumerator PlayDrinkAnimation()
    {
        isDrinking = true;
        chickenAnimator.Play("chicken_drink");
        ButtonsController.button.onClick.AddListener(() => Stop());
        yield return new WaitForSeconds(5);
    }
    IEnumerator PlaySleepAnimation()
    {
        isSleeping = true;
        chickenAnimator.Play("chicken_sleep");
        ButtonsController.button.onClick.AddListener(() => Stop());
        yield return new WaitForSeconds(2);
    }
    IEnumerator LayEgg(Collider2D other)
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
                ButtonsController.progressBar.GetComponent<Image>().sprite = ButtonsController.progressBar.SetProgress(progress);
            }
            yield return null;
        }
        progress = 0;
        ButtonsController.progressBar.GetComponent<Image>().sprite = ButtonsController.progressBar.SetProgress(progress);
        ButtonsController.button2.gameObject.SetActive(false);
        Egg egg = other.GetComponentInChildren<Egg>();
        egg.gameObject.SetActive(true);
        Stop();
    }
}