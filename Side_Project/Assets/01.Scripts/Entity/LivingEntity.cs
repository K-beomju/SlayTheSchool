using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    public int curHp { get; set; }
    public int maxHp;

    public bool dead;
    public event Action OnDeath;

    protected virtual void Awake()
    {
     
    }


    protected virtual void Start()
    {
        dead = false;
        curHp = maxHp;
    }

    public virtual void OnDamage(int damage)
    {
        curHp -= damage;

        if (curHp <= 0 && !dead)
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