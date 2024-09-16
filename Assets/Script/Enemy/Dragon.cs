using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Enemy
{
    [Header("Fire Rain")]
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] float phase1CD = 1;
    [SerializeField] float phase2CD = 1;
    [SerializeField] float fireRainMoveSpeed = 10;
    [SerializeField] int fireAmountLv1 = 5;
    [SerializeField] int fireAmountLv2 = 5;
    [SerializeField] float minX = -19;
    [SerializeField] float maxX = 19;
    [SerializeField] float atY = 19;

    float CD = 0;
    float currentAmountOfFire = 0;
    float currentCD = 0;

    void Update()
    {
        CheckPhase();
        Attack();
    }

    void CheckPhase()
    {
        if(currentHP <= maxHP / 2)
        {
            CD = phase2CD;
            currentAmountOfFire = fireAmountLv2;
        }
        else
        {
            CD = phase1CD;
            currentAmountOfFire = fireAmountLv1;
        }
    }

    void Attack()
    {
        currentCD -= Time.deltaTime;

        if (currentCD <= 0)
        {
            for (int i = 0; i < currentAmountOfFire; i++)
            {
                Vector2 randPos = new Vector2(Random.Range(minX, maxX), atY);
                GameObject fireball = Instantiate(fireballPrefab, randPos, Quaternion.identity);
                EnemyFireball enemyFireball = fireball.GetComponent<EnemyFireball>();
                enemyFireball.SetData(randPos + Vector2.down, fireRainMoveSpeed);
            }

            currentCD = CD;
        }
    }
}
