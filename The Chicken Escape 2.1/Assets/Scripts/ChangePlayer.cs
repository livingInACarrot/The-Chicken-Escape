using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayer : MonoBehaviour
{
    public GameObject[] chickens; // ������ ���� �������
    private GameObject currentChicken; // ������� ������
    public Button firstChickenButton;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        // ������������� ������ ������ ��� �������
        currentChicken = chickens[0];
        currentChicken.tag = "Player";

        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstChickenButton.gameObject);
        // ������������� ��� ��������� ������� ��� NPC
        for (int i = 1; i < chickens.Length; i++)
        {
            chickens[i].tag = "NPC";
        }
    }
    public void ChooseChicken(int chickenNumber)
    {
        audioManager.PlaySound(audioManager.buttonClick);
        // ������� ���������� ��� ������� ������ �� NPC
        currentChicken.tag = "NPC";

        // ����� ������������� ��� ����� ������ �� Player
        currentChicken = chickens[chickenNumber];
        currentChicken.tag = "Player";
        SmoothCameraFollow.target = currentChicken.GetComponent<Transform>();
    }
}
