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

    public List<Item> itemBuffer { get; set; }
    private Card selectCard;

    private bool isCardDrag;
    private bool onCardArea;

    public static Action<int> pickCardAction = (x) => { };
    public static Action<int> throwCardAction = (x) => { };

    private int throwCount = 0;     // ¹ö¸°Ä«µå °¹¼ö
    private int maxCardCount = 0;   // Ä«µå µ¦ ÃÖ´ë °¹¼ö

    public int spawnCardCount;

    protected override void Awake()
    {
        base.Awake();
        SetupItemBuffer();
        maxCardCount = itemBuffer.Count;
    }


    private IEnumerator SpawnCardCo()
    {
        for(int i = 0; i < spawnCardCount; i++)
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
            throwCount++;
            throwCardAction(throwCount);
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = myCards.Count - 1; i >= 0; i--)
        {
              myCards.Remove(myCards[i]);
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

        // ADD
        for(int i = 0; i < itemSO.items.Length; i++)
        {
            Item item = itemSO.items[i];
            for(int j = 0; j < item.count; j++)
                itemBuffer.Add(item);
        }
        
        // Shuffle
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


        if(Input.GetKeyDown(KeyCode.Alpha1))
            AddCard();
        if(Input.GetKeyDown(KeyCode.Alpha2))
            StartCoroutine(ExitCardCo());
        

    }

    private void AddCard()
    {
        if (itemBuffer.Count == 0) // »ÌÀ» Ä«µå°¡ ¾ø´Ù¸é 
        {
            SetupItemBuffer(); // ´Ù½Ã »ÌÀ½ 

            // Card Value Text Update
            pickCardAction(itemBuffer.Count); 
            throwCount = 0; // Reset
            throwCardAction(throwCount);
        }

        var cardObj = Instantiate(cardPrefab, cardSpawnPoint.position, Utils.QI);
        var card = cardObj.GetComponent<Card>();
        card.Setup(PopItem(), true);
        myCards.Add(card);

        pickCardAction(itemBuffer.Count);
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
            
            case 0: objLerps = new float[] { 0.5f }; break;
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2:
                cardLeft.transform.position = new Vector3(-1.5f, cardLeft.position.y);
                cardRight.transform.position = new Vector3(1.5f, cardRight.position.y);
                objLerps = new float[] { 0.1f, 0.9f }; 
                break;
            case 3:
                cardLeft.transform.position = new Vector3(-3f, cardLeft.position.y);
                cardRight.transform.position = new Vector3(3f, cardRight.position.y);
                objLerps = new float[] { 0.1f, 0.5f, 0.9f };
                break;
            case 4:
                cardLeft.transform.position = new Vector3(-4f, cardLeft.position.y);
                cardRight.transform.position = new Vector3(4f, cardRight.position.y);
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = 1f / (objCount - 1) * i;
                break;
            case 5:
                cardLeft.transform.position = new Vector3(-4.5f, cardLeft.position.y);
                cardRight.transform.position = new Vector3(4.5f, cardRight.position.y);
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = 1f / (objCount - 1) * i;
                break;

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
            if (objCount >= 2)
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
            selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale), false);
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

