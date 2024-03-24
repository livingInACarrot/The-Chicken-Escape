using UnityEngine;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    public Button interactionButton;         // Кнопка над объектом
    public float interactionDistance = 2.0f; // Расстояние, на котором курочка может взаимодействовать с объектом
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
            // Здесь реализуйте логику взаимодействия с объектом
            Debug.Log("Взаимодействие с объектом");
        }
    }
}

