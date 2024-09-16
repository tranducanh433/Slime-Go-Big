using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonUpgrade : MonoBehaviour
{
    [SerializeField] float jumpForce = 15;
    void Start()
    {
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        player.jumpForce = jumpForce;
        Destroy(gameObject);
    }
}
