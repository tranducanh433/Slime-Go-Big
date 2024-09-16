using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSwordUpgrade : MonoBehaviour
{
    [SerializeField] float cooldown = 6;
    [SerializeField] int swordAmount = 1;

    void Start()
    {
        SlimeSwordSkill slimeSwordSkill = GameObject.Find("Player").GetComponentInChildren<SlimeSwordSkill>();
        slimeSwordSkill.isActive = true;
        slimeSwordSkill.cooldown = cooldown;
        slimeSwordSkill.numberOfSword = swordAmount;

        Destroy(gameObject);
    }
}
