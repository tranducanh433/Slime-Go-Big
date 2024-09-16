using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInteractSFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip onEnterSound;
    [SerializeField] AudioClip onClickSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(onClickSound != null || audioSource != null)
            audioSource.PlayOneShot(onClickSound);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnterSound != null || audioSource != null)
            audioSource.PlayOneShot(onEnterSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
