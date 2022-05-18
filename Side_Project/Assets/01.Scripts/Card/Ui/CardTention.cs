using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardTention : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject picDecPanel;

    private void Start()
    {
        picDecPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        picDecPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        picDecPanel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (picDecPanel.activeSelf)
            picDecPanel.SetActive(false);
    }

}
