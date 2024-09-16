using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rorating : MonoBehaviour
{
    [SerializeField] float m_rotateSpeed = 360f;
    [SerializeField] bool m_clockWise = true;
    [SerializeField] bool m_rotate_on_start = true;

    bool stop = false;

    public float rotateSpeed { get { return m_rotateSpeed; } set {  m_rotateSpeed = value; } }
    public bool clockWise { get { return m_clockWise;} set { m_clockWise = value; } }


    private void Start()
    {
        if (!m_rotate_on_start)
            stop = true;
    }

    void Update()
    {
        if (stop) return;
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);    
    }

    public void Stop()
    {
        stop = true;
    }
    public void StartRotate()
    {
        stop = false;
    }
    public void ResetAngle()
    {
        stop = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
