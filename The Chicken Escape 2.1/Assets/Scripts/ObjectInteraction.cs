using UnityEngine;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    public Button interactionButton;         // ������ ��� ��������
    public float interactionDistance = 2.0f; // ����������, �� ������� ������� ����� ����������������� � ��������
    private GameObject obj;
    private void OnMouseOver()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) <= interactionDistance)
        {
            interactionButton.gameObject.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        interactionButton.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) <= interactionDistance)
        {
            // ����� ���������� ������ �������������� � ��������
            Debug.Log("�������������� � ��������");
        }
    }
}

