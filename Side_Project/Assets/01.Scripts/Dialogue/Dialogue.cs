using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    [Tooltip("캐릭터 이름")]
    public string name;

    [Tooltip("대사 내용")]
    public string[] contexts;

    [Tooltip("스킵라인")]
    public string[] skipNum;
}

[System.Serializable]
public class DialogueEvent
{
    //이벤트 이름
    public string name;

    public Dialogue[] dialogues;
}

