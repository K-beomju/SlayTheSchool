using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pools
{
    public ObjectPooling<BOX> poolBox { get; set; }
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject box;
    private Pools pools = new Pools();


    protected override void Awake()
    {
        if (pools == null)
            pools = new Pools();
        ResetPoolEntity();
    }

    public void ResetPoolEntity() => pools.poolBox = new ObjectPooling<BOX>(box, transform);

    public static BOX GetCreateBox()
    {
        return Instance.pools.poolBox.GetOrCreate();
    }



}
