using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyHealth : LivingEntity
{
    [SerializeField] private HpSlider hpSlider;
    [SerializeField] private CanvasGroup scGroup;
    private EnemyAnimation enemyAnim;
    private DamageText damageText;


    protected override void Awake()
    {
        enemyAnim = GetComponent<EnemyAnimation>();
    }

    protected override void Start()
    {
        base.Start();
        hpSlider.SetHpbar(curHp, maxHp);
        OnDeath += DeathEvent;
    }

    public override void OnDamage(int damage)
    {
        base.OnDamage(damage + CardManager.Instance.fireCkValue);
        hpSlider.SetHpbar(curHp, maxHp);

        damageText = GameManager.GetDamageText();

        damageText.SetValueText(damage + CardManager.Instance.fireCkValue);
        damageText.SetPositionData(new Vector3(transform.position.x + 1f,
            transform.position.y + 2f, 0), Utils.QI);

        if(CardManager.Instance.fireCkValue != 0)
        {
        CardManager.Instance.fireCkValue = 0;
        FindObjectOfType<Player>().OnOutline(false);
        }

        if (curHp > 0) // 현재 체력이 0 이상일때만 애니메이션 실행
            enemyAnim.HitState();
        else
            hpSlider.SetDeath();

    }

    public void DeathEvent()
    {
        enemyAnim.DieState();
        scGroup.DOFade(1, 1).SetDelay(1).OnComplete(() => {
            FindObjectOfType<PlayerAnimation>().StopAnim();
            scGroup.interactable = true;
            });
    }

    public override void Die()
    {
        base.Die();
    }
}
