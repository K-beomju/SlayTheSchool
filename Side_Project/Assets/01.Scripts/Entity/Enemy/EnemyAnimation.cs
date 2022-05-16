using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


public class EnemyAnimation : MonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;

    private readonly string hashIdle = "Ani_Idle01";
    private readonly string hashHit = "Ani_Hit";


    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }
 

    public void HitAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, hashHit, false);
        skeletonAnimation.AnimationState.AddAnimation(0, hashIdle, true, 0);

    }
}
