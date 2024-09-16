using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHitable
{
    [Header("Enemy's base stats")]
    [SerializeField] protected int maxHP = 2;
    [SerializeField] protected float startAtY = -0.5f;
    [SerializeField] protected int m_expGive = 1;
    [SerializeField] protected bool instantKillable = true;

    [Header("Other component")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected Collider2D[] hitboxs;
    [SerializeField] ParticleSystem movingEffect;

    protected bool m_isDead = false;
    protected int currentHP = 0;

    [HideInInspector] public UnityEvent<Enemy> OnDead = new UnityEvent<Enemy>();
    [HideInInspector]  public UnityEvent OnTakeDamage = new UnityEvent();

    public bool isDead { get { return m_isDead; } }
    public int expGive { get { return m_expGive; } }

    private void Start()
    {
        currentHP = maxHP;
        transform.position = new Vector3(transform.position.x, startAtY, 0);
        StartFunc();
    }

    protected virtual void StartFunc()
    {

    }

    public void InstantKill()
    {
        if(instantKillable)
            Dead();
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        anim.SetTrigger("Take Damage");
        OnTakeDamage.Invoke();
        if(currentHP <= 0)
        {
            Dead();
        }
    }


    void Dead()
    {
        m_isDead = true;
        anim.SetBool("Dead", true);
        DisableHitbox();
        if(movingEffect != null)
        {
            movingEffect.Stop();
        }

        OnDead.Invoke(this);
    }

    protected void DisableHitbox()
    {
        for (int i = 0; i < hitboxs.Length; i++)
        {
            hitboxs[i].enabled = false;
        }
    }

    protected void EnableHitbox()
    {
        for (int i = 0; i < hitboxs.Length; i++)
        {
            hitboxs[i].enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(transform);
        }
    }
}
