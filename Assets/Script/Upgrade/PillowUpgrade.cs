using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowUpgrade : MonoBehaviour
{
    [SerializeField] float bounceForce = 10;


    private void Start()
    {
        PlayerAttack playerAttack = GameObject.Find("Player").GetComponentInChildren<PlayerAttack>();
        playerAttack.bounceForce = bounceForce;
        Destroy(gameObject);
    }
}
