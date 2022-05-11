using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardButtonCanvas : MonoBehaviour
{
    [SerializeField] private Button pickButton;
    [SerializeField] private Button throwButton;

    [SerializeField] private Text pickCountText;
    [SerializeField] private Text throwCountText;

    private void Start()
    {
        PickDeckCount(CardManager.Instance.itemBuffer.Count);
        CardManager.pickCardAction += PickDeckCount;
        CardManager.throwCardAction += ThrowDeckCount;

    }

    // ³²¾ÆÀÖ´Â µ¦ÀÇ Ä«µå °¹¼ö 
    public void PickDeckCount(int count)
    {
        pickCountText.text = count.ToString();
    }

    // ³²¾ÆÀÖ´Â µ¦ÀÇ Ä«µå °¹¼ö 
    public void ThrowDeckCount(int count)
    {
        throwCountText.text = count.ToString();
    }

}
