using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float explodeRadius = 3f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] GameObject explodeEffect;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.position += Vector2.down * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("Ground"))
        {
            Instantiate(explodeEffect, transform.position, Quaternion.identity);

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explodeRadius, enemyLayer);
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].GetComponent<IHitable>().TakeDamage(2);
            }

            Destroy(gameObject);
        }
    }
}
