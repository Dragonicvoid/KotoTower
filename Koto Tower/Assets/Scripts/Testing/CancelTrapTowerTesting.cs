using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelTrapTowerTesting : MonoBehaviour
{
    // child parent
    Button[] buttons;
    RectTransform rect;
    MoveUIComponentTesting moveUiComp;

    // initialization
    private void Start()
    {
        GameEventsTesting.current.onTowerOrTrapBuild += cancel;

        buttons = this.gameObject.GetComponentsInChildren<Button>();
        rect = this.gameObject.GetComponent<RectTransform>();
        moveUiComp = this.gameObject.GetComponent<MoveUIComponentTesting>();
    }

    // show the UI
    public void readyToSpawn()
    {
        foreach (Button button in buttons)
            button.interactable = true;

        rect.anchoredPosition = new Vector2(0, (Screen.width / moveUiComp.getDivY()) * -1.05f);
    }

    // deactivate the UI
    public void cancel()
    {
        foreach (Button button in buttons)
            button.interactable = false;

        rect.anchoredPosition = new Vector2(0, Screen.width / (moveUiComp.getDivY()));
    }

    // delete the event
    private void OnDestroy()
    {
        GameEventsTesting.current.onTowerOrTrapBuild -= cancel;
    }
}
