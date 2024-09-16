using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyCoinUpgrade : MonoBehaviour
{
    [SerializeField] string powerUpSpawnerTag = "Power Up Spawner";

    void Start()
    {
        GameObject[] spawner = GameObject.FindGameObjectsWithTag(powerUpSpawnerTag);

        for (int i = 0; i < spawner.Length; i++)
        {
            spawner[i].GetComponent<PowerUpSpawner>().haveSpecialDecreaseCD = true;
        }

        Destroy(gameObject);
    }
}
