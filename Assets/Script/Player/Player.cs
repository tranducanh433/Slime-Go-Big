using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int m_maxHP = 5;
    [SerializeField] float invincibleTime = 1.5f;
    [SerializeField] float instantKillRadius = 5f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] int expScalePerLevel = 2;
    [SerializeField] int startEXPRequire = 5;

    [Header("Effect")]
    [SerializeField] GameObject explodeEffect;

    [Header("Component")]
    [SerializeField] Animator playerAnim;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Collider2D playerCollider;

    [Header("Bubble Power up")]
    [SerializeField] GameObject bubble;
    [SerializeField] Animator bubbleAnim;
    public bool haveBubble { get { return bubble.activeSelf; } }

    [Header("Shooting Power Up")]
    [SerializeField] GameObject slimeBullet;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] Transform shootPos;
    [SerializeField] float shootCD = 0.25f;
    float currentShootingDuration = 0;
    float currentShootCD = 0;

    [Header("Unity Event")]
    [HideInInspector] public UnityEvent OnTakeDamage = new UnityEvent();
    [HideInInspector] public UnityEvent OnHeal = new UnityEvent();
    [HideInInspector] public UnityEvent OnIncreaseMaxHP = new UnityEvent();
    [HideInInspector] public UnityEvent OnDefeatEnemy = new UnityEvent();
    [HideInInspector] public UnityEvent OnLevelUp = new UnityEvent();
    [HideInInspector] public UnityEvent OnDead = new UnityEvent();

    int m_level = 0;
    int m_EXPRequire = 5;
    int m_currentEXP = 0;

    int m_currentHP = 0;
    bool invincible = false;

    public int maxHP { get { return m_maxHP; } }
    public int currentHP { get { return m_currentHP; } }
    public int level { get { return m_level; } }
    public int EXPRequire { get { return m_EXPRequire; } }
    public int currentEXP { get { return m_currentEXP; } }


    private void Start()
    {
        m_currentHP = m_maxHP;
        m_EXPRequire = startEXPRequire;
    }

    private void Update()
    {
        ShootingPowerUp();
    }

    #region Power Up
    void ShootingPowerUp()
    {
        if(currentShootingDuration > 0)
        {
            currentShootingDuration -= Time.deltaTime;
            currentShootCD -= Time.deltaTime;
            if(currentShootCD <= 0)
            {
                int shootDirection = transform.localScale.x < 0 ? -1 : 1;
                GameObject _bullet = Instantiate(slimeBullet, shootPos.position, Quaternion.identity);
                _bullet.GetComponent<SlimeBullet>().SetData(bulletSpeed, shootDirection);
                currentShootCD = shootCD;
            }
        }
    }
    public void EnableShootingPowerUp(float duration)
    {
        currentShootingDuration = duration;
        currentShootCD = 0;
    }


    public void GetBubble(float duration)
    {
        StartCoroutine(BubbleCO(duration));
    }

    IEnumerator BubbleCO(float duration)
    {
        bubble.SetActive(true);
        yield return new WaitForSeconds(duration - 2);
        bubbleAnim.SetBool("Ending", true);
        yield return new WaitForSeconds(2);
        bubbleAnim.SetBool("Ending", false);
        bubble.SetActive(false);
    }
    #endregion

    public void TakeDamage(Transform attacker)
    {
        if (invincible)
            return;

        if(!bubble.activeSelf)
        {
            m_currentHP--;
            OnTakeDamage.Invoke();
        }

        StartCoroutine(TakeDamageCO());
        if (m_currentHP <= 0)
        {
            playerAnim.SetBool("Dead", true);
            enabled = false;
            playerMovement.enabled = false;
            playerCollider.enabled = false;

            OnDead.Invoke();
        }
    }

    public void DefeatEnemy(Enemy enemy)
    {

        m_currentEXP += enemy.expGive;
        if(m_currentEXP >= m_EXPRequire)
        {
            Heal();

            m_currentEXP -= m_EXPRequire;
            m_level++;
            m_EXPRequire = startEXPRequire + m_level * expScalePerLevel;

            StopCoroutine(TakeDamageCO());
            invincible = false;

            OnLevelUp.Invoke();
        }

        OnDefeatEnemy.Invoke();
    }

    IEnumerator TakeDamageCO()
    {
        invincible = true;
        playerAnim.SetTrigger("Take Damage");

        // Slow the time and spawn explode effect
        TimeScaleManager.SetTime(0.2f, 0.1f);
        Instantiate(explodeEffect, transform.position, Quaternion.identity);
        
        // Deal damage to all enemies nearby
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, instantKillRadius, enemyLayer);
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].GetComponent<IHitable>().InstantKill();
        }

        yield return new WaitForSeconds(invincibleTime);
        invincible = false;
    }

    public void AddMaxHP(int maxHp)
    {
        m_maxHP += maxHp;
        m_currentHP = m_maxHP;
        OnIncreaseMaxHP.Invoke();
    }

    public bool Heal()
    {
        if(m_currentHP >= m_maxHP)
            return false;
        else
        {
            m_currentHP++;
            OnHeal.Invoke();
            return true;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
