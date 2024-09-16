using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("Base Player Skill")]
    [SerializeField] protected float heatUpgradeDecrease = 2f;

    public GameObject skillPrefab;
    public float cooldown = 6f;
    public bool haveHeatUpgrade = false;
    public bool isActive = false;

    protected float currentCD = 0;

}
