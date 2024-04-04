using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public string mainSceneName = "Main Menu";
    AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void Click()
    {
        audioManager.PlaySound(audioManager.buttonClick);
        SceneManager.LoadScene(mainSceneName);
    }
}
