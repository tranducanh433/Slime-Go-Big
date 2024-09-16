using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompAttackEffect : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] float lifeTime = 1.5f;
    [SerializeField] int direction = 1;
    [SerializeField] ParticleSystem effect;
    [SerializeField] BoxCollider2D hitBox;

    float currentLifeTime = 0;

    private void Start()
    {
        currentLifeTime = lifeTime;
    }


    // Update is called once per frame
    void Update()
    {
        if(currentLifeTime > 0)
        {
            transform.position += Vector3.right * direction * speed * Time.deltaTime;

            currentLifeTime -= Time.deltaTime;
            if (currentLifeTime <= 0)
            {
                effect.Stop();
                hitBox.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IHitable>().TakeDamage(2);
        }
    }
}
