using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUIToTopScreen : MonoBehaviour
{
    float rectHeight;
    float currentValue;
    Vector3 startTouch;
    bool isJustText;

    // get this rect transform component
    RectTransform rect;

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
            
        currentValue = 0;
    }

    private void Update()
    {
        moveUIToTopBehave();
    }

    // for tidiness
    void moveUIToTopBehave()
    {
        if (rect.rect.height != rectHeight && !isJustText)
            rectHeight = rect.rect.height;

        // if now tower or trap is selected, or there is a selected tower make the UI goes up
        if (GameManager.instance.isSelectTower || GameManager.instance.isSelectTrap || GameManager.instance.currentStatus == GameStatus.SELECTING_TOWER)
            currentValue = Mathf.Clamp(currentValue + (rectHeight / 10), 0f, rectHeight);
        else
            currentValue = 0;

        rect.anchoredPosition = new Vector3(rect.anchoredPosition.x,
                                                currentValue,
                                                0f);
    }
}
