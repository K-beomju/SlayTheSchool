using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject attackEffect;
    private ObjectPooling<SkillObject> attackPool;

    protected override void Awake()
    {
        base.Awake();
        attackPool = new ObjectPooling<SkillObject>(attackEffect, this.transform, 5);
    }

    public static SkillObject GetAttackEffect()
    {
        return Instance.attackPool.GetOrCreate();
    }


}
