using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerUpgradeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image upgradeIcon;
    [SerializeField] TextMeshProUGUI levelText;

    UpgradeSlot upgradeDetail;
    UpgradeData upgradeData;

    public void Display(UpgradeData upgradeData, UpgradeSlot upgradeDetail)
    {
        gameObject.SetActive(true);
        this.upgradeData = upgradeData;
        this.upgradeDetail = upgradeDetail;

        upgradeIcon.sprite = upgradeData.sprite;
        levelText.text = "Lv. " + upgradeData.upgradeLevel;
    }

    public void Hidden()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        upgradeDetail.gameObject.SetActive(true);
        upgradeDetail.Display(upgradeData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        upgradeDetail.gameObject.SetActive(false);
    }
}
