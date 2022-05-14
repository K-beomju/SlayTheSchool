using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    #region SerializeField Fields
    [SerializeField] private Vector3 offset;
    public bool isMove { get; set; }

    #endregion

    #region private Fields
    private Entity entity;
    private EntityHPbar hpbar;
    #endregion

    protected override void Awake()
    {
        if (entity == null)
            entity = GetComponent<Entity>();
    }



    protected override void Start()
    {
        maxHp = entity.health;
        base.Start();
        isMove = false;
        hpbar = GameManager.GetEntityHPBar();
        hpbar.Reset(this.transform.position + offset, curHp);

    }

    private void Update()
    {
        if(!isMove)
        hpbar.SetPosition(this.transform.position + offset);
        
    }

    public void ActiveMove()
    {
        isMove = false;
    }

    [ContextMenu("123123")]
    public void Test()
    {
        OnDamage(1);
    }


    public override void OnDamage(int damage)
    {
        base.OnDamage(damage);
        hpbar.SetValue(curHp, maxHp);

    }

    public override void Die()
    {

    }
}
