using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game")]
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] float bossTime = 600;
    [SerializeField] Image blackEffect;
    [SerializeField] GameObject loseScreen;

    float currentTime = 0;

    [Header("Tutorial")]
    [SerializeField] SpriteRenderer moonFace;
    [SerializeField] Sprite moonTalkFace;
    [SerializeField] Sprite moonNormalFace;
    [SerializeField] GameObject[] moonDialogues;
    [SerializeField] float delayEachDialogue = 2;

    [Header("Spawner")]
    [SerializeField] EnemySpawner spawner;
    [SerializeField] float timeToAddNewEnemy = 45;

    [Header("Audio")]
    [SerializeField] BackgroundMusicManager backgroundMusicManager;
    [SerializeField] List<float> changeMusicTime = new List<float>();
    bool changeMusicEffect = false;

    float currentTimeToAddNewEnemy = 45;
    bool isBossSpawned = false;
    bool isTiming = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTimeToAddNewEnemy = timeToAddNewEnemy;
        StartCoroutine(ShowTutorial());

        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.OnDead.AddListener(PlayerLose);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTiming)
            return;

        currentTime += Time.deltaTime;

        AddNewEnemy();
        UpdateTimeUI();
        ChangeMusic();
    }

    IEnumerator ShowTutorial()
    {
        if (!PlayerSetting.Instance.skipTutorial)
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < moonDialogues.Length; i++)
            {
                moonFace.sprite = moonNormalFace;
                if (i - 1 >= 0)
                {
                    moonDialogues[i - 1].SetActive(false);
                }
                yield return new WaitForSeconds(0.5f);

                moonDialogues[i].SetActive(true);
                moonFace.sprite = moonTalkFace;

                yield return new WaitForSeconds(delayEachDialogue);
            }
        }
        moonDialogues[moonDialogues.Length - 1].SetActive(false);
        moonFace.sprite = moonNormalFace;

        EnemySpawner.spawning = true;
        isTiming = true;
    }


    void AddNewEnemy()
    {
        if(currentTime >= currentTimeToAddNewEnemy && currentTime < bossTime)
        {
            spawner.AddNewEnemy();
            currentTimeToAddNewEnemy += timeToAddNewEnemy;
        }

        if(currentTime >= bossTime && !isBossSpawned)
        {
            isBossSpawned = true;
            spawner.SpawnBoss();
        }
    }

    void UpdateTimeUI()
    {
        int minute = (int)currentTime / 60;
        int second = (int)currentTime % 60;

        timeText.text = minute + " : " + (second < 10 ? "0" : "") + second;
    }

    void ChangeMusic()
    {
        if (changeMusicTime.Count == 0)
            return;

        if(currentTime >= changeMusicTime[0])
        {
            changeMusicTime.RemoveAt(0);
            backgroundMusicManager.PlayNextMusic(changeMusicEffect);

            if(!changeMusicEffect)
                changeMusicEffect = true;
        }
    }

    public void ChangeToCreditScene()
    {
        isTiming = false;
        StartCoroutine(ChangeToCreditSceneCO());
    }

    public void PlayerLose()
    {
        StartCoroutine(PlayerLoseCO());
    }

    IEnumerator PlayerLoseCO()
    {
        isTiming = false;
        yield return new WaitForSeconds(1);
        loseScreen.SetActive(true);
    }

    IEnumerator ChangeToCreditSceneCO()
    {
        //Instant kill all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().InstantKill();
        }

        //Remove Bullets
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Enemy Bullet");
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().InstantKill();
        }

        // Black transition
        float aSpeed = Time.deltaTime / 4;
        while(blackEffect.color.a < 1)
        {
            blackEffect.color = new Color(blackEffect.color.r,
                blackEffect.color.g,
                blackEffect.color.b,
                blackEffect.color.a + aSpeed);

            yield return null;
        }

        SceneManager.LoadScene("TyScene");
    }


    // Button

    public void TryAgain()
    {
        PlayerSetting.Instance.skipTutorial = true;
        SceneManager.LoadScene("GameScene");
    }
}
