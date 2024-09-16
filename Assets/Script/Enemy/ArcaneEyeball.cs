using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArcaneEyeball : Enemy
{
    [Header("Arcane Eyeball Setting")]
    [SerializeField] float teleportDistance = 8;
    [SerializeField] EnemyMovement enemyMovement;
    [SerializeField] float teleportTime = 1f;
    [SerializeField] ParticleSystem teleportEffect;
    [SerializeField] SpriteRenderer enemySprite;

    Transform player;
    bool checkDistance = true;

    protected override void StartFunc()
    {
        player = GameObject.Find("Player").transform;

        OnDead.AddListener(DisableScript);
    }

    // Update is called once per frame
    void Update()
    {
        if (!checkDistance)
            return;

        float xDistance = (transform.position.x - player.position.x) * enemyMovement.direction;

        if (xDistance >= teleportDistance)
        {
            StartCoroutine(TeleportCO());
        }
    }

    public void DisableScript(Enemy enemy)
    {
        enabled = false;
    }

    IEnumerator TeleportCO()
    {
        checkDistance = false;
        DisableHitbox();
        enemyMovement.enabled = false;
        enemySprite.enabled = false;
        teleportEffect.Play();
        anim.SetBool("Appear", false);

        Vector2 teleTo = player.position + new Vector3(-teleportDistance * enemyMovement.direction, 0, 0); ;
        float speed = Vector2.Distance(teleTo, player.position) / teleportTime;

        while((Vector2)transform.position != teleTo)
        {
            transform.position = Vector2.MoveTowards(transform.position, teleTo, speed * Time.deltaTime);
            yield return null;
        }

        checkDistance = true;
        EnableHitbox();
        enemyMovement.enabled = true;
        enemySprite.enabled = true;
        teleportEffect.Stop();
        anim.SetBool("Appear", true);
    }
}
