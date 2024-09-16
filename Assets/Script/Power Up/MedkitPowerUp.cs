using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitPowerUp : PowerUp
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player.Heal())
            {
                OnPickUp.Invoke(gameObject);
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
