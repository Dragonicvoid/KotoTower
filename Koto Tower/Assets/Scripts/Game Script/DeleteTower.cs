using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteTower : MonoBehaviour
{
    [SerializeField] DescriptionForInspection desc = null;
    [SerializeField] GameObject moneyAdd = null;

    TowerBehaviour selectedTower;
    Button[] buttons;
    RectTransform rect;
    MoveUIComponent moveUiComp;

    //initialization
    private void Start()
    {
        GameEvents.current.onTowerSelected += OnTowerSelected;
        GameEvents.current.onTowerUnselected += OnTowerUnselected;

        buttons = this.gameObject.GetComponentsInChildren<Button>();
        rect = this.gameObject.GetComponent<RectTransform>();
        moveUiComp = this.gameObject.GetComponent<MoveUIComponent>();
        this.gameObject.SetActive(false);
    }

    // select Tower, also enable descriptor
    void OnTowerSelected(TowerBehaviour obj)
    {
        selectedTower = obj;
        desc.changeDescriptor(selectedTower.getTowerType());
        desc.gameObject.SetActive(true);
        foreach (Button button in buttons)
            button.interactable = true;

        this.gameObject.SetActive(true);
    }

    // unselect Tower, also disable descriptor
    void OnTowerUnselected()
    {
        desc.gameObject.SetActive(false);

        foreach (Button button in buttons)
            button.interactable = false;

        this.gameObject.SetActive(false);
    }

    //despawn the tower and give back the money
    public void despawn()
    {
        GameObject moneyAddObj = Instantiate(moneyAdd);
        MoneyAddedBehaviour moneyAddBehave = moneyAddObj.GetComponent<MoneyAddedBehaviour>();
        moneyAddBehave.activateRefund((int)(GameManager.instance.getRefundMoney(selectedTower.getTowerType())), selectedTower.transform.position);
        TowerGridBlocker blocker = selectedTower.gameObject.GetComponent<TowerGridBlocker>();
        blocker.removeGridStatus();
        GameManager.instance.refund(selectedTower.getTowerType());
        Destroy(selectedTower.gameObject);
        GameEvents.current.TowerUnselected();
        GameManager.instance.currentStatus = GameStatus.PLAY;
        OnTowerUnselected();
    }

    //cancel the condition
    public void unselect()
    {
        GameEvents.current.TowerUnselected();
        GameManager.instance.currentStatus = GameStatus.PLAY;
        OnTowerUnselected();
    }

    // delete the event
    private void OnDestroy()
    {
        GameEvents.current.onTowerSelected -= OnTowerSelected;
        GameEvents.current.onTowerUnselected -= OnTowerUnselected;
    }
}
