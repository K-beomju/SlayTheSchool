using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TurnManager : Singleton<TurnManager>
{
    [SerializeField] private Button turnEndBtn;
    [SerializeField] private CanvasGroup turnPanel;
    [SerializeField] private Text turnText;

    private TMP_Text turnBtnText;
    private Enemy enemy;

    protected override void Awake()
    {
        base.Awake();
        enemy = FindObjectOfType<Enemy>();
        turnBtnText = turnEndBtn.GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        turnEndBtn.onClick.AddListener(() => TurnEnd());
        TurnStart();
    }

    public void TurnEnd()
    {
        turnBtnText.text = "상대 턴";
        CardManager.Instance.PlayerTurnEnd();
        turnEndBtn.interactable = false;
        enemy.Attack();


        turnText.text = "상대 턴";
        turnPanel.DOFade(1, 0.5f).SetLoops(2, LoopType.Yoyo);
    }



    public void TurnStart()
    {
        turnBtnText.text = "턴 종료";
        turnEndBtn.interactable = true;
        CardManager.Instance.PlayerTurnStart();

        turnText.text = "내 턴";
        turnPanel.DOFade(1, 0.5f).SetLoops(2, LoopType.Yoyo);
    }
}
