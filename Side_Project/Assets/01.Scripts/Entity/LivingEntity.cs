using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    public int initHealth;
    public int hp { get; protected set; }

    public bool dead;
    public event Action OnDeath;

    protected virtual void Awake()
    {
     
    }


    protected virtual void Start()
    {
        Debug.Log("부모 메서드 실행");
        dead = false;
        hp = initHealth;
    }

    public virtual void OnDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0 && !dead)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        if (OnDeath != null) OnDeath();
        dead = true;
    }
}