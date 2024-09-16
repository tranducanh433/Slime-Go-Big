using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompAttackSkill : PlayerSkill
{
    [Header("Stomp Attack Setting")]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] float spawnAtY = -0.5f;

    bool isJumped = false;

    void Update()
    {
        if (!isActive) return;

        CheckForStompAttack();
        CheckGround();
    }

    void CheckGround()
    {
        isJumped = !playerMovement.IsGrounded();
    }

    void CheckForStompAttack()
    {
        currentCD -= Time.deltaTime;

        if(isJumped 
            && playerMovement.IsGrounded()
            && currentCD <= 0)
        {
            Vector2 spawnPos = new Vector2(transform.position.x, spawnAtY);
            Instantiate(skillPrefab, spawnPos, Quaternion.identity);
            currentCD = cooldown - (haveHeatUpgrade ? heatUpgradeDecrease : 0);
        }
    }
}
