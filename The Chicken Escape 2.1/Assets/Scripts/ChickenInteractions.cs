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
    public GameObject gate;

    private bool aborted = false;

    private float layingEggTime = 50;
    private bool eggLaidToday = false;
    private int progress = 0;
    private float currentProcessTime = 0;

    void Start()
    {
        chickenAnimator = GetComponent<Animator>();
        CheckGateStatus();
    }

    private void Update()
    {
        CheckGateStatus();
    }

    private void CheckGateStatus()
    {
        // If the current time is past 11:00 (660 minutes) and no egg has been laid, close the gate
        if (TimerClock.currentTime > 660 && !eggLaidToday)
        {
            gate.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            gate.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (CompareTag("NPC"))
        {
            return;
        }

        ButtonsController.button.transform.position = other.bounds.center + new Vector3(0, 3);
        ButtonsController.button2.transform.position = other.bounds.center + new Vector3(0, 4);
        ButtonsController.progressBar.transform.position = other.bounds.center + new Vector3(0, 3.1f);

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
            if (!isSleeping && !isLayingEgg)
            {
                aborted = false;
                ButtonsController.button.gameObject.SetActive(true);
                ButtonsController.button2.gameObject.SetActive(true);
                ButtonsController.button.GetComponentInChildren<TMP_Text>().text = "sleep";
                ButtonsController.button2.GetComponentInChildren<TMP_Text>().text = "lay egg";
                ButtonsController.button.onClick.AddListener(() => StartCoroutine(ISleep(other.bounds.center)));
                ButtonsController.button2.onClick.AddListener(() => StartCoroutine(ILayEgg(other)));
            }
            else if (isSleeping)
            {
                //ButtonsController.button.onClick.AddListener(() => StartCoroutine(ISleep(other.bounds.center)));
                return;
            }
            else if (isLayingEgg)
            {
                //ButtonsController.button2.onClick.AddListener(() => StartCoroutine(ILayEgg(other)));
                return;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        ButtonsController.Hide();
        Stop();
    }
    private void Stop()
    {
        //aborted = false;
        ButtonsController.Refresh();
        chickenAnimator.Play("chicken_run");
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
    IEnumerator ISleep(Vector3 bedPos)
    {
        isSleeping = true;
        transform.position = bedPos + new Vector3(0, -0.12f, 0);
        ButtonsController.button2.gameObject.SetActive(false);
        ButtonsController.button.GetComponentInChildren<TMP_Text>().text = "stop";
        ButtonsController.button.onClick.RemoveAllListeners();
        ButtonsController.button.onClick.AddListener(() => isSleeping = false);
        yield return new WaitUntil(() => Sleep());
        Stop();
    }
    bool Sleep()
    {
        chickenAnimator.Play("chicken_sleep");
        return !isSleeping;
    }
    IEnumerator ILayEgg(Collider2D bed)
    {
        transform.position = bed.bounds.center + new Vector3(0, -0.12f, 0);
        ButtonsController.button.gameObject.SetActive(false);
        ButtonsController.progressBar.gameObject.SetActive(true);
        ButtonsController.button2.GetComponentInChildren<TMP_Text>().text = "stop";
        ButtonsController.button2.onClick.AddListener(() => { 
            isLayingEgg = false;
            aborted = true;
            Debug.Log(aborted);
        });
        ButtonsController.progressBar.GetComponent<Image>().sprite = ButtonsController.progressBar.SetProgress(progress);
        isLayingEgg = true;
        yield return new WaitUntil(() => LayEgg());
        progress = 0;
        ButtonsController.Hide();
        Debug.Log(aborted);
        if (!aborted)
        {
            eggLaidToday = true;
            bed.gameObject.transform.Find("egg").gameObject.SetActive(true);
        }
        Stop();
        CheckGateStatus();
    }
    bool LayEgg()
    {
        if (!isLayingEgg)
            return true;
        chickenAnimator.Play("chicken_lay_egg");
        currentProcessTime += Time.deltaTime;
        if (currentProcessTime >= layingEggTime)
        {
            if (progress >= 10)
            {
                isLayingEgg = false;
                return true;
            }
            currentProcessTime = 0;
            ++progress;
            ButtonsController.progressBar.GetComponent<Image>().sprite = ButtonsController.progressBar.SetProgress(progress);
        }
        return false;
    }
}