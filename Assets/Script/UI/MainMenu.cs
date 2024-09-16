using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] AudioMixer audioMixer;

    private void Start()
    {
        float musicVolume = PlayerSetting.Instance.musicVolume;

        musicSlider.value = musicVolume;

        audioMixer.SetFloat("musicVolume", musicVolume);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnChangeMusicVolume()
    {

        PlayerSetting.Instance.musicVolume = musicSlider.value;
        audioMixer.SetFloat("musicVolume", musicSlider.value);
    }

    public void SkipTutorial(bool skipTutorial)
    {
        PlayerSetting.Instance.skipTutorial = skipTutorial;
    }
}
