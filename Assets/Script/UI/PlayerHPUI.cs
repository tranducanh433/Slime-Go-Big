using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPUI : MonoBehaviour
{
    [SerializeField] GameObject[] hpIcons;

    Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        player.OnTakeDamage.AddListener(UpdateHPUI);
        player.OnHeal.AddListener(UpdateHPUI);
        player.OnIncreaseMaxHP.AddListener(UpdateHPUI);
        UpdateHPUI();
    }


    public void UpdateHPUI()
    {
        for (int i = 0; i < hpIcons.Length; i++)
        {
            hpIcons[i].SetActive(i < player.maxHP);
            SetHidden(hpIcons[i], player.currentHP <= i);
        }
    }

    void SetHidden(GameObject hpUI, bool hidden)
    {
        Animator anim = hpUI.GetComponentInChildren<Animator>();
        anim.SetBool("Hidden", hidden);
    }
}
