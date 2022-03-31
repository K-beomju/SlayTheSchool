using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
  //  [SerializeField] private GameObject box;


    protected override void Awake()
    {
        base.Awake();
    //    ResetPoolEntity();
    }

  //  public void ResetPoolEntity() => poolBox = new ObjectPooling<BOX>(box, transform);

    // public static BOX GetCreateBox()
    // {
    //     return Instance.poolBox.GetOrCreate();
    // }

   

}
