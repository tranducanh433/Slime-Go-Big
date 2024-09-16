using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    [Header("Wizard Setting")]
    [SerializeField] float attackCD = 7f;
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] Transform shootPoint;
    [SerializeField] float fireballSpeed = 6f;
    [SerializeField] float chargingTime = 1f;

    GameObject chargingFireball;
    float currentCD = 0f;

    protected override void StartFunc()
    {
        currentCD = attackCD;
        OnDead.AddListener(RemoveChargingFireball);
    }

    void Update()
    {
        currentCD -= Time.deltaTime;
        if(currentCD <= 0)
        {
            StartCoroutine(ShootFireballCO());

            currentCD = attackCD;
        }
    }

    public void RemoveChargingFireball(Enemy enemy)
    {
        if(chargingFireball != null)
        {
            Destroy(chargingFireball);
            StopAllCoroutines();
        }

        //Disable script
        enabled = false;
    }

    IEnumerator ShootFireballCO()
    {
        GameObject fireball = Instantiate(fireballPrefab, shootPoint.position, Quaternion.identity, shootPoint);
        chargingFireball = fireball;

        yield return new WaitForSeconds(chargingTime);

        fireball.transform.parent = null;
        EnemyFireball enemyFireball = fireball.GetComponent<EnemyFireball>();
        Transform player = GameObject.Find("Player").transform;
        enemyFireball.SetData(player.position, fireballSpeed);

        chargingFireball = null;
    }
}
