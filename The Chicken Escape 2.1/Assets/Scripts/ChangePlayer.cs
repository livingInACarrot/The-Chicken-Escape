using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayer : MonoBehaviour
{
    public GameObject[] chickens; // ������ ���� �������
    private GameObject currentChicken; // ������� ������
    public Button firstChickenButton;

    void Start()
    {
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
        // ������� ���������� ��� ������� ������ �� NPC
        currentChicken.tag = "NPC";

        // ����� ������������� ��� ����� ������ �� Player
        currentChicken = chickens[chickenNumber];
        currentChicken.tag = "Player";
        ChickenInteractions.SetAllXP(currentChicken);
        SmoothCameraFollow.target = currentChicken.GetComponent<Transform>();
    }
}
