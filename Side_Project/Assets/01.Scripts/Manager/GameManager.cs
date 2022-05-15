using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using MEC;

public class GameManager : Singleton<GameManager>
{

    protected override void Awake()
    {
        base.Awake();
    }

  



}



//public void Prob()
//{
//       public RectTransform canvas;

//[Header("Pooling Objs")]
//[SerializeField] private GameObject hpBarPrefab;
//private ObjectPooling<EntityHPbar> barPool;


//protected override void Awake()
//{
//    base.Awake();
//    barPool = new ObjectPooling<EntityHPbar>(hpBarPrefab, canvas, 3);
//}

//public static EntityHPbar GetEntityHPBar()
//{
//    return Instance.barPool.GetOrCreate();
//}
//}