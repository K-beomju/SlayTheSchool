using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    [SerializeField] private HpSlider hpSlider;
    private PlayerAnimation playerAnim;
    private DamageText damageText;




    protected override void Awake()
    {
        playerAnim = GetComponent<PlayerAnimation>();
    }

    protected override void Start()
    {
        base.Start();
        hpSlider.SetHpbar(curHp, maxHp);
        OnDeath += DeathEvent;


    }

    public override void OnDamage(int damage)
    {    
        base.OnDamage(SkillManager.Instance.LostShieldValue(damage));
       
        hpSlider.SetHpbar(curHp, maxHp);
        damageText = GameManager.GetDamageText();

        damageText.SetValueText(damage);
        damageText.SetPositionData(new Vector3(transform.position.x - 1f,
            transform.position.y + 3f, 0), Utils.QI);

        if (curHp > 0) // 현재 체력이 0 이상일때만 애니메이션 실행
            playerAnim.HitState();
        else
            hpSlider.SetDeath();

    }

    public void DeathEvent()
    {

    }

    public override void Die()
    {
        base.Die();

    }
}
