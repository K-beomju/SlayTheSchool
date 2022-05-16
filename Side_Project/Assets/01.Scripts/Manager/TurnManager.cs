using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnManager : Singleton<TurnManager>
{
    [SerializeField] private Button turnEndBtn;
    private TMP_Text turnText;
    private Enemy enemy;

    protected override void Awake()
    {
        base.Awake();
        enemy = FindObjectOfType<Enemy>();
        turnText = turnEndBtn.GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        turnEndBtn.onClick.AddListener(() => TurnEnd());
    }

    public void TurnEnd()
    {
        CardManager.Instance.PlayerTurnEnd();
        turnEndBtn.interactable = false;
        turnText.text = "상대 턴";
        enemy.Attack();
    }



    public void TurnStart()
    {
        turnText.text = "턴 종료";
        turnEndBtn.interactable = true;
        CardManager.Instance.PlayerTurnStart();
    }
}
