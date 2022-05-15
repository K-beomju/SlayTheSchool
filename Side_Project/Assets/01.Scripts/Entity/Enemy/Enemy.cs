using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject targetSlot;
 

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
