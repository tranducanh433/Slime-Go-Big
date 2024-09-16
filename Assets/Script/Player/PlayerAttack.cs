using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float bounceForce = 7.5f;
    [SerializeField] ParticleSystem JumpEffect;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy Head") /*&& rb.velocity.y <= 1*/)
        {
            collision.GetComponentInParent<IHitable>().TakeDamage(2);
            Bounce();
        }
    }

    public void Bounce()
    {
        JumpEffect.Play();
        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, bounceForce);
    }
}
