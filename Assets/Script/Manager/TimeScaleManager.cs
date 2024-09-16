using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    static float setTimeDuration = 0;
    static bool m_isPause = false;
    public static bool isPaused { get { return m_isPause; } }

    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(setTimeDuration > 0)
        {
            setTimeDuration -= Time.deltaTime;
            if(setTimeDuration <= 0)
            {
                Time.timeScale = 1;
            }
        }
    }

    public static void SetTime(float newTimeScale, float duration)
    {
        if (newTimeScale <= 0)
            return;

        setTimeDuration = duration;
        Time.timeScale = newTimeScale;
    }

    public static void PauseGame()
    {
        setTimeDuration = 0;
        Time.timeScale = 0;
        m_isPause = true;
    }

    public static void ResumeGame()
    {
        setTimeDuration = 0;
        Time.timeScale = 1;
        m_isPause = false;
    }
}
