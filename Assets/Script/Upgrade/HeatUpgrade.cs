using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatUpgrade : MonoBehaviour
{
    void Start()
    {
        SlimeSwordSkill slimeSwordSkill = GameObject.Find("Player").GetComponentInChildren<SlimeSwordSkill>();
        slimeSwordSkill.haveHeatUpgrade = true;

        StompAttackSkill stompAttackSkill = GameObject.Find("Player").GetComponentInChildren<StompAttackSkill>();
        stompAttackSkill.haveHeatUpgrade = true;

        Destroy(gameObject);
    }
}
