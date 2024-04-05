using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayer : MonoBehaviour
{
    public GameObject[] chickens;
    private GameObject currentChicken;
    public Button firstChickenButton;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        currentChicken = chickens[0];
        currentChicken.tag = "Player";
        currentChicken.GetComponent<Rigidbody2D>().mass = 1;

        for (int i = 1; i < chickens.Length; i++)
        {
            chickens[i].tag = "NPC";
            chickens[i].GetComponent<Rigidbody2D>().mass = 10;
        }

        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstChickenButton.gameObject);
    }

    public void ChooseChicken(int chickenNumber)
    {
        audioManager.PlaySound(audioManager.buttonClick);

        currentChicken.tag = "NPC";
        currentChicken.GetComponent<Rigidbody2D>().mass = 10;

        currentChicken = chickens[chickenNumber];
        currentChicken.tag = "Player";
        currentChicken.GetComponent<Rigidbody2D>().mass = 1;

        // Set the camera to follow the new player chicken
        SmoothCameraFollow.target = currentChicken.GetComponent<Transform>();

        foreach (var chicken in chickens)
        {
            if (chicken != currentChicken)
            {
                chicken.tag = "NPC";
                chicken.GetComponent<Rigidbody2D>().mass = 10;
            }
        }
    }
}
