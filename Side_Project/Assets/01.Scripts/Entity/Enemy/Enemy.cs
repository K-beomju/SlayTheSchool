using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject targetSlot;

    public void AttackMovement()
    {
        transform.DOMoveX(transform.position.x - 1, 0.2f).SetLoops(2, LoopType.Yoyo);
    }



    void OnMouseEnter()
    {
        if (CardManager.Instance.isAttackCardArea())
            targetSlot.SetActive(true);
    } 

    void OnMouseExit()
    {
        if(targetSlot.activeSelf)
            targetSlot.SetActive(false);
    }


}
