using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
using TMPro;

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

    private int throwCount = 0;     // 버린 카드 갯수
    public int spawnCardCount;      // 생성할 카드 갯수 

    // cost
    [SerializeField] private TMP_Text costText;
    private int maxCost = 3;
    private int cost;

    // ETC
    [SerializeField] private GameObject bezierArrow;
    [SerializeField] private GameObject targetSlot;

    // Effect
    private SkillObject attackEffect;
    private DamageText damageText;

    protected override void Awake()
    {
        base.Awake();
        SetupItemBuffer();
    }

    private void Start()
    {
        StartCoroutine(SpawnCardCo());
        cost = maxCost;
        costText.text = String.Format("{0} / {1}", cost, maxCost);
    }

    private IEnumerator SpawnCardCo()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < spawnCardCount; i++)
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
            myCards[i].SetDeleteObject();
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
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            Item item = itemSO.items[i];
            for (int j = 0; j < item.count; j++)
                itemBuffer.Add(item);
        }

        // Shuffle
        for (int i = 0; i < itemBuffer.Count; i++)
        {
            int rand = Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;

        }
    }


    private void Update()
    {
        if (isCardDrag)
        {
            if (selectCard.item.type != TypeEnum.공격)
                CardDrag();

            bezierArrow.SetActive(selectCard.item.type == TypeEnum.공격);
        }
        else
            bezierArrow.SetActive(false);


        DetectCardArea();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            AddCard();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            StartCoroutine(ExitCardCo());
    }

    private void AddCard()
    {
        if (itemBuffer.Count == 0) // 뽑을 카드가 없다면 
        {
            SetupItemBuffer(); // 다시 뽑음 

            // Card Value Text Update
            pickCardAction(itemBuffer.Count);
            throwCount = 0; // Reset
            throwCardAction(throwCount);
        }

        var cardObj = Instantiate(cardPrefab, cardSpawnPoint.position, Utils.QI);
        var card = cardObj.GetComponent<Card>();
        card.Setup(PopItem(), true);
        myCards.Add(card);

        SetOriginOrder();

        pickCardAction(itemBuffer.Count);
        CardAlignment();
    }

    public void SetOriginOrder()
    {
        int count = myCards.Count;

        for (int i = 0; i < count; i++)
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
        for (int i = 0; i < targetCards.Count; i++)
        {
            var targetCard = targetCards[i];

            targetCard.originPRS = originCardPRss[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 0.3f);
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>();

        switch (objCount)
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

        for (int i = 0; i < objCount; i++)
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


    public bool TryPutCard()
    {
        if (cost == 0) return false;

        switch (selectCard.item.action)
        {
            case ActionEnum.책넣기:
                {
                    SkillManager.Instance.Shield(selectCard.item.defense);
                    UseCard();
                }
                break;
            case ActionEnum.죽빵:
                RaycastHit2D[] hit = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward, LayerMask.NameToLayer("Enemy"));
                if (Array.Exists(hit, x => x.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")))
                {
                    var hits = hit[0];

                    EnemyHealth eh = hits.collider.GetComponent<EnemyHealth>();
                    if (eh != null)
                        eh.OnDamage(selectCard.item.attack);
                    CameraManager.ShakeCam(1, 0.2f);

                    attackEffect = GameManager.GetAttackEffect();
                    attackEffect.SetPositionData(new Vector3(hits.transform.position.x - 0.3f,
                        hits.transform.position.y + 0.5f, 0), Utils.QI);

                    FindObjectOfType<Player>().AttackMovement();

                    damageText = GameManager.GetDamageText();

                    damageText.SetValueText(selectCard.item.attack);
                    damageText.SetPositionData(new Vector3(hits.transform.position.x + 1f,
                        hits.transform.position.y + 0.3f, 0), Utils.QI);

                    UseCard();
                }
                else
                {
                    CardAlignment();
                    SetOriginOrder();
                }
                break;
        }
        return true;
    }

    public void UseCard()
    {
        cost -= selectCard.item.cost;
        costText.text = String.Format("{0} / {1}", cost, maxCost);

        myCards.Remove(selectCard);
        selectCard.transform.DOKill();
        DestroyImmediate(selectCard.gameObject);

        selectCard = null;
        CardAlignment();

        throwCount++;
        throwCardAction(throwCount);

        targetSlot.SetActive(false);
    }

    #region MyCard

    public void CardMouseOver(Card card)
    {
        if (isCardDrag) return;

        if (!isCardDrag)
            selectCard = card;
        EnlargeCard(true, card);
    }

    public void CardMouseExit(Card card)
    {
        if (selectCard.item.type != TypeEnum.공격 || !isCardDrag)
            EnlargeCard(false, card);
    }

    public void CardMouseDown()
    {
        isCardDrag = true;

        if (selectCard.item.type == TypeEnum.공격) // isCardDrag의 선택된 카드가 공격일때 
        {
            Vector3 enlarPos = new Vector3(0, -4.43f, 0f);
            selectCard.MoveTransform(new PRS(enlarPos, Utils.QI, cardPrefab.transform.localScale), true, 0.2f);
        }

    }

    public void CardMouseUp()
    {
        isCardDrag = false;

        if (!onCardArea)
        {
            TryPutCard();
        }

    }

    void CardDrag()
    {
        if (!onCardArea)
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
        if (isEnlarge)
        {
            Vector3 enlarPos = new Vector3(card.originPRS.pos.x, -4.3f, -10f);
            card.MoveTransform(new PRS(enlarPos, Utils.QI, cardPrefab.transform.localScale), false);
        }
        else
            card.MoveTransform(card.originPRS, false);

        card.GetComponent<Order>().SetMostFrontOrder(isEnlarge);
    }

    #endregion

    public bool isAttackCardArea()
    {
        if (selectCard == null) return false;

        if (selectCard.item.type == TypeEnum.공격 && isCardDrag && !onCardArea)
        {
            return true;
        }
        return false;
    }
}

