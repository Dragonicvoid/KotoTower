using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckPowerUITesting : MonoBehaviour
{
    // child parent
    Button[] buttons;
    RectTransform rect;
    MoveUIComponentTesting moveUiComp;
    float rectHeight;
    float currentValue;
    bool truckMove;

    // initialization
    private void Start()
    {
        GameEventsTesting.current.onTruckDestroyedEnter += OnTruckDestroyed;
        GameEventsTesting.current.onTruckSentEnter += OnTruckSent;

        truckMove = false;
        buttons = this.gameObject.GetComponentsInChildren<Button>();
        rect = this.gameObject.GetComponent<RectTransform>();
        rectHeight = rect.rect.height + 10;
        currentValue = rectHeight;
        moveUiComp = this.gameObject.GetComponent<MoveUIComponentTesting>();
    }

    // update when selecting tower
    private void Update()
    {
        moveUIToTopBehave();
    }

    // for tidiness
    void moveUIToTopBehave()
    {
        if (rect.rect.height != rectHeight)
            rectHeight = rect.rect.height + 10;

        // if now tower is selected, make the UI goes up
        if ((GameManager.isSelectTower || GameManager.isSelectTrap || GameManager.currentStatus == GameStatus.SELECTING_TOWER) && truckMove)
            currentValue = Mathf.Clamp(currentValue + (rectHeight / 10), -rectHeight, rectHeight + 10);
        else if (truckMove)
            currentValue = Mathf.Clamp(currentValue - (rectHeight / 10), -rectHeight, rectHeight + 10);

        rect.anchoredPosition = new Vector3(rect.anchoredPosition.x,
                                                currentValue,
                                                0f);
    }

    // show the UI
    void OnTruckDestroyed(bool isExplode)
    {
        foreach (Button button in buttons)
            button.interactable = false;

        truckMove = false;
        currentValue = rectHeight + 10;
        rect.anchoredPosition = new Vector2(0, rectHeight + 10);
    }

    // deactivate the UI
    void OnTruckSent()
    {
        foreach (Button button in buttons)
            button.interactable = true;

        truckMove = true;
    }

    //delete the event on destroy
    private void OnDestroy()
    {
        GameEventsTesting.current.onTruckDestroyedEnter -= OnTruckDestroyed;
        GameEventsTesting.current.onTruckSentEnter -= OnTruckSent;
    }
}
