using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    [SerializeField] private HpSlider hpSlider;



    [ContextMenu("Damage")]
    public void Hit()
    {
        OnDamage(1);
    }


    protected override void Awake()
    {

    }

    protected override void Start()
    {
        base.Start();
        hpSlider.SetHpbar(curHp, maxHp);
    }

    public override void OnDamage(int damage)
    {
        base.OnDamage(damage);
        hpSlider.SetHpbar(curHp, maxHp);

    }

    public override void Die()
    {

    }
}
