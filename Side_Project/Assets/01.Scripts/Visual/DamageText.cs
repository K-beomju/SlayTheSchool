using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamageText : MonoBehaviour
{
    private TMP_Text damageTxt;
    private void Awake()
    {
        damageTxt = GetComponent<TMP_Text>();
    }
    
}
