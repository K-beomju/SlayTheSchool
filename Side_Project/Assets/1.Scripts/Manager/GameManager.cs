using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
    }


    // More Effective Coroutine Guide
    // Timing.RunCoroutine(_TimeCo());
    // private IEnumerator<float> _TimeCo()
    // {
    //     yield return Timing.WaitForSeconds(3f);
    // }
}
