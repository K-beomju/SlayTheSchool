using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] private SpriteRenderer card;
    [SerializeField] private SpriteRenderer cardImage;

    [SerializeField] private TMP_Text costTMP;
    [SerializeField] private TMP_Text nameTMP;
    [SerializeField] private TMP_Text typeTMP;
    [SerializeField] private TMP_Text descriptionTMP;



    public Item item;
    public PRS originPRS;
    private bool isFront;


    public void Setup(Item item, bool isFront)
    {
        this.item = item;
        this.isFront = isFront;

        if(this.isFront)
        {
            cardImage.sprite = this.item.sprite;
            nameTMP.text = this.item.name;
            costTMP.text = this.item.cost.ToString();
            typeTMP.text = this.item.type.ToString();
            descriptionTMP.text = this.item.description;
        }
        else
        {
            nameTMP.text = "";
            costTMP.text = "";
            typeTMP.text = "";
            descriptionTMP.text = "";
        }
    }

    void OnMouseOver()
    {
        CardManager.Instance.CardMouseOver(this);
    }

    void OnMouseExit()
    {
        CardManager.Instance.CardMouseExit(this);
    }

    void OnMouseDown()
    {
        CardManager.Instance.CardMouseDown();
    }

    void OnMouseUp()
    {
        CardManager.Instance.CardMouseUp();
    }

    public void SetDeleteObject()
    {
        Destroy(gameObject, 1);
    }

    public void MoveTransform(PRS prs, bool useDotween , float dotWeenTime = 0)
    {
        if(useDotween)
        {
            transform.DOMove(prs.pos, dotWeenTime);
            transform.DORotateQuaternion(prs.rot, dotWeenTime);
            transform.DOScale(prs.scale, dotWeenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
}
