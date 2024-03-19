using UnityEngine;
using UnityEngine.Audio; // Include this for the AudioMixer
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public string mainSceneName = "Game";
    public GameObject menuPanel;
    public GameObject optionsPanel;
    public Slider musicVolumeSlider; // Assign this in the Unity Editor
    public Slider effectsVolumeSlider; // Assign this in the Unity Editor
    public Slider masterVolumeSlider; // Assign this in the Unity Editor
    public AudioMixer audioMixer; // Assign your Audio Mixer here in the Unity Editor

    private void Start()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);

        // Initialize the volume sliders' values to the current volumes
        //float musicVolume, effectsVolume, masterVolume;
        //audioMixer.GetFloat("MusicVolume", out musicVolume);
        //audioMixer.GetFloat("EffectsVolume", out effectsVolume);
        //audioMixer.GetFloat("MasterVolume", out masterVolume);
        //if (musicVolumeSlider != null)
            //musicVolumeSlider.value = musicVolume;
        //if (effectsVolumeSlider != null)
            //effectsVolumeSlider.value = effectsVolume;
        //if (masterVolumeSlider != null)
            //masterVolumeSlider.value = masterVolume;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(mainSceneName);
    }

    public void ToggleOptions()
    {
        if (menuPanel != null && optionsPanel != null)
        {
            bool isOptionsActive = optionsPanel.activeSelf;
            menuPanel.SetActive(isOptionsActive);
            optionsPanel.SetActive(!isOptionsActive);
        }
    }

    public void SetMusicVolume(float volume)
    {
        //audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        //audioMixer.SetFloat("EffectsVolume", volume);
    }

    public void SetMasterVolume(float volume)
    {
        //audioMixer.SetFloat("MasterVolume", volume); // Ensure "MasterVolume" matches the exposed parameter
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MenuButtonClicked()
    {
        if (menuPanel != null && optionsPanel != null)
        {
            optionsPanel.SetActive(false);
            menuPanel.SetActive(true);
        }
    }
}
