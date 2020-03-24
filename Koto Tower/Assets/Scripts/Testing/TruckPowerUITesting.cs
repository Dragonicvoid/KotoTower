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

    // initialization
    private void Start()
    {
        GameEventsTesting.current.onTruckDestroyedEnter += OnTruckDestroyed;
        GameEventsTesting.current.onTruckSentEnter += OnTruckSent;

        buttons = this.gameObject.GetComponentsInChildren<Button>();
        rect = this.gameObject.GetComponent<RectTransform>();
        moveUiComp = this.gameObject.GetComponent<MoveUIComponentTesting>();
    }

    // show the UI
    void OnTruckDestroyed(bool isExplode)
    {
        foreach (Button button in buttons)
            button.interactable = false;

        rect.anchoredPosition = new Vector2(0, Screen.width / (moveUiComp.getDivY()));
    }

    // deactivate the UI
    void OnTruckSent()
    {
        foreach (Button button in buttons)
            button.interactable = true;

        rect.anchoredPosition = new Vector2(0, Screen.width / (moveUiComp.getDivY() * -1));
    }

    //delete the event on destroy
    private void OnDestroy()
    {
        GameEventsTesting.current.onTruckDestroyedEnter -= OnTruckDestroyed;
        GameEventsTesting.current.onTruckSentEnter -= OnTruckSent;
    }
}
