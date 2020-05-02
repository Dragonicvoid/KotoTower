using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeUpPressed : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] MoveUIToBottomScreen towerButton;
    [SerializeField] MoveUIToBottomScreen trapButton;
    // pointer up
    public void OnPointerDown(PointerEventData eventData)
    {
        if (towerButton != null)
            towerButton.showUi();

        if (trapButton != null)
            trapButton.showUi();
    }
}
