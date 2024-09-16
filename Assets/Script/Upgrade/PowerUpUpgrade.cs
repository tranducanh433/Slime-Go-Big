using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUpgrade : MonoBehaviour
{
    [SerializeField] string gameobjectName = "Bubble Power-up Spawner";
    [SerializeField] float cd = 40;
    [SerializeField] float duration = 4;
    [SerializeField] int maxAmount = 1;

    private void Start()
    {
        PowerUpSpawner powerUpSpawner = GameObject.Find(gameobjectName).GetComponent<PowerUpSpawner>();
        powerUpSpawner.active = true;
        powerUpSpawner.cd = cd;
        powerUpSpawner.duration = duration;
        powerUpSpawner.maxAmount = maxAmount;
        Destroy(gameObject);
    }
}
