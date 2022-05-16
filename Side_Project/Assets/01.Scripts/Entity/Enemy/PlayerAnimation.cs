using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


public class PlayerAnimation : MonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;

    private readonly string hashIdle = "Ani_Idle01";

    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

 
}
