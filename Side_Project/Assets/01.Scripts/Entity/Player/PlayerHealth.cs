using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    #region SerializeField Fields
    [SerializeField] private Vector3 offset;
    #endregion

    #region private Fields
    #endregion

    protected override void Awake()
    {
      
    }



    protected override void Start()
    {
        base.Start();
    }



    [ContextMenu("123123")]
    public void Test()
    {
        OnDamage(1);
    }


    public override void OnDamage(int damage)
    {
        base.OnDamage(damage);
    }

    public override void Die()
    {

    }
}
