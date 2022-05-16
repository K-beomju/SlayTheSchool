using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject attackEffect;
    private ObjectPooling<SkillObject> attackPool;

    [SerializeField] private GameObject damageText;
    private ObjectPooling<DamageText> damageTxtPool;

    [SerializeField] private GameObject biteEffect;
    private ObjectPooling<SpineSkillObject> bitePool;

    [SerializeField] private GameObject punchEffect;
    private ObjectPooling<SpineSkillObject> punchPool;

    [SerializeField] private GameObject takeMoneyEffect;
    private ObjectPooling<SpineSkillObject> takeMoneyPool;

    protected override void Awake()
    {
        base.Awake();
        attackPool = new ObjectPooling<SkillObject>(attackEffect, this.transform, 5);
        damageTxtPool = new ObjectPooling<DamageText>(damageText, this.transform, 15);
        bitePool = new ObjectPooling<SpineSkillObject>(biteEffect, this.transform, 5);
        punchPool = new ObjectPooling<SpineSkillObject>(punchEffect, this.transform, 5);
        takeMoneyPool = new ObjectPooling<SpineSkillObject>(takeMoneyEffect, this.transform, 5);

    }

    private void Start()
    {
        SoundManager.Instance.PlayBGMSound("BGM");
    }

    public static SkillObject GetAttackEffect()
    {
        return Instance.attackPool.GetOrCreate();
    }


    public static DamageText GetDamageText()
    {
        return Instance.damageTxtPool.GetOrCreate();
    }

    public static SpineSkillObject GetBiteEffect()
    {
        return Instance.bitePool.GetOrCreate();
    }

    public static SpineSkillObject GetPunchEffect()
    {
        return Instance.punchPool.GetOrCreate();
    }

    public static SpineSkillObject GetTakeMoneyEffect()
    {
        return Instance.takeMoneyPool.GetOrCreate();
    }
}
