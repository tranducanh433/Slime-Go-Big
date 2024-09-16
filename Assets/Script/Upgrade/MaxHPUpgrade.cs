using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHPUpgrade : MonoBehaviour
{
    [SerializeField] int maxHPAmount = 1;

    void Start()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.AddMaxHP(maxHPAmount);
    }

}
