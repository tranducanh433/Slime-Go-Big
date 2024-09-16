using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public int maxAmount = 1;
    public float cd = 10;
    public float duration = 4;
    [SerializeField] float minX = -17;
    [SerializeField] float maxX = 17;
    public bool active = false;
    public bool haveSpecialDecreaseCD = false;
    public float specialDecreaseCD = 2;

    List<GameObject> spawedPowerUps = new List<GameObject>();
    float currentCD = 0;

    PlayerMovement player;
    Vector2 minPos;
    Vector2 maxPos;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if(active && spawedPowerUps.Count < maxAmount)
        {
            currentCD -= Time.deltaTime;

            if(currentCD <= 0)
            {
                Vector2 spawnPos = GetRandomSpawnPos();
                GameObject spawn = Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);
                spawedPowerUps.Add(spawn);
                spawn.GetComponent<PowerUp>().OnPickUp.AddListener(OnPickUp);
                spawn.GetComponent<PowerUp>().powerUpSpawner = this;
                currentCD = cd - (haveSpecialDecreaseCD ? specialDecreaseCD : 0);
            }
        }
    }

    public void OnPickUp(GameObject gameObject)
    {
        spawedPowerUps.Remove(gameObject);
    }

    Vector2 GetRandomSpawnPos()
    {
        minPos = new Vector2(minX, 2);
        maxPos = new Vector2(maxX, player.posibleJumpHeight);
        Vector2 rs = new Vector2(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y));
        
        while(Vector2.Distance(rs, player.transform.position) < 7)
        {
            rs = new Vector2(Random.Range(minPos.x, maxPos.x), Random.Range(minPos.y, maxPos.y));
        }

        return rs;
    }
}
