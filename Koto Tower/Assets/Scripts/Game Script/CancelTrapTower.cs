using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelTrapTower: MonoBehaviour
{
    // child parent
    Button[] buttons;
    RectTransform rect;
    MoveUIComponent moveUiComp;

    // koto tower and generator
    ClickOnGenerator generator;
    ClickOnKotoTower kotoTower;

    // initialization
    private void Start()
    {
        GameEvents.current.onTowerOrTrapBuild += cancel;

        buttons = this.gameObject.GetComponentsInChildren<Button>();
        rect = this.gameObject.GetComponent<RectTransform>();
        moveUiComp = this.gameObject.GetComponent<MoveUIComponent>();
        kotoTower = GameObject.FindGameObjectWithTag("Koto Tower").GetComponent<ClickOnKotoTower>();
        generator = GameObject.FindGameObjectWithTag("Generator").GetComponent<ClickOnGenerator>();

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

        if(GameManager.instance.gameStart)
        {
            //open generator and koto tower question answer
            generator.StartCoroutine(generator.openGenerator());
            kotoTower.StartCoroutine(kotoTower.openKotoTower());
        }
    }

    // delete the event
    private void OnDestroy()
    {
        GameEvents.current.onTowerOrTrapBuild -= cancel;
    }
}
