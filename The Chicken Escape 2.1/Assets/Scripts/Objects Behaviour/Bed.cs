using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bed : MonoBehaviour
{
    public Button button;
    void Start()
    {
        button.gameObject.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            button.gameObject.SetActive(true);
            button.GetComponentInChildren<TMP_Text>().text = "sleep";
            button.onClick.AddListener(delegate () {
                //other.transform.position = transform.position;
                //other.offset = transform.position;
                button.GetComponentInChildren<TMP_Text>().text = "stop";
                StartCoroutine(PlaySleepAnimation(other.GetComponent<Animator>()));
            });
        }
    }
    IEnumerator PlaySleepAnimation(Animator animator)
    {
        NeedsController.isSleeping = true;
        animator.Play("chicken_sleep");
        button.onClick.AddListener(() => Stop(animator));
        yield return new WaitForSeconds(300);
    }
    private void Stop(Animator animator)
    {
        NeedsController.isEating = false;
        NeedsController.isDrinking = false;
        NeedsController.isSleeping = false;
        animator.Play("chicken_idle");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        button.gameObject.SetActive(false);
        Stop(other.GetComponent<Animator>());
    }
}
