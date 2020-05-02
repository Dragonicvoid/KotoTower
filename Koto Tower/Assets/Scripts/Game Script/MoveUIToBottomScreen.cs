using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUIToBottomScreen : MonoBehaviour
{
    [SerializeField] GameObject swipeUp = null;
    float rectHeight;
    float currentValue;
    Vector3 startTouch;
    bool isOnBottom;
    bool isGoingDown;

    // get this rect transform component
    RectTransform rect;

    private void Start()
    {
        rect = this.gameObject.GetComponent<RectTransform>();
        isOnBottom = false;
        isGoingDown = false;
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
        if (Input.touchCount > 0 && !GameManager.instance.isSelectTower && !GameManager.instance.isSelectTrap && GameManager.instance.currentStatus != GameStatus.SELECTING_TOWER && !GameManager.instance.isPaused && !GameManager.instance.isPractice)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.y <= Screen.height / 10)
                    isOnBottom = true;
                else
                    isOnBottom = false;

                startTouch = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && (isOnBottom || !isGoingDown))
            {
                float deltaY = touch.position.y - startTouch.y;
                currentValue = Mathf.Clamp(currentValue + deltaY * 0.5f, rectHeight * -1.1f, 0f);
            }
            else if (touch.phase == TouchPhase.Ended)
                isOnBottom = false;
        }

        // For debugging (PC), by pressing left control and scroll wheel you can move the UI
        if (Input.mouseScrollDelta.y != 0 && Input.GetKey(KeyCode.LeftControl) && !GameManager.instance.isSelectTower && !GameManager.instance.isSelectTrap && GameManager.instance.currentStatus != GameStatus.SELECTING_TOWER && !GameManager.instance.isPaused && !GameManager.instance.isPractice)
            currentValue = Mathf.Clamp(currentValue + Input.mouseScrollDelta.y * 6f, rectHeight * -1.1f, 0f);

        if (!Input.GetKey(KeyCode.LeftControl) && !GameManager.instance.isSelectTower && !GameManager.instance.isSelectTrap && GameManager.instance.currentStatus != GameStatus.SELECTING_TOWER && !GameManager.instance.isPaused && !GameManager.instance.isPractice)
        {
            if (currentValue >= -0.5f * rectHeight)
                currentValue = Mathf.Clamp(currentValue + (rectHeight / 10), rectHeight * -1.1f, 0f);
            else
                currentValue = Mathf.Clamp(currentValue - (rectHeight / 10), rectHeight * -1.1f, 0f);
        }

        // if now tower is selected, make the UI goes down, the 0.1 value is to prevent error
        if (GameManager.instance.isSelectTower || GameManager.instance.isSelectTrap || GameManager.instance.currentStatus == GameStatus.SELECTING_TOWER || GameManager.instance.isPaused || GameManager.instance.isPractice)
            currentValue = Mathf.Clamp(currentValue - (rectHeight / 10), rectHeight * -1.1f, 0f);

        isGoingDown = !(currentValue >= -0.5f * rectHeight);

        rect.anchoredPosition = new Vector3(rect.anchoredPosition.x,
                                                currentValue,
                                                0f);

        if (currentValue - (rectHeight / 10) <= rectHeight * -1f && !GameManager.instance.isSelectTower && !GameManager.instance.isSelectTrap && GameManager.instance.currentStatus != GameStatus.SELECTING_TOWER && !GameManager.instance.isPaused && !GameManager.instance.isPractice)
            swipeUp.SetActive(true);
        else
            swipeUp.SetActive(false);
    }

    // make the ui show when the button pressed
    public void showUi()
    {
        currentValue = rectHeight / 10;
    }
}
