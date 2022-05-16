using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private GameObject fireParticle;
    [SerializeField] private GameObject fireNoodleicon;

    private void Start()
    {
        OnOutline(false);
    }

    public void OnOutline(bool isActive)
    {
        if(isActive)
        mat.shader = Shader.Find("Spine/Outline/Skeleton");
        else
        mat.shader = Shader.Find("Spine/Skeleton");

        fireNoodleicon.SetActive(isActive);
       fireParticle.SetActive(isActive);
    }

    public void AttackMovement()
    {
        transform.DOMoveX(transform.position.x + 1, 0.2f).SetLoops(2, LoopType.Yoyo);
    }
}
