using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using MEC;
using TMPro;

public class SkillManager : Singleton<SkillManager>
{
    #region Shield
    [SerializeField] private Image shieldImage;
    [SerializeField] private float endRtShieldPos;

    [SerializeField] private TMP_Text shieldText;
    public int shieldValue;

    [SerializeField] private SpriteRenderer shieldSprite;
    [SerializeField] private float endTrShieldPos;
    private Vector3 shieldSpriteTr;

    [SerializeField] private Image shieldSliderImage;
    [SerializeField] private Sprite shieldSliderSprite;

    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        shieldImage.gameObject.SetActive(false);
        shieldText.gameObject.SetActive(false);
        shieldText.text = shieldValue.ToString();

    }

    #region Player Direct

    [ContextMenu("Shield")]
    public void Shield(int value) // 스킬 방어 연출
    {
        shieldSpriteTr = shieldSprite.transform.position;

        shieldSliderImage.sprite = shieldSliderSprite;

        shieldText.gameObject.SetActive(true);
        shieldValue += value;
        shieldText.text = shieldValue.ToString();

        shieldImage.gameObject.SetActive(true);
        shieldImage.DOFade(1, 1);
        shieldImage.GetComponent<RectTransform>().DOAnchorPosY(endRtShieldPos, 0.7f).OnComplete(() =>
        {
            SoundManager.Instance.PlayFXSound("Shield");
            RectTransform shieldrt = shieldText.GetComponent<RectTransform>();
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(shieldrt.DOScale(new Vector2(1.5f, 1.5f), 0.5f).SetLoops(2, LoopType.Yoyo)).Insert(0.1f, shieldText.DOFade(1, 0.5f));
            mySequence.Play();
        });

        shieldSprite.DOFade(1, 1).OnComplete(() => shieldSprite.transform.position = shieldSpriteTr);
        shieldSprite.GetComponent<Transform>().DOLocalMoveY(endTrShieldPos, 0.7f).OnComplete(() => shieldSprite.DOFade(0, 0.3f));
    }


    #endregion



}



//public void Prob()
//{
//       public RectTransform canvas;

//[Header("Pooling Objs")]
//[SerializeField] private GameObject hpBarPrefab;
//private ObjectPooling<EntityHPbar> barPool;


//protected override void Awake()
//{
//    base.Awake();
//    barPool = new ObjectPooling<EntityHPbar>(hpBarPrefab, canvas, 3);
//}

//public static EntityHPbar GetEntityHPBar()
//{
//    return Instance.barPool.GetOrCreate();
//}
//}