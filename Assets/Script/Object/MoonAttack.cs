using MyExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonAttack : MonoBehaviour
{
    [Header("Laser Setting")]
    [SerializeField] float minX = -14f;
    [SerializeField] float maxX = 14f;
    [SerializeField] float atY = -0.5f;
    [SerializeField] float xDistance = 6f;
    [SerializeField] float dealDamageDelay = 0.5f;
    [SerializeField] float laserSurviveTime = 2f;
    [SerializeField] float BFFDecrease = 3f;
    [SerializeField] Transform[] shootPos;
    [SerializeField] Animator[] laserAnims;
    [SerializeField] ParticleSystem[] laserEffects;
    [SerializeField] LayerMask enemyLayer;

    [Header("Moon Face")]
    [SerializeField] SpriteRenderer faceSR;
    [SerializeField] Animator faceAnim;
    [SerializeField] Sprite happyFace;
    [SerializeField] Sprite chargingFace;
    [SerializeField] Sprite shootingFace;

    [HideInInspector] public float cooldown = 20f;
    [HideInInspector] public bool isActivated = false;
    [HideInInspector] public bool isLevel3 = false;
    [HideInInspector] public bool haveBFFUpgrade = false;

    float currentCD = 0f;



    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            ShootLaser();
        }
    }

    void ShootLaser()
    {
        currentCD -= Time.deltaTime;

        if (currentCD <= 0)
        {
            int laserCount = isLevel3 ? 3 : 1;
            for (int i = 0; i < laserCount; i++)
            {
                Vector2 startPos = new Vector2(Random.Range(minX, maxX), atY);
                int direction = Random.Range(0, 2) == 0 ? 1 : -1;
                Vector2 endPos = startPos + (Vector2.right * xDistance) * direction;

                StartCoroutine(ShootLaserCO(shootPos[i].position, startPos, endPos, laserAnims[i], laserEffects[i]));
            }
            
            currentCD = cooldown - (haveBFFUpgrade ? 0 : BFFDecrease);
        }
    }

    IEnumerator ShootLaserCO(Vector2 shootPos, Vector2 startPos, Vector2 endPos, Animator laserAnim, ParticleSystem laserEffect)
    {
        faceSR.sprite = chargingFace;
        yield return new WaitForSeconds(1f);

        faceAnim.SetTrigger("Charging");
        yield return new WaitForSeconds(1.5f);
        faceSR.sprite = shootingFace;

        laserAnim.SetBool("Appear", true);
        laserEffect.Play();
        Vector2 currentPos = startPos;
        int moveDirection = startPos.x < endPos.x ? 1 : -1;
        float currentDealDamageCD = dealDamageDelay;

        while ( moveDirection == 1
            ? currentPos.x < endPos.x
            : currentPos.x > endPos.x )
        {
            // Adjust Laser Position and Rotation
            Vector2 shootDirection = currentPos - shootPos;
            laserEffect.transform.position = currentPos;

            Vector2 laserPos = (shootDirection / 2) + shootPos;
            laserAnim.transform.position = laserPos;

            float laserAngle = shootDirection.ToAngle();
            laserAnim.transform.rotation = Quaternion.Euler(0, 0, laserAngle);

            float distance = shootDirection.magnitude;
            laserAnim.GetComponent<SpriteRenderer>().size = new Vector2(distance, 1);


            // Deal Damage
            currentDealDamageCD -= Time.deltaTime;
            if(currentDealDamageCD <= 0)
            {
                DealDamage(laserPos, new Vector2(distance, 0.5f), laserAngle);
                currentDealDamageCD = dealDamageDelay;
            }

            //Move
            float speed = xDistance / laserSurviveTime;
            currentPos += moveDirection * Vector2.right * speed * Time.deltaTime; 

            yield return Time.deltaTime;
        }

        laserAnim.SetBool("Appear", false);
        laserEffect.Stop();
        faceSR.sprite = happyFace;
    }

    void DealDamage(Vector2 point, Vector2 size, float angle)
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(point, size, angle, enemyLayer);
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].GetComponent<IHitable>().TakeDamage(1);
        }
    }
}
