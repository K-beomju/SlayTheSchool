using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{

    public void AttackMovement()
    {
        transform.DOMoveX(transform.position.x + 1, 0.2f).SetLoops(2, LoopType.Yoyo);
    }
}
