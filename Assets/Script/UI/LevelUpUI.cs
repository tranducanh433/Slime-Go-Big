using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] UpgradePool upgradePool;

    [Header("Other")]
    [SerializeField] GameObject content;
    [SerializeField] UpgradeData startUpgrade;
    [SerializeField] UpgradeSlot[] slots;

    bool m_isDisplaying = false;

    public bool isDisplaying { get {  return m_isDisplaying; } }


    private void Start()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.OnLevelUp.AddListener(DisplayUpgradeSelector);

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].OnSelected.AddListener(SelectUpgrade);
        }

        //Add to selected Upgrade
        if(startUpgrade != null)
            upgradePool.RemoveFromPool(startUpgrade);
    }


    public void DisplayUpgradeSelector()
    {
        if (upgradePool.IsEmpty())
            return;

        m_isDisplaying = true;
        TimeScaleManager.PauseGame();

        content.SetActive(true);
        UpgradeData[] upgrades = upgradePool.GetRandomUpgrade(3);
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < upgrades.Length)
            {
                slots[i].gameObject.SetActive(true);
                slots[i].Display(upgrades[i]);
            }
            else
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }

    public void SelectUpgrade(UpgradeData upgradeData)
    {
        TimeScaleManager.ResumeGame();
        m_isDisplaying = false;

        upgradePool.RemoveFromPool(upgradeData);
        Instantiate(upgradeData.spawnObj);
        content.SetActive(false);
    }

    public UpgradeData[] GetSelectedUpgrade()
    {
        return upgradePool.SelectedUpgrade;
    }
}
