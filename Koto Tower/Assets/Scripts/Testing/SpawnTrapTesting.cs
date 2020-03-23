using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnTrapTesting : MonoBehaviour
{
    Camera mainCamera = null;
    bool isReadyToBuild = false;
    bool isReadyToSpawn = false;
    bool canSpawn = false;
    GameObject currentTrap;
    TrapType currentTrapType;

    // variable for spawning trap according the button that player selected
    [SerializeField] GameObject trapBomb = null;
    [SerializeField] GameObject trapTime = null;
    [SerializeField] GameObject trapFreeze = null;
    [SerializeField] ButtonForTrapTesting trapButton = null;

    // Find camera with tag
    private void Awake()
    {
        mainCamera = Camera.main;
        isReadyToBuild = false;
        isReadyToSpawn = false;
        currentTrap = null;
    }

    // Update is called once per frame after normal update
    void Update()
    {
        // for touch
        if (Input.touchCount > 0 && GameManager.isSelectTrap && !isReadyToSpawn)
        {
            // If it is the first time to build then make the trap
            if (!isReadyToBuild)
            {
                switch (GameManager.selectedTrap)
                {
                    case 0:
                        currentTrap = Instantiate(trapBomb, this.transform);
                        currentTrapType = TrapType.BOMB_TRAP;
                        break;
                    case 1:
                        currentTrap = Instantiate(trapTime, this.transform);
                        currentTrapType = TrapType.TIME_TRAP;
                        break;
                    case 2:
                        currentTrap = Instantiate(trapFreeze, this.transform);
                        currentTrapType = TrapType.FREEZE_TRAP;
                        break;
                    default:
                        currentTrap = Instantiate(trapBomb, this.transform);
                        currentTrapType = TrapType.BOMB_TRAP;
                        break;
                }

                currentTrap.SetActive(false);
                SpriteRenderer renderer = currentTrap.GetComponent<SpriteRenderer>();
                renderer.color = new Color(1, 1, 1, 150f / 256f);
                isReadyToBuild = true;
            }

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
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
                    currentTrap.SetActive(true);
                    isReadyToSpawn = true;
                    canSpawn = false;
                    currentTrap.transform.position = new Vector3(touch.position.x, touch.position.y, 0);
                }
            }
        }

        if (Input.touchCount > 0 && isReadyToBuild && isReadyToSpawn)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition;
            touchPosition = mainCamera.ScreenToWorldPoint(touch.position);

            int x, y;
            GridTesting.getXYFromPosition(touchPosition, out x, out y);

            x += GridTesting.offsetX;
            y += GridTesting.offsetY;
            // if the player touch the area again then spawn the trap
            if (touch.phase == TouchPhase.Ended && GameManager.isSelectTrap)
            {
                if (isReadyToSpawn && isReadyToBuild)
                    canSpawn = true;
                else if (canSpawn)
                {
                    // change the color
                    SpriteRenderer renderer = currentTrap.GetComponent<SpriteRenderer>();
                    renderer.color = new Color(1, 1, 1, 1);

                    // activate it
                    TrapsBehaviourTesting trap = currentTrap.GetComponent<TrapsBehaviourTesting>();
                    trap.activate();

                    // pay the trap
                    GameManager.pay(currentTrapType);
                    trapButton.disableButton(GameManager.selectedTrap);

                    // reset variable
                    isReadyToBuild = false;
                    isReadyToSpawn = false;
                    canSpawn = false;
                    return;
                }
            }
            else if (touch.phase == TouchPhase.Began && Vector3.Distance(currentTrap.transform.position, new Vector3(touchPosition.x, touchPosition.y, 0f)) > 0.5f)
            {
                isReadyToSpawn = false;
                canSpawn = false;
            }

        }

        // for mouse (debug)
        if (Input.GetMouseButton(0) && GameManager.isSelectTrap)
        {
            // If it is the first time to build then make the trap
            if (!isReadyToBuild)
            {
                switch (GameManager.selectedTrap)
                {
                    case 0:
                        currentTrap = Instantiate(trapBomb, this.transform);
                        currentTrapType = TrapType.BOMB_TRAP;
                        break;
                    case 1:
                        currentTrap = Instantiate(trapTime, this.transform);
                        currentTrapType = TrapType.TIME_TRAP;
                        break;
                    case 2:
                        currentTrap = Instantiate(trapFreeze, this.transform);
                        currentTrapType = TrapType.FREEZE_TRAP;
                        break;
                    default:
                        currentTrap = Instantiate(trapBomb, this.transform);
                        currentTrapType = TrapType.BOMB_TRAP;
                        break;
                }

                currentTrap.SetActive(false);
                SpriteRenderer renderer = currentTrap.GetComponent<SpriteRenderer>();
                renderer.color = new Color(1, 1, 1, 150f / 256f);
                isReadyToBuild = true;
            }

            Vector2 touchPosition;
            touchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // checking if the place is open field
            int x, y;
            GridTesting.getXYFromPosition(touchPosition, out x, out y);

            x += GridTesting.offsetX;
            y += GridTesting.offsetY;

            if (Vector3.Distance(currentTrap.transform.position, touchPosition) > 0.5f)
                isReadyToSpawn = false;

            if (GridTesting.cells[x, y].cellContent == CellContent.PATH && !isReadyToSpawn)
            {
                currentTrap.SetActive(true);
                currentTrap.transform.position = new Vector3(touchPosition.x, touchPosition.y, -0);
            }
        }

        // if the player touch the area again then spawn the trap, if its the first time then just be ready to spawn
        if (Input.GetMouseButtonUp(0) && GameManager.isSelectTrap)
        {
            if (!isReadyToSpawn && isReadyToBuild)
                isReadyToSpawn = true;
            else if (isReadyToBuild)
            {
                // change the color
                SpriteRenderer renderer = currentTrap.GetComponent<SpriteRenderer>();
                renderer.color = new Color(1, 1, 1, 1);

                // activate it
                TrapsBehaviourTesting trap = currentTrap.GetComponent<TrapsBehaviourTesting>();
                trap.activate();

                // pay the trap
                GameManager.pay(currentTrapType);
                trapButton.disableButton(GameManager.selectedTrap);

                // reset variable
                isReadyToBuild = false;
                isReadyToSpawn = false;
                return;
            }
        }
    }
}
