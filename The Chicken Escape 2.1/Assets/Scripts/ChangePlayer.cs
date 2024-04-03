using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayer : MonoBehaviour
{
    public GameObject[] chickens; // Массив всех курочек
    private GameObject currentChicken; // Текущая курица
    public Button firstChickenButton;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        // Устанавливаем первую курицу как текущую
        currentChicken = chickens[0];
        currentChicken.tag = "Player";

        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstChickenButton.gameObject);
        // Устанавливаем все остальные курочки как NPC
        for (int i = 1; i < chickens.Length; i++)
        {
            chickens[i].tag = "NPC";
        }
    }
    public void ChooseChicken(int chickenNumber)
    {
        audioManager.PlaySound(audioManager.buttonClick);
        // Сначала сбрасываем тег текущей курицы на NPC
        currentChicken.tag = "NPC";

        // Затем устанавливаем тег новой курицы на Player
        currentChicken = chickens[chickenNumber];
        currentChicken.tag = "Player";
        SmoothCameraFollow.target = currentChicken.GetComponent<Transform>();
    }
}
