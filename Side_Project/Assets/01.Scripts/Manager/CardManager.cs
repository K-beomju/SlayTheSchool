using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class CardManager : Singleton<CardManager>
{
    [SerializeField] private ItemSO itemSO;
    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private Transform cardSpawnPoint;
    [SerializeField] private Transform cardExitPoint;

    [SerializeField] private List<Card> myCards;
    [SerializeField] private Transform cardLeft;
    [SerializeField] private Transform cardRight;

    private List<Item> itemBuffer;
    private Card selectCard;

    private bool isCardDrag;
    private bool onCardArea;

    public int cardCount; 

    private void Start()
    {
        SetupItemBuffer();
        StartCoroutine(SpawnCardCo());
    }

    private IEnumerator SpawnCardCo()
    {
        for(int i = 0; i < cardCount; i++)
        {
            AddCard();
            yield return new WaitForSeconds(0.2f);
        }

    }

    private IEnumerator ExitCardCo()
    {
        for (int i = 0; i < myCards.Count; i++)
        {
            myCards[i].MoveTransform(new PRS(cardExitPoint.position, myCards[i].transform.rotation, cardPrefab.transform.localScale), true, 0.3f);
            yield return new WaitForSeconds(0.1f);
        }
    }

   

    public Item PopItem()
    {
        if (itemBuffer.Count == 0)
            SetupItemBuffer();

        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        return item;
    }

    private void SetupItemBuffer()
    {
        itemBuffer = new List<Item>();

        for(int i = 0; i < itemSO.items.Length; i++)
        {
            Item item = itemSO.items[i];
            for(int j = 0; j < item.percent; j++)
                itemBuffer.Add(item);
        }
        
        for(int i = 0; i < itemBuffer.Count; i++)
        {
            int rand = Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }
    }



    private void Update()
    {
        if(isCardDrag)
        {
            CardDrag();
        }
        DetectCardArea();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(ExitCardCo());

        }

    }

    private void AddCard()
    {
        var cardObj = Instantiate(cardPrefab, cardSpawnPoint.position, Utils.QI);
        var card = cardObj.GetComponent<Card>();
        card.Setup(PopItem(), true);
        myCards.Add(card);

        SetOriginOrder();
        CardAlignment();
    }

    public void SetOriginOrder()
    {
        int count = myCards.Count;

        for(int i = 0; i < count; i++)
        {
            var targetCard = myCards[i];
            targetCard?.GetComponent<Order>().SetOriginOrder(i); 
        }
    }


    void CardAlignment()
    {
        List<PRS> originCardPRss = new List<PRS>();
        originCardPRss = RoundAlignment(cardRight, cardLeft, myCards.Count, 0.5f, cardPrefab.transform.localScale);

        var targetCards = myCards; 
        for(int i = 0; i< targetCards.Count;i++)
        {
            var targetCard = targetCards[i];

            targetCard.originPRS = originCardPRss[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 0.3f);
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
         float[] objLerps = new float[objCount];
        List<PRS>  results = new List<PRS>();

        switch(objCount)
        {
            
            case 0: objLerps = new float[] { 0.4f }; break;
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                float interval = 1f / (objCount - 1);
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = interval * i;
                break;
        }

        for(int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Utils.QI;
            if (objCount >= 3)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                targetPos.y += curve; 
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }

            results.Add(new PRS(targetPos, targetRot, scale));

        }
        return results;
    }

    #region MyCard

    public void CardMouseOver(Card card)
    {
        selectCard = card;
        EnlargeCard(true, card);
    }

    public void CardMouseExit(Card card)
    {
        EnlargeCard(false, card);
    }

    public void CardMouseDown()
    {
        isCardDrag = true;
    }

    public void CardMouseUp()
    {
        isCardDrag = false;

        if(!onCardArea)
        {
            Debug.Log(selectCard.item.name);

            myCards.Remove(selectCard);
            selectCard.transform.DOKill();
            DestroyImmediate(selectCard.gameObject);
            selectCard = null;
            CardAlignment();

        }
    }

    void CardDrag()
    {
        if(!onCardArea)
        {
            //selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale), false);
            selectCard.transform.position = Utils.MousePos;
        }
    }

    void DetectCardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);
        int layer = LayerMask.NameToLayer("CardArea");
        onCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
    }

    private void EnlargeCard(bool isEnlarge, Card card)
    {
        if(isEnlarge)
        {
            Vector3 enlarPos = new Vector3(card.originPRS.pos.x, -4.3f, -10f);
            card.MoveTransform(new PRS(enlarPos, Utils.QI, cardPrefab.transform.localScale), false); 
        }
        else
            card.MoveTransform(card.originPRS, false);

        card.GetComponent<Order>().SetMostFrontOrder(isEnlarge); 
    }

    #endregion
}

