using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteTowerTesting : MonoBehaviour
{
    TowerBehaviourTesting selectedTower;
    Button[] buttons;
    RectTransform rect;
    MoveUIComponentTesting moveUiComp;

    //initialization
    private void Start()
    {
        GameEventsTesting.current.onTowerSelected += OnTowerSelected;
        GameEventsTesting.current.onTowerUnselected += OnTowerUnselected;

        buttons = this.gameObject.GetComponentsInChildren<Button>();
        rect = this.gameObject.GetComponent<RectTransform>();
        moveUiComp = this.gameObject.GetComponent<MoveUIComponentTesting>();
        this.gameObject.SetActive(false);
    }

    // select Tower 
    void OnTowerSelected(TowerBehaviourTesting obj)
    {
        selectedTower = obj;

        foreach (Button button in buttons)
            button.interactable = true;

        this.gameObject.SetActive(true);
    }

    // unselect Tower
    void OnTowerUnselected()
    {
        foreach (Button button in buttons)
            button.interactable = false;

        this.gameObject.SetActive(false);
    }

    //despawn the tower and give back the money
    public void despawn()
    {
        TowerGridBlocker blocker = selectedTower.gameObject.GetComponent<TowerGridBlocker>();
        blocker.removeGridStatus();
        Debug.Log(selectedTower + " : " + selectedTower.transform.position);
        GameManager.instance.refund(selectedTower.getTowerType());
        Destroy(selectedTower.gameObject);
        GameEventsTesting.current.TowerUnselected();
        GameManager.instance.currentStatus = GameStatus.PLAY;
        OnTowerUnselected();
    }

    //cancel the condition
    public void unselect()
    {
        GameEventsTesting.current.TowerUnselected();
        GameManager.instance.currentStatus = GameStatus.PLAY;
        OnTowerUnselected();
    }

    // delete the event
    private void OnDestroy()
    {
        GameEventsTesting.current.onTowerSelected -= OnTowerSelected;
        GameEventsTesting.current.onTowerUnselected -= OnTowerUnselected;
    }
}
