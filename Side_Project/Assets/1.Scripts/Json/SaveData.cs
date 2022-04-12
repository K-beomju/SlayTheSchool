using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;


[Serializable]
public class DTest
{
    public int _dtest;

    public DTest()
    {
        _dtest = 0;
    }
}

[Serializable]
public class SaveData
{
    public int version = 1; // 세이브 파일 버전
    public DTest _dTest = new DTest();

    public SaveData()
    {

    }
}
