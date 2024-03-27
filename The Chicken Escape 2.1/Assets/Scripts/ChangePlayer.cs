using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    public GameObject[] chickens; // ������ ���� �������
    private GameObject currentChicken; // ������� ������

    void Start()
    {
        // ������������� ������ ������ ��� �������
        currentChicken = chickens[0];
        currentChicken.tag = "Player";

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
        SmoothCameraFollow.target = currentChicken.GetComponent<Transform>();
    }
}
