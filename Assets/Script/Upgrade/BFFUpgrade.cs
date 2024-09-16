using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFFUpgrade : MonoBehaviour
{
    void Start()
    {
        MoonAttack moonAttack = GameObject.Find("Moon").GetComponent<MoonAttack>();
        moonAttack.haveBFFUpgrade = true;

        MeteorSpawner meteorSpawner = GameObject.Find("Meteor Spawner").GetComponent<MeteorSpawner>();
        meteorSpawner.haveBFFUpgrade = true;

        Destroy(gameObject);
    }
}
