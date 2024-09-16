using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] GameObject meteorPrefab;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float spawnAtY;
    [SerializeField] float BFFDecrease = 2f;
    public float cooldown = 10;
    public bool activated = false;
    public bool haveBFFUpgrade = false;

    float currentCD = 0;

    private void Update()
    {
        if(activated)
        {
            currentCD -= Time.deltaTime;
            if(currentCD <= 0 )
            {
                float randX = Random.Range(minX, maxX);
                Vector2 spawnPos = new Vector2(randX, spawnAtY);
                Instantiate(meteorPrefab, spawnPos, Quaternion.identity);

                currentCD = cooldown - (haveBFFUpgrade ? BFFDecrease : 0);
            }
        }
    }
}
