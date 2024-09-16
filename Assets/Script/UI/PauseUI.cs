using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] LevelUpUI levelUpUI;
    [SerializeField] UpgradeSlot upgradeDetail;
    [SerializeField] GameObject content;
    [SerializeField] Slider musicSlider;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] PlayerUpgradeSlot[] playerUpgradeSlots;

    private void Start()
    {
        audioMixer.SetFloat("musicVolume", PlayerSetting.Instance.musicVolume);
        musicSlider.value = PlayerSetting.Instance.musicVolume;
    }

    public void Pause()
    {
        if (!TimeScaleManager.isPaused)
        {
            TimeScaleManager.PauseGame();
            content.SetActive(true);
        }
        else
        {
            if (!levelUpUI.isDisplaying)
            {
                TimeScaleManager.ResumeGame();
            }
            content.SetActive(false);
            return;
        }

        UpgradeData[] upgradeDatas = levelUpUI.GetSelectedUpgrade();
        for (int i = 0; i < playerUpgradeSlots.Length; i++)
        {
            if(i < upgradeDatas.Length)
            {
                playerUpgradeSlots[i].Display(upgradeDatas[i], upgradeDetail);
            }
            else
            {
                playerUpgradeSlots[i].Hidden();
            }
        }
    }

    public void OnChangeMusicVolume()
    {

        PlayerSetting.Instance.musicVolume = musicSlider.value;
        audioMixer.SetFloat("musicVolume", musicSlider.value);
    }
}
