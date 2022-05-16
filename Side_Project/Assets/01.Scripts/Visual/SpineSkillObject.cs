using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class SpineSkillObject : MonoBehaviour
{
    private SkeletonAnimation skel;
    [SerializeField] private string skillName;


    private void Awake()
    {
        skel = GetComponent<SkeletonAnimation>();
    }

    private void Start()
    {
        SkillMotion();    
    }
 

    private void SkillMotion()
    {
        skel.ClearState();
        skel.AnimationName = skillName;
        Spine.AnimationState state = skel.AnimationState;

        state.Complete += SkillDetactive;

        void SkillDetactive(TrackEntry entry)
        {
            state.End -= SkillDetactive;
            this.gameObject.SetActive(false);
        }

    }

    public void SetPositionData(Vector3 position, Quaternion rot)
    {
        transform.position = position;
        transform.rotation = rot;
    }

}
