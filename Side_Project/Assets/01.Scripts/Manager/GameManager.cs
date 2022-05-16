using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject attackEffect;
    private ObjectPooling<SkillObject> attackPool;
    [SerializeField] private GameObject damageText;
    private ObjectPooling<DamageText> damageTxtPool;

    protected override void Awake()
    {
        base.Awake();
        attackPool = new ObjectPooling<SkillObject>(attackEffect, this.transform, 5);
        damageTxtPool = new ObjectPooling<DamageText>(damageText, this.transform, 5);

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
}
