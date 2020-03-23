using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUIToBottomScreenTesting : MonoBehaviour
{
    // how much the screen has to move before it counts as activate
    [SerializeField] float screenDivision = 10f;
    float screenThreshold;
    float currentValue;
    Vector3 startTouch;

    // get this rect transform component
    RectTransform rect;

    private void Start()
    {
        rect = this.gameObject.GetComponent<RectTransform>();
        screenThreshold = 0.5f * screenDivision;
        currentValue = (Screen.width / screenDivision) * -1;
    }

    private void Update()
    {
        // if there is vertical input the make the UI go to that direction
        if (Input.touchCount > 0 && !GameManager.isSelectTower && !GameManager.isSelectTrap)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                startTouch = touch.position;
            else if(touch.phase == TouchPhase.Moved)
            {
                float deltaY = touch.position.y - startTouch.y;
                currentValue = Mathf.Clamp(currentValue + deltaY * 0.5f, (Screen.width / screenDivision) * -1, 0f);
            }
        }

        // For debugging (PC), by pressing left control and scroll wheel you can move the UI
        if (Input.mouseScrollDelta.y != 0 && Input.GetKey(KeyCode.LeftControl) && !GameManager.isSelectTower && !GameManager.isSelectTrap)
            currentValue = Mathf.Clamp(currentValue + Input.mouseScrollDelta.y * 6f, (Screen.width / screenDivision) * -1, 0f);
        
        if (!Input.GetKey(KeyCode.LeftControl) && !GameManager.isSelectTower && !GameManager.isSelectTrap)
        {
            if (currentValue >= -0.5f * (Screen.width / screenDivision))
                currentValue = Mathf.Clamp(currentValue + (Screen.width / (screenDivision * 10)), (Screen.width / screenDivision) * -1, 0f);
            else
                currentValue = Mathf.Clamp(currentValue - (Screen.width / (screenDivision * 10)), (Screen.width / screenDivision) * -1, 0f);
        }

        // if now tower is selected, make the UI goes down
        if (GameManager.isSelectTower || GameManager.isSelectTrap)
            currentValue = Mathf.Clamp(currentValue - (Screen.width / (screenDivision * 10)), (Screen.width / screenDivision) * -1, 0f);

        rect.anchoredPosition = new Vector3(rect.anchoredPosition.x,
                                                currentValue,
                                                0f);
    }
}
