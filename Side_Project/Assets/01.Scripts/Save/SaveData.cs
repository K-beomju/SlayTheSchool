using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class UserInfoData
{
    public double _myGold;

    public UserInfoData()
    {
        _myGold = 1;
    }
}


[Serializable]
public class SaveData
{
    public int version = 1; 
    public UserInfoData _myUserInfoDt = new UserInfoData();

    public SaveData()
    {

    }
}
