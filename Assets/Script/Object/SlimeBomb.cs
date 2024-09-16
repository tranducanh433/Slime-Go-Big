using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBomb : MonoBehaviour
{
    [SerializeField] float speed = 20;
    [SerializeField] float explodeRadius = 2;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject explodeEffect;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Ground"))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explodeRadius, enemyLayer);
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].GetComponent<IHitable>().TakeDamage(2);
            }

            Instantiate(explodeEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
