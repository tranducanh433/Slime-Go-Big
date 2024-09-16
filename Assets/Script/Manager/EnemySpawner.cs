using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPools = new List<GameObject>();
    [SerializeField] int maxEnemies = 10;
    [SerializeField] float spawnCD = 1.0f;
    [SerializeField] Transform[] spawnPos;
    [SerializeField] int startNumberOfEnemy = 1;
    [SerializeField] GameObject bossPrefab;
    [SerializeField] GameObject[] enemySpawnDuringBoss;

    [Header("Component")]
    [SerializeField] GameManager gameManager;

    List<GameObject> spawnableEnemies = new List<GameObject>();
    List<GameObject> lastSpawnedEnemy = new List<GameObject>();
    List<GameObject> currentSpawnedEnemies = new List<GameObject>();
    int notRepeatLength = 0;
    float currentCD = 0;
    Player player;

    public static bool spawning = false;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        for (int i = 0; i < startNumberOfEnemy; i++)
        {
            spawnableEnemies.Add(enemyPools[0]);
            enemyPools.RemoveAt(0);
        }

        notRepeatLength = spawnableEnemies.Count / 2;
    }


    void Update()
    {
        if (spawning)
        {
            SpawnMonster();
        }
    }

    public void AddNewEnemy()
    {
        if (enemyPools.Count <= 0)
            return;

        spawnableEnemies.Add(enemyPools[0]);
        enemyPools.Remove(enemyPools[0]);
        notRepeatLength = (spawnableEnemies.Count + lastSpawnedEnemy.Count) / 2;
    }

    public void SpawnBoss()
    {
        // Spawn Boss
        Vector2 _spawnPos = spawnPos[Random.Range(0, 2)].position;
        GameObject spawnEnemy = Instantiate(bossPrefab, _spawnPos, Quaternion.identity);
        spawnEnemy.GetComponent<Enemy>().OnDead.AddListener(EndGame);

        spawnableEnemies.Clear();
        lastSpawnedEnemy.Clear();

        for (int i = 0; i < enemySpawnDuringBoss.Length; i++)
        {
            spawnableEnemies.Add(enemySpawnDuringBoss[i]);
        }

        notRepeatLength = spawnableEnemies.Count / 2;
    }

    void SpawnMonster()
    {
        currentCD -= Time.deltaTime;

        if(currentCD <= 0 && currentSpawnedEnemies.Count < maxEnemies)
        {
            // Spawn Enemy
            GameObject selectSpawn = spawnableEnemies[Random.Range(0, spawnableEnemies.Count)];
            Vector2 _spawnPos = spawnPos[Random.Range(0, 2)].position;
            GameObject spawnEnemy = Instantiate(selectSpawn, _spawnPos, Quaternion.identity);
            
            // Add to current enemies in arena
            currentSpawnedEnemies.Add(spawnEnemy);

            // Add to the list that revent spawn repeat enemy
            spawnableEnemies.Remove(selectSpawn);
            lastSpawnedEnemy.Add(selectSpawn);
            if(lastSpawnedEnemy.Count > notRepeatLength)
            {
                spawnableEnemies.Add(lastSpawnedEnemy[0]);
                lastSpawnedEnemy.RemoveAt(0);
            }

            // Add listener to On Dead event
            Enemy enemy = spawnEnemy.GetComponent<Enemy>();
            enemy.OnDead.AddListener(EnemyDead);
            enemy.OnDead.AddListener(player.DefeatEnemy);

            currentCD = spawnCD;
        }
    }

    void EnemyDead(Enemy enemy)
    {
        currentSpawnedEnemies.Remove(enemy.gameObject);
        currentSpawnedEnemies.RemoveAll(enemy => enemy == null);
    }

    public void EndGame(Enemy enemy)
    {
        gameManager.ChangeToCreditScene();
        spawning = false;
    }
}
