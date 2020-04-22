using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnTrap : MonoBehaviour
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
    [SerializeField] ButtonForTrap trapButton = null;
    [SerializeField] GameObject desc = null;

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
        if (GameManager.instance.isPaused)
            cancel();

        // for touch
        if (Input.touchCount > 0 && GameManager.instance.isSelectTrap && !isReadyToSpawn && !OtherMethod.onUiPressed(Input.GetTouch(0).position))
        {
            Touch touch = Input.GetTouch(0);

            // If it is the first time to build then make the trap
            if (!isReadyToBuild && !OtherMethod.onUiPressed(touch.position))
            {
                switch (GameManager.instance.selectedTrap)
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

            if (touch.phase == TouchPhase.Began && !OtherMethod.onUiPressed(touch.position))
            {
                Vector2 touchPosition;
                touchPosition = mainCamera.ScreenToWorldPoint(touch.position);

                // checking if the place is open field
                int x, y;
                GridCustom.getXYFromPosition(touchPosition, out x, out y);

                x += GridCustom.offsetX;
                y += GridCustom.offsetY;
               
                if (GridCustom.cells[x, y].cellContent == CellContent.PATH)
                {
                    currentTrap.SetActive(true);
                    isReadyToSpawn = true;
                    canSpawn = false;
                    currentTrap.transform.position = new Vector3(touch.position.x, touch.position.y, 0);
                }
            }
        }

        if (Input.touchCount > 0 && isReadyToBuild && isReadyToSpawn && !OtherMethod.onUiPressed(Input.GetTouch(0).position))
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition;
            touchPosition = mainCamera.ScreenToWorldPoint(touch.position);

            int x, y;
            GridCustom.getXYFromPosition(touchPosition, out x, out y);

            x += GridCustom.offsetX;
            y += GridCustom.offsetY;
            // if the player touch the area again then spawn the trap
            if (touch.phase == TouchPhase.Ended && GameManager.instance.isSelectTrap)
            {
                if (isReadyToSpawn && isReadyToBuild)
                    canSpawn = true;
                else if (canSpawn)
                {
                    // change the color
                    SpriteRenderer renderer = currentTrap.GetComponent<SpriteRenderer>();
                    renderer.color = new Color(1, 1, 1, 1);

                    // activate it
                    TrapsBehaviour trap = currentTrap.GetComponent<TrapsBehaviour>();
                    trap.activate();

                    // pay the trap
                    GameManager.instance.pay(currentTrapType);
                    trapButton.disableButton(GameManager.instance.selectedTrap);

                    // reset variable
                    currentTrap = null;
                    currentTrapType = TrapType.BOMB_TRAP;
                    isReadyToBuild = false;
                    isReadyToSpawn = false;
                    canSpawn = false;
                    desc.gameObject.SetActive(false);
                    GameEvents.current.TowerOrTrapBuild();
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
        if (Input.GetMouseButton(0) && GameManager.instance.isSelectTrap && !OtherMethod.onUiPressed(Input.mousePosition))
        {
            // If it is the first time to build then make the trap
            if (!isReadyToBuild)
            {
                switch (GameManager.instance.selectedTrap)
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
            GridCustom.getXYFromPosition(touchPosition, out x, out y);

            x += GridCustom.offsetX;
            y += GridCustom.offsetY;

            if (Vector3.Distance(currentTrap.transform.position, touchPosition) > 0.5f)
                isReadyToSpawn = false;

            if (GridCustom.cells[x, y].cellContent == CellContent.PATH && !isReadyToSpawn)
            {
                currentTrap.SetActive(true);
                currentTrap.transform.position = new Vector3(touchPosition.x, touchPosition.y, -0);
            }
        }

        // if the player touch the area again then spawn the trap, if its the first time then just be ready to spawn
        if (Input.GetMouseButtonUp(0) && GameManager.instance.isSelectTrap && !OtherMethod.onUiPressed(Input.mousePosition))
        {
            if (!isReadyToSpawn && isReadyToBuild)
                isReadyToSpawn = true;
            else if (isReadyToBuild)
            {
                // change the color
                SpriteRenderer renderer = currentTrap.GetComponent<SpriteRenderer>();
                renderer.color = new Color(1, 1, 1, 1);

                // activate it
                TrapsBehaviour trap = currentTrap.GetComponent<TrapsBehaviour>();
                trap.activate();

                // pay the trap
                GameManager.instance.pay(currentTrapType);
                trapButton.disableButton(GameManager.instance.selectedTrap);

                // reset variable
                currentTrap = null;
                currentTrapType = TrapType.BOMB_TRAP;
                isReadyToBuild = false;
                isReadyToSpawn = false;
                desc.gameObject.SetActive(false);
                GameEvents.current.TowerOrTrapBuild();
                return;
            }
        }
    }

    // cancel the building tower
    public void cancel()
    {
        isReadyToBuild = false;
        isReadyToSpawn = false;

        if(currentTrap != null)
            Destroy(currentTrap);

        currentTrap = null;
        desc.gameObject.SetActive(false);

        if(GameManager.instance.isSelectTrap)
            trapButton.disableButton(GameManager.instance.selectedTrap);
    }
}
