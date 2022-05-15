using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamageText : MonoBehaviour
{
    private TMP_Text damageTxt;
    private Sequence sequence;

    private void Awake()
    {
        damageTxt = GetComponent<TMP_Text>();

        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(3, 1)).
            Join(damageTxt.DOFade(0, 1)).
            AppendCallback(() => gameObject.SetActive(false));
    }


    public void SetValueText(int damage)
    {
        damageTxt.text = damage.ToString();
        sequence.Play();
    }

    public void SetPositionData(Vector3 position, Quaternion rot)
    {
        transform.position = position;
        transform.rotation = rot;
    }

}
