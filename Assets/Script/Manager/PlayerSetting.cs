using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerSetting : MonoBehaviour
{
    [SerializeField] bool m_skipTutorial;
    [SerializeField] float m_musicVolume;
    [SerializeField] float m_soundVolume;
  
    public bool skipTutorial { get { return m_skipTutorial; } set { m_skipTutorial = value; } }
    public float musicVolume { get { return m_musicVolume; } set { m_musicVolume = value; } }
    public float sfxVolume { get { return m_soundVolume; } set { m_soundVolume = value; } }


    public static PlayerSetting Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

}
