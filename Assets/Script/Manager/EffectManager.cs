using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] ParticleSystem healEffect;
    [SerializeField] Animator healAnim;

    [SerializeField] Animator takeDamageAnim;

    Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        player.OnHeal.AddListener(TriggerHealEffect);
        player.OnTakeDamage.AddListener(TriggerTakeDamageEffect);
    }

    public void TriggerHealEffect()
    {
        healEffect.Play();
        healAnim.SetTrigger("Trigger Effect");
    }

    public void TriggerTakeDamageEffect()
    {
        takeDamageAnim.SetTrigger("Trigger Effect");
    }
}
