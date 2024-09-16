using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSwordSkill : PlayerSkill
{
    [Header("Slime Sword Setting")]
    [SerializeField] Transform[] swordSpawnPos;

    public int numberOfSword = 1;


    void Update()
    {
        if (!isActive) return;

        currentCD -= Time.deltaTime;
        if(currentCD <= 0)
        {
            for (int i = 0; i < numberOfSword; i++)
            {
                Instantiate(skillPrefab, swordSpawnPos[i].position, Quaternion.identity);
            }
            currentCD = cooldown - (haveHeatUpgrade ? heatUpgradeDecrease : 0);
        }
    }
}
