using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyExtension;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowMovement : MonoBehaviour
{
    [Header("Setting Movement")]
    [SerializeField] Transform target;
    [SerializeField] float speed = 5f;
    [SerializeField] Transform point;
    [SerializeField] float lineLength = 1.5f;
    [SerializeField] bool moveOnAwake = true;
    Vector2 targetPos;

    [Header("Debug")]
    [SerializeField] bool debug;

    bool moving;

    Rigidbody2D rb;

    private void Start()
    {
        moving = moveOnAwake;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (moving)
        {
            if(target != null)
                rb.MovePosition((Vector2)transform.position + (MoveDir() * speed * Time.fixedDeltaTime));
            else
                rb.MovePosition((Vector2)transform.position + (MoveDir(targetPos) * speed * Time.fixedDeltaTime));
        }
    }

    Vector2 MoveDir()
    {
        Vector2 targetDir = target.position - transform.position;

        Vector2[] dir = { (0f).ToDirection2D()  , (45f).ToDirection2D() , (90f).ToDirection2D() , (135f).ToDirection2D() ,
                          (180f).ToDirection2D(), (225f).ToDirection2D(), (270f).ToDirection2D(), (315f).ToDirection2D()};

        Vector2 result = targetDir.normalized;
        for (int i = 0; i < dir.Length; i++)
        {
            RaycastHit2D[] objInDir = Physics2D.LinecastAll(point.position, (Vector2)point.position + dir[i] * lineLength);

            for (int j = 0; j < objInDir.Length; j++)
            {
                if(objInDir[j].transform.gameObject != gameObject && objInDir[j].transform.gameObject != target.gameObject)
                {
                    result -= dir[i];
                    break;
                }
            }
        }

        return result.normalized;
    }
    Vector2 MoveDir(Vector2 target)
    {
        Vector2 targetDir = target - (Vector2)transform.position;

        Vector2[] dir = { (0f).ToDirection2D()  , (45f).ToDirection2D() , (90f).ToDirection2D() , (135f).ToDirection2D() ,
                          (180f).ToDirection2D(), (225f).ToDirection2D(), (270f).ToDirection2D(), (315f).ToDirection2D()};

        Vector2 result = targetDir.normalized;
        for (int i = 0; i < dir.Length; i++)
        {
            RaycastHit2D[] objInDir = Physics2D.LinecastAll(point.position, (Vector2)point.position + dir[i] * lineLength);

            for (int j = 0; j < objInDir.Length; j++)
            {
                if (objInDir[j].transform.gameObject != gameObject)
                {
                    result -= dir[i];
                    break;
                }
            }
        }

        return result.normalized;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetTarget(Vector2 target)
    {
        targetPos = target;
    }

    public void StopMoving()
    {
        moving = false;
    }
    public void StartMoving()
    {
        moving = true;
    }
}
