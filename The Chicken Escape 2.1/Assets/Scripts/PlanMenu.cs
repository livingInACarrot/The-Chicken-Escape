using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlanMenu : MonoBehaviour
{
    public GameObject planMenuCanvas;
    public GameObject hidePanel;
    public GameObject pauseButton;
    public List<GameObject> redOvals;
    public static int planNumber = 1;
    private void Start()
    {
        foreach (var oval in redOvals)
        {
            oval.SetActive(false);
        }
        redOvals[planNumber-1].SetActive(true);        
    }
    public void TogglePlanMenu()
    {
        bool isActive = planMenuCanvas.activeSelf;
        planMenuCanvas.SetActive(!isActive);
        pauseButton.SetActive(isActive);
        hidePanel.SetActive(isActive);

    }
    public void ChoosePlan(int number)
    {
        redOvals[planNumber-1].SetActive(false);
        planNumber = number;
        redOvals[planNumber-1].SetActive(true);
    }
}
