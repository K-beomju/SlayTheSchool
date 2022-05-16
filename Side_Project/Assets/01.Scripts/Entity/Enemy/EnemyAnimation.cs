using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


public class EnemyAnimation : MonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;
    [SerializeField] private AnimationReferenceAsset[] animClip;

    public enum AnimState
    {
        Idle,
        Hit,
        Die
    }
    public AnimState _animState;


    private readonly string hashIdle = "Ani_Idle01";
    private readonly string hashHit = "Ani_Hit";

    private string currentAnimName;


    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    private void Start()
    {
        SetCurrentAnimation(AnimState.Idle);
    }

    private void AsyncAnimation(AnimationReferenceAsset animClip, bool loop, float timeScale = 1f)
    {
        skeletonAnimation.state.SetAnimation(0, animClip, loop).TimeScale = timeScale;

        if(animClip.name.Equals(hashHit))
            skeletonAnimation.AnimationState.AddAnimation(0, hashIdle, true, 0);

        skeletonAnimation.loop = loop;
        skeletonAnimation.timeScale = timeScale;

        currentAnimName = animClip.name;

        _animState = AnimState.Idle;
    }

    private void SetCurrentAnimation(AnimState _state)
    {
        switch(_state)
        {
            case AnimState.Idle:
                AsyncAnimation(animClip[(int)AnimState.Idle], true);
                break;
                case AnimState.Hit:
                AsyncAnimation(animClip[(int)AnimState.Hit], false);
                break;
                case AnimState.Die:
                AsyncAnimation(animClip[(int)AnimState.Die], false);
                break;
        }
    }

    public void HitState()
    {
        _animState = AnimState.Hit;
        SetCurrentAnimation(_animState);

    }

    public void DieState()
    {
        _animState = AnimState.Die;
        SetCurrentAnimation(_animState);

    }

 
}
