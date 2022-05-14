using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardPickButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    //[SerializeField] private GameObject pickPanel;
    [SerializeField] private GameObject picDecPanel;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        if (picDecPanel == null)
            picDecPanel = GameObject.Find("PickPanel");

        picDecPanel.SetActive(false);
        //exitButton.onClick.AddListener(DetativePanel);
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
        //if(pickPanel != null)
        //    pickPanel.gameObject.SetActive(true);


        if (picDecPanel.activeSelf)
            picDecPanel.SetActive(false);
    }

    //public void DetativePanel()
    //{
    //    pickPanel.gameObject.SetActive(false);
    //}
}
