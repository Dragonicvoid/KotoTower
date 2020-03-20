using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnTrapTesting : MonoBehaviour
{
    Camera mainCamera = null;
    float timer = 0f;
    float maxTimer = 0.5f;
    bool isTouched = false;
    // variable for spawning trap according the button that player selected
    [SerializeField] GameObject trapBomb = null;
    [SerializeField] GameObject trapTime = null;
    [SerializeField] GameObject trapFreeze = null;
    [SerializeField] ButtonForTrapTesting trapButton = null;

    // Find camera with tag
    private void Awake()
    {
        mainCamera = Camera.main;
        timer = 0f;
        maxTimer = 0.5f;
        isTouched = false;
    }

    // Update is called once per frame after normal update
    void Update()
    {
        // check if there is touches, button is selected, and there is no button in front of the touches
        if (Input.touchCount > 0 && GameManager.isSelectTrap)
        {
            Touch touch = Input.GetTouch(0);

            // Spawn the trap at the position that player touch at camera and disable the toggle
            if (touch.phase == TouchPhase.Began
                && (touch.position.y > (Screen.height * 20 / 100) || touch.position.x > (Screen.width * 32 / 100))
                && (touch.position.y > (Screen.height * 20 / 100) || touch.position.x < (Screen.width * 68 / 100)))
            {
                Vector2 touchPosition;
                touchPosition = mainCamera.ScreenToWorldPoint(touch.position);

                // checking if the place is open field
                int x, y;
                GridTesting.getXYFromPosition(touchPosition, out x, out y);

                x += GridTesting.offsetX;
                y += GridTesting.offsetY;

                if (GridTesting.cells[x, y].cellContent == CellContent.PATH)
                {
                    GameObject spawnedTrap;
                    switch (GameManager.selectedTrap)
                    {
                        case 0:
                            spawnedTrap = Instantiate(trapBomb, this.transform);
                            GameManager.pay(TrapType.BOMB_TRAP);
                            break;
                        case 1:
                            spawnedTrap = Instantiate(trapTime, this.transform);
                            GameManager.pay(TrapType.TIME_TRAP);
                            break;
                        case 2:
                            spawnedTrap = Instantiate(trapFreeze, this.transform);
                            GameManager.pay(TrapType.FREEZE_TRAP);
                            break;
                        default:
                            spawnedTrap = Instantiate(trapBomb, this.transform);
                            GameManager.pay(TrapType.BOMB_TRAP);
                            break;
                    }
                    spawnedTrap.transform.position = new Vector3(touchPosition.x, touchPosition.y, 0f); ;
                    isTouched = false;
                    timer = 0f;
                    trapButton.disableButton(GameManager.selectedTrap);
                    return;
                }
            }

            // check if its a double click or not, if it is break from this
            if (!isTouched)
                isTouched = true;
            else
            {
                isTouched = false;
                timer = 0f;
                trapButton.disableButton(GameManager.selectedTrap);
                return;
            }
        }

        // Detecting mouse click for debug
        // check if there is touches, button is selected, and there is no button in front of the touches
        if (Input.GetMouseButtonDown(0) && GameManager.isSelectTrap)
        {
            Vector3 touchLocation = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // Spawn the trap at the position that player touch at camera and disable the toggle
            if (!GameManager.isPressedButtonTrap)
            {
                // checking if the place is open field
                int x, y;
                GridTesting.getXYFromPosition(touchLocation, out x, out y);

                x += GridTesting.offsetX;
                y += GridTesting.offsetY;

                if (GridTesting.cells[x, y].cellContent == CellContent.PATH
                    && (Input.mousePosition.y > (Screen.height * 20 / 100) || Input.mousePosition.x > (Screen.width * 32 / 100))
                    && (Input.mousePosition.y > (Screen.height * 20 / 100) || Input.mousePosition.x < (Screen.width * 68 / 100)))
                {
                    GameObject spawnedTrap;
                    switch (GameManager.selectedTrap)
                    {
                        case 0:
                            spawnedTrap = Instantiate(trapBomb, this.transform);
                            GameManager.pay(TrapType.BOMB_TRAP);
                            break;
                        case 1:
                            spawnedTrap = Instantiate(trapTime, this.transform);
                            GameManager.pay(TrapType.TIME_TRAP);
                            break;
                        case 2:
                            spawnedTrap = Instantiate(trapFreeze, this.transform);
                            GameManager.pay(TrapType.FREEZE_TRAP);
                            break;
                        default:
                            spawnedTrap = Instantiate(trapBomb, this.transform);
                            GameManager.pay(TrapType.BOMB_TRAP);
                            break;
                    }
                    spawnedTrap.transform.position = new Vector3(touchLocation.x, touchLocation.y, 0f);
                    isTouched = false;
                    timer = 0f;
                    trapButton.disableButton(GameManager.selectedTrap);
                    return;
                }
            }

            // check if its a double click or not, if it is break from this
            if (!isTouched)
                isTouched = true;
            else
            {
                isTouched = false;
                timer = 0f;
                trapButton.disableButton(GameManager.selectedTrap);
                return;
            }
        }

        if (isTouched)
            timer += Time.deltaTime;

        if (timer > maxTimer)
        {
            timer = 0f;
            isTouched = false;
        }
    }
}
