using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    [SerializeField] private HpSlider hpSlider;
    private EnemyAnimation ea;


    [ContextMenu("Damage")]
    public void Hit()
    {
        OnDamage(1);
    }

    protected override void Awake()
    {
        ea = GetComponent<EnemyAnimation>();
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
        ea.HitAnimation();

    }

    public override void Die()
    {

    }
}
