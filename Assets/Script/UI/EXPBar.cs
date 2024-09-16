using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPBar : MonoBehaviour
{
    [SerializeField] Slider expSlider;
    Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        player.OnDefeatEnemy.AddListener(UpdateEXPBar);
    }

    public void UpdateEXPBar()
    {
        expSlider.maxValue = player.EXPRequire;
        expSlider.value = player.currentEXP;
    }
}
