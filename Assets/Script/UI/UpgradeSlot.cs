using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Effect")]
    [SerializeField] Sprite slimeUpgradeIcon;
    [SerializeField] Color slimeUpgradeColor;

    [SerializeField] Sprite powerUpIcon;
    [SerializeField] Color powerUpColor;

    [SerializeField] Sprite friendIcon;
    [SerializeField] Color friendColor;

    [SerializeField] Sprite skillIcon;
    [SerializeField] Color skillColor;

    [SerializeField] Color level1Color;
    [SerializeField] Color level2Color;
    [SerializeField] Color level3Color;


    [Header("UI")]
    [SerializeField] Image border;
    [SerializeField] Image typeIcon;
    [SerializeField] GameObject selected;

    [SerializeField] TextMeshProUGUI upgradeNameText;
    [SerializeField] Image upgradeIcon;
    [SerializeField] TextMeshProUGUI typeText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Animator anim;
    [SerializeField] bool interactable = true;

    [Header("Event")]
    public UnityEvent<UpgradeData> OnSelected = new UnityEvent<UpgradeData> ();

    UpgradeData upgradeData;

    public void Display(UpgradeData upgradeData)
    {
        this.upgradeData = upgradeData;

        if(interactable)
            anim.SetBool("Appear", true);

        switch (upgradeData.upgradeType)
        {
            case UPGRADE_TYPE.SLIME_UPGRADE:
                typeIcon.sprite = slimeUpgradeIcon;
                border.color = slimeUpgradeColor;
                typeText.text = "Slime Upgrade";
                break;

            case UPGRADE_TYPE.POWER_UP:
                typeIcon.sprite = powerUpIcon;
                border.color = powerUpColor;
                typeText.text = "Power-Up";
                break;

            case UPGRADE_TYPE.FRIEND:
                typeIcon.sprite = friendIcon;
                border.color = friendColor;
                typeText.text = "Friend";
                break;

            case UPGRADE_TYPE.SKILL:
                typeIcon.sprite = skillIcon;
                border.color = skillColor;
                typeText.text = "Skill";
                break;
        }

        upgradeNameText.text = upgradeData.upgradeName;
        upgradeIcon.sprite = upgradeData.sprite;

        if(interactable)
            descriptionText.text = upgradeData.description;
        else
            descriptionText.text = upgradeData.baseDescription;

        switch (upgradeData.upgradeLevel)
        {
            case 1:
                upgradeNameText.color = level1Color;
                break;

            case 2:
                upgradeNameText.color = level2Color;
                break;

            case 3:
                upgradeNameText.color = level3Color;
                break;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!interactable) return;

        anim.SetBool("MouseEnter", true);
        selected.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!interactable) return;

        anim.SetBool("MouseEnter", false);
        selected.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!interactable) return;

    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (!interactable) return;

        OnSelected.Invoke(upgradeData);
        selected.gameObject.SetActive(false);
        anim.SetBool("Appear", false);
    }
}
