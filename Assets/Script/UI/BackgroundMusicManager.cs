using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] backgroundMusics;

    int currentMusic = 0;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(PlayMusicCO(false));
    }

    public void PlayNextMusic(bool chageMusicEffect)
    {
        currentMusic++;
        if(chageMusicEffect)
            StopAllCoroutines();
        StartCoroutine(PlayMusicCO(chageMusicEffect));
    }

    IEnumerator PlayMusicCO(bool chageMusicEffect)
    {
        if(chageMusicEffect)
        {
            while(audioSource.volume > 0)
            {
                audioSource.volume -= 2 * Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(1.5f);
            audioSource.clip = backgroundMusics[currentMusic];
            audioSource.Play();
            audioSource.volume = 1;

            StartCoroutine(PlayMusicCO(false));
        }
        else
        {
            while (true)
            {
                yield return null;
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = backgroundMusics[currentMusic];
                    audioSource.Play();
                    audioSource.volume = 1;
                }
            }
        }
    }
}
