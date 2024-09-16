using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerUp : MonoBehaviour
{
    [HideInInspector] public UnityEvent<GameObject> OnPickUp = new UnityEvent<GameObject>();
    [HideInInspector] public PowerUpSpawner powerUpSpawner;
}
