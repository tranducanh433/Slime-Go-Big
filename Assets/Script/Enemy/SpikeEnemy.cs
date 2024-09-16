using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnemy : Enemy
{
    [SerializeField] Animator warningAnim;
    [SerializeField] float speed = 10f;
    Vector2 spawnPos;

    protected override void StartFunc()
    {
        StartCoroutine(FindingPosCO());
    }

    IEnumerator FindingPosCO()
    {
        m_isDead = true;

        Vector2 pos;
        do
        {
            pos = SpikePositionManager.GetPosition(gameObject);
            yield return null;
        }
        while (pos == new Vector2(-999, -999));

        spawnPos = pos;
        StartCoroutine(AppearCO());
    }

    IEnumerator AppearCO()
    {
        // Spawn under the ground
        Vector2 appearPos = new Vector2(spawnPos.x, spawnPos.y - 2);
        transform.position = appearPos;

        // Show warning
        warningAnim.gameObject.SetActive(true);
        warningAnim.transform.position = spawnPos;
        warningAnim.SetBool("Ending", true);

        yield return new WaitForSeconds(1.5f);

        // Hidden warning
        warningAnim.gameObject.SetActive(false);

        //Go up from the ground
        while((Vector2)transform.position != spawnPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, spawnPos, speed * Time.deltaTime);
            yield return null;
        }

        m_isDead = false;
    }
}
