using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HpSlider : MonoBehaviour
{
    private Slider hpSlider;
    private Text hpText;
    private CanvasGroup cg;

    public Slider hpShadowSlider;

    public Text uiHpText;

    private void Awake()
    {
        hpSlider = GetComponent<Slider>();
        hpText = GetComponentInChildren<Text>();
        cg = GetComponent<CanvasGroup>();
    }
    
    public void SetHpbar(float curHp, float maxHp)
    {
        hpSlider.value = Mathf.Clamp(curHp / maxHp, 0, 1);
        hpText.text = string.Format("{0} / {1}", curHp, maxHp);
        if(uiHpText != null)
            uiHpText.text = string.Format("{0} / {1}", curHp, maxHp);
        hpShadowSlider.DOValue(curHp / maxHp, 1).SetDelay(0.5f);

    }

    public void SetDeath()
    {
        cg.DOFade(0, 1);
        hpText.text = "»ç¸Á";
    }

   
}
