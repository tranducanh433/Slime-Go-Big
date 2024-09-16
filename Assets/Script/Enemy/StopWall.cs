using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopWall : MonoBehaviour
{
    [SerializeField] float minY = 1.5f;

    Transform player;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float yPos = Mathf.Clamp(player.position.y, minY, float.MaxValue);
        transform.position = new Vector2(transform.position.x, yPos);
    }
}
