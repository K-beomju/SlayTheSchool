using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CardThrowButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject throwPanel;
    [SerializeField] private GameObject throwDecPanel;
    [SerializeField] private Button exitButton;


    private void Start()
    {
        if (throwDecPanel == null)
            throwDecPanel = GameObject.Find("ThrowPanel");

        throwDecPanel.SetActive(false);
        exitButton.onClick.AddListener(DetativePanel);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throwDecPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throwDecPanel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (throwPanel != null)
            throwPanel.gameObject.SetActive(true);


        if (throwDecPanel.activeSelf)
            throwDecPanel.SetActive(false);
    }

    public void DetativePanel()
    {
        throwPanel.gameObject.SetActive(false);
    }
}
