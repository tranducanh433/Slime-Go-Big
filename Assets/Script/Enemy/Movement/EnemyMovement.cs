using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 2.5f;
    public bool moving = true;
    
    protected int m_direction = 1;

    public int direction { get { return m_direction; } }

    protected void RotateSprite()
    {
        transform.localScale = new Vector3(Mathf.Sign(m_direction), 1, 1);
    }
}
