using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Entity : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private SpriteRenderer character;

    [SerializeField] private TMP_Text nameTMP;
    [SerializeField] private TMP_Text attackTMP;
    [SerializeField] private TMP_Text healthTMP;

    public PlayerHealth ph { get; set; }

    public int attack;
    public int health;
    public Vector3 originPos;
    public bool isBossOrEmpty;

    private void Awake()
    {
        ph = GetComponent<PlayerHealth>();
    }

    public void SetUp(Item item)
    {
        attack = item.attack;
        health = item.hp;

        this.item = item;
        character.sprite = this.item.sprite;
        nameTMP.text = this.item.name;

        attackTMP.text = attack.ToString();
        healthTMP.text = health.ToString();
    }

    public void MoveTransform(Vector3 pos , bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
            transform.DOMove(pos, dotweenTime).OnComplete(() =>
            {
                if (ph != null) ph.isMove = true;
            });
        else
            transform.position = pos;
    }
}
