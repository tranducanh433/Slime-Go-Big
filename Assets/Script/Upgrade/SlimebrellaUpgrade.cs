using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimebrellaUpgrade : MonoBehaviour
{
    [SerializeField] float duration = 7;

    void Start()
    {
        PlayerMovement playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerMovement.slimebrellaDuration = duration;
        playerMovement.isSlimebrellaActive = true;

        Destroy(gameObject);
    }
}
