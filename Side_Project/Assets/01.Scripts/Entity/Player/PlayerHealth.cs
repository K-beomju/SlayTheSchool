using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    #region SerializeField Fields
    [SerializeField] private Vector3 offset;
    public bool isSpawn { get; set; }

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
        entity.health = initHealth;
        isSpawn = false;
        hpbar = GameManager.GetEntityHPBar();
    }

    private void Update()
    {
        if(!isSpawn)
        hpbar.Reset(Utils.ScreenTransform(this.transform, offset), 1);
        
    }



    public override void OnDamage(int damage)
    {

    }

    public override void Die()
    {

    }
}
