using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    public GameObject[] chickens; // Массив всех курочек
    private GameObject currentChicken; // Текущая курица

    void Start()
    {
        // Устанавливаем первую курицу как текущую
        currentChicken = chickens[0];
        currentChicken.tag = "Player";

        // Устанавливаем все остальные курочки как NPC
        for (int i = 1; i < chickens.Length; i++)
        {
            chickens[i].tag = "NPC";
        }
    }

    public void ChooseChicken(int chickenNumber)
    {
        // Сначала сбрасываем тег текущей курицы на NPC
        currentChicken.tag = "NPC";

        // Затем устанавливаем тег новой курицы на Player
        currentChicken = chickens[chickenNumber];
        currentChicken.tag = "Player";
        SmoothCameraFollow.target = currentChicken.GetComponent<Transform>();
    }
}
