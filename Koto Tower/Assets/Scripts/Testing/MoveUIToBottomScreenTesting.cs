using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUIToBottomScreenTesting : MonoBehaviour
{
    float rectHeight;
    float currentValue;
    Vector3 startTouch;

    // get this rect transform component
    RectTransform rect;

    private void Start()
    {
        rect = this.gameObject.GetComponent<RectTransform>();
        rectHeight = rect.rect.height;
        currentValue = rectHeight * -1;
    }

    private void Update()
    {
        moveUIToBottomBehave();
    }

    // for tidiness
    void moveUIToBottomBehave()
    {
        if (rect.rect.height != rectHeight)
            rectHeight = rect.rect.height;

        // if there is vertical input the make the UI go to that direction
        if (Input.touchCount > 0 && !GameManager.isSelectTower && !GameManager.isSelectTrap && GameManager.currentStatus == GameStatus.PLAY)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                startTouch = touch.position;
            else if (touch.phase == TouchPhase.Moved)
            {
                float deltaY = touch.position.y - startTouch.y;
                currentValue = Mathf.Clamp(currentValue + deltaY * 0.5f, rectHeight * -1.1f, 0f);
            }
        }

        // For debugging (PC), by pressing left control and scroll wheel you can move the UI
        if (Input.mouseScrollDelta.y != 0 && Input.GetKey(KeyCode.LeftControl) && !GameManager.isSelectTower && !GameManager.isSelectTrap && GameManager.currentStatus == GameStatus.PLAY)
            currentValue = Mathf.Clamp(currentValue + Input.mouseScrollDelta.y * 6f, rectHeight * -1.1f, 0f);

        if (!Input.GetKey(KeyCode.LeftControl) && !GameManager.isSelectTower && !GameManager.isSelectTrap && GameManager.currentStatus == GameStatus.PLAY)
        {
            if (currentValue >= -0.5f * rectHeight)
                currentValue = Mathf.Clamp(currentValue + (rectHeight / 10), rectHeight * -1.1f, 0f);
            else
                currentValue = Mathf.Clamp(currentValue - (rectHeight / 10), rectHeight * -1.1f, 0f);
        }

        // if now tower is selected, make the UI goes down
        if (GameManager.isSelectTower || GameManager.isSelectTrap || GameManager.currentStatus == GameStatus.SELECTING_TOWER)
            currentValue = Mathf.Clamp(currentValue - (rectHeight / 10), rectHeight * -1.1f, 0f);

        rect.anchoredPosition = new Vector3(rect.anchoredPosition.x,
                                                currentValue,
                                                0f);
    }
}
