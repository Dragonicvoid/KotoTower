using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTruckButtonOnWait : MonoBehaviour
{
    TruckBehaviour truck;
    float rectHeight;
    float currentValue;
    float anchoredPos;
    Vector3 startTouch;
    bool isJustText;

    // get this rect transform component
    RectTransform rect;

    //get the object
    private void Awake()
    {
        truck = GameObject.FindGameObjectWithTag("Truck").GetComponent<TruckBehaviour>();
    }

    // initialization
    private void Start()
    {
        rect = this.gameObject.GetComponent<RectTransform>();
        rectHeight = rect.rect.height;

        if (rectHeight == 0)
        {
            rectHeight = Screen.height / 10;
            isJustText = true;
        }
        else
            isJustText = false;

        anchoredPos = rect.anchoredPosition.y;
        currentValue = anchoredPos * -1;
    }

    // Update is called once per frame
    void Update()
    {
        behaveOnTruckStatus();
    }

    // move UI behave
    void behaveOnTruckStatus()
    {
        if (rect.rect.height != rectHeight && !isJustText)
            rectHeight = rect.rect.height;

        // if now tower or trap is selected, or there is a selected tower make the UI goes up
        if (GameManager.instance.isSelectTower || GameManager.instance.isSelectTrap || GameManager.instance.currentStatus == GameStatus.SELECTING_TOWER || GameManager.instance.isPaused || truck.getTruckStatus() != TruckStatus.WAITING)
            currentValue = Mathf.Clamp(currentValue + (rectHeight / 10), 0f, rectHeight);
        else
            currentValue = anchoredPos;

        rect.anchoredPosition = new Vector3(rect.anchoredPosition.x,
                                                currentValue,
                                                0f);
    }
}
