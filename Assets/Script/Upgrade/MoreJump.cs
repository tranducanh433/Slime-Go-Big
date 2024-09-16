using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreJump : MonoBehaviour
{
    [SerializeField] int additionalJump = 1;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        player.additionalJump = additionalJump;
        Destroy(gameObject);
    }
}
