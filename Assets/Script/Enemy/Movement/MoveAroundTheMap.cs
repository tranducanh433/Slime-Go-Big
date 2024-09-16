using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAroundTheMap : EnemyMovement
{
    [Header("Map Border")]
    [SerializeField] float minX = -20;
    [SerializeField] float maxX = 20;
    [SerializeField] float restTime = 0.5f;
    bool resting = false;


    private void Start()
    {
        if (transform.position.x < 0)
            m_direction = 1;
        else
            m_direction = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(moving && !resting)
        {
            Move();
            RotateSprite();
        }
    }

    void Move()
    {
        transform.position += Vector3.right * moveSpeed * m_direction * Time.deltaTime;

        if((m_direction == 1 && transform.position.x > maxX) || (m_direction == -1 && transform.position.x < minX))
        {
            StartCoroutine(ChangeDirectionCO());
        }
    }

    IEnumerator ChangeDirectionCO()
    {
        resting = true;
        yield return new WaitForSeconds(restTime);
        resting = false;
        m_direction *= -1;
    }
}
