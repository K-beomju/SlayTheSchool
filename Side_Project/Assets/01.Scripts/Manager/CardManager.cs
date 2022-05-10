using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
    [SerializeField] private ItemSO itemSO;
    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private Transform cardSpawnPoint;
    [SerializeField] private List<Card> myCards;
    [SerializeField] private Transform cardLeft;
    [SerializeField] private Transform cardRight;
    private List<Item> itemBuffer;


    private void Start()
    {
        SetupItemBuffer();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            AddCard();

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
        originCardPRss = RoundAlignment(cardLeft, cardRight, myCards.Count, 0.5f, Vector3.one * 1.9f);

        var targetCards = myCards; 
        for(int i = 0; i< targetCards.Count;i++)
        {
            var targetCard = targetCards[i];

            targetCard.originPRS = originCardPRss[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
         float[] objLerps = new float[objCount];
        List<PRS>  results = new List<PRS>();
        
        switch(objCount)
        {
            case 0: objLerps = new float[] { 0.4f }; break;
            case 1: objLerps = new float[] { 0.27f, 0.73f }; break;
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
            if (objCount >= 2)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve; 
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }

            results.Add(new PRS(targetPos, targetRot, scale));

        }
        return results;
    }


}
