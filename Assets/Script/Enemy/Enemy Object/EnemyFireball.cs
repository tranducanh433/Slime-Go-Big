using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : MonoBehaviour
{
    [SerializeField] ParticleSystem m_particles;
    [SerializeField] Collider2D hitbox;

    public void SetData(Vector2 target, float speed)
    {
        GetComponent<Rigidbody2D>().velocity = speed * (target - (Vector2)transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(transform);
            StartCoroutine(DestroySelfCO());
        }
        if(collision.CompareTag("Wall") || collision.CompareTag("Ground"))
        {
            StartCoroutine(DestroySelfCO());
        }
    }

    IEnumerator DestroySelfCO()
    {
        m_particles.Stop();
        hitbox.enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);

    }
}
