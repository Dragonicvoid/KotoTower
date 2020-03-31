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

        this.gameObject.SetActive(false);
    }

    // show the UI
    public void readyToSpawn()
    {
        foreach (Button button in buttons)
            button.interactable = true;

        this.gameObject.SetActive(true);
    }

    // deactivate the UI
    public void cancel()
    {
        foreach (Button button in buttons)
            button.interactable = false;

        this.gameObject.SetActive(false);
    }

    // delete the event
    private void OnDestroy()
    {
        GameEventsTesting.current.onTowerOrTrapBuild -= cancel;
    }
}
