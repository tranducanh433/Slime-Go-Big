using MyExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSword : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] float delayTime = 0.4f;
    [SerializeField] GameObject hitEffect;

    Transform target;
    bool startMoving;

    Vector3 moveDir = Vector3.zero;

    void Start()
    {
        StartCoroutine(StartChasingCO());
    }

    IEnumerator StartChasingCO()
    {
        yield return new WaitForSeconds(delayTime);
        startMoving = true;
    }

    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }
        else if(target.GetComponent<Enemy>().isDead)
        {
            target = null;
            return;
        }

        moveDir = target.position - transform.position;
        LookAtTarget();

        if (!startMoving) return;

        ChaseTarget();
    }


    void ChaseTarget()
    {
        transform.position += moveDir.normalized * speed * Time.deltaTime;

        if (Vector2.Distance(transform.position, target.position) <= 0.5f)
        {
            target.GetComponent<IHitable>().TakeDamage(2);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void LookAtTarget()
    {
        float angle = moveDir.ToAngle() - 225;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length <= 0)
            return;

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetComponent<Enemy>().isDead) 
                continue;

            if (target == null)
            {
                target = enemies[i].transform;
                continue;
            }

            float nearestDistance = Vector2.Distance(transform.position, target.position);
            float currentDistance = Vector2.Distance(transform.position, enemies[i].transform.position);
            if(nearestDistance > currentDistance)
            {
                target = enemies[i].transform;
            }
        }
    }
}
