using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    public int curHp;
    public int maxHp;

    public bool dead;
    public event Action OnDeath;

    protected virtual void Awake()
    {
     
    }


    protected virtual void Start()
    {
        Debug.Log("부모 메서드 실행");
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