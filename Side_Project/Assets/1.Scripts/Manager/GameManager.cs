using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Text testTxt;

    protected override void Awake()
    {
        base.Awake();
    }


    public void SetText()
    {
        testTxt.text = MGSaveData.instance.GetUserInfoData()._myGold.ToString();
    }

    public void UpGold()
    {
        MGSaveData.instance.GetUserInfoData()._myGold++;
    }


}
