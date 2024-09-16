using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorUpgrade : MonoBehaviour
{
    [SerializeField] string spawnerName = "Meteor Spawner";
    [SerializeField] float cooldown = 10f;

    void Start()
    {
        GameObject meteorSpawner = GameObject.Find(spawnerName);
        MeteorSpawner spawner = meteorSpawner.GetComponent<MeteorSpawner>();
        spawner.cooldown = cooldown;
        spawner.activated = true;

    }
}
