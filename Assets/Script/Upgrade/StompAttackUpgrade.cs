using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StompAttackUpgrade : MonoBehaviour
{
    [SerializeField] GameObject skillPrefab;

    void Start()
    {
        StompAttackSkill stompAttackSkill = GameObject.Find("Player").GetComponentInChildren<StompAttackSkill>();
        stompAttackSkill.isActive = true;
        stompAttackSkill.skillPrefab = skillPrefab;

        Destroy(gameObject);
    }
}
