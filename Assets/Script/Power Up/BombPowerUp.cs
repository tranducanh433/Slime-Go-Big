using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPowerUp : PowerUp
{
    [SerializeField] GameObject bombPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerAttack player = collision.GetComponentInChildren<PlayerAttack>();
            player.Bounce();

            Instantiate(bombPrefab, transform.position, Quaternion.identity);
            
            OnPickUp.Invoke(gameObject);
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 0.1f);
        }
    }
}
