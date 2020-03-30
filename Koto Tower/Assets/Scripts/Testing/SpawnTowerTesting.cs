using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnTowerTesting : MonoBehaviour
{
    Camera mainCamera = null;
    bool isReadyToBuild = false;
    bool isReadyToSpawn = false;
    bool canSpawn = false;
    GameObject currentTower;
    TowerType currentTowerType;
    int currX, currY;

    // variable for spawning tower according the button that player selected
    [SerializeField] GameObject towerMachineGun = null;
    [SerializeField] GameObject towerSniper= null;
    [SerializeField] GameObject towerElectric = null;
    [SerializeField] ButtonForTowerTesting towerButton = null;

    // Find camera with tag
    private void Awake()
    {
        mainCamera = Camera.main;
        isReadyToBuild = false;
        isReadyToSpawn = false;
        currentTower = null;
        currX = -1;
        currY = -1;
    }

    // Update is called once per frame after normal update
    void Update()
    {
        detectTouches();
        detectMouse();
    }

    // for touches
    void detectTouches()
    {
        // for touch
        if (Input.touchCount > 0 && GameManager.isSelectTower && !isReadyToSpawn)
        {
            // If it is the first time to build then make the tower
            if (!isReadyToBuild)
            {
                switch (GameManager.selectedTower)
                {
                    case 0:
                        currentTower = Instantiate(towerMachineGun, this.transform);
                        currentTowerType = TowerType.MACHINE_GUN;
                        break;
                    case 1:
                        currentTower = Instantiate(towerSniper, this.transform);
                        currentTowerType = TowerType.SNIPER;
                        break;
                    case 2:
                        currentTower = Instantiate(towerElectric, this.transform);
                        currentTowerType = TowerType.ELECTRIC;
                        break;
                    default:
                        currentTower = Instantiate(towerMachineGun, this.transform);
                        currentTowerType = TowerType.MACHINE_GUN;
                        break;
                }

                currentTower.SetActive(false);
                SpriteRenderer renderer = currentTower.GetComponent<SpriteRenderer>();
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

                if (GridTesting.cells[x, y].cellContent == CellContent.OPEN_FIELD)
                {
                    currentTower.SetActive(true);
                    isReadyToSpawn = true;
                    canSpawn = false;
                    currentTower.transform.position = GridTesting.getWorldSpace(x, y) + new Vector3(0.5f, 0.5f, 0);
                    currX = x;
                    currY = y;
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
            // if the player touch the area again then spawn the tower
            if (touch.phase == TouchPhase.Ended && GameManager.isSelectTower)
            {
                if (isReadyToSpawn && isReadyToBuild)
                    canSpawn = true;
                else if (canSpawn)
                {
                    // change the color
                    SpriteRenderer renderer = currentTower.GetComponent<SpriteRenderer>();
                    renderer.color = new Color(1, 1, 1, 1);

                    // activate it
                    TowerBehaviourTesting tower = currentTower.GetComponent<TowerBehaviourTesting>();
                    tower.activate();

                    // block the area
                    TowerGridBlocker gridBlocker = currentTower.GetComponent<TowerGridBlocker>();
                    gridBlocker.changeGridStatus();

                    // pay the tower
                    GameManager.pay(currentTowerType);
                    towerButton.disableButton(GameManager.selectedTower);

                    // reset variable
                    isReadyToBuild = false;
                    isReadyToSpawn = false;
                    canSpawn = false;
                    currX = -1;
                    currY = -1;
                    GameEventsTesting.current.TowerOrTrapBuild();
                    return;
                }
            }
            else if (touch.phase == TouchPhase.Began && !(currX == x && currY == y))
            {
                isReadyToSpawn = false;
                canSpawn = false;
            }

        }
    }

    // for mouse (debug)
    void detectMouse()
    {
        if (Input.GetMouseButton(0) && GameManager.isSelectTower)
        {
            // If it is the first time to build then make the tower
            if (!isReadyToBuild)
            {
                switch (GameManager.selectedTower)
                {
                    case 0:
                        currentTower = Instantiate(towerMachineGun, this.transform);
                        currentTowerType = TowerType.MACHINE_GUN;
                        break;
                    case 1:
                        currentTower = Instantiate(towerSniper, this.transform);
                        currentTowerType = TowerType.SNIPER;
                        break;
                    case 2:
                        currentTower = Instantiate(towerElectric, this.transform);
                        currentTowerType = TowerType.ELECTRIC;
                        break;
                    default:
                        currentTower = Instantiate(towerMachineGun, this.transform);
                        currentTowerType = TowerType.MACHINE_GUN;
                        break;
                }

                currentTower.SetActive(false);
                SpriteRenderer renderer = currentTower.GetComponent<SpriteRenderer>();
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

            if (currX != x || currY != y)
                isReadyToSpawn = false;

            if (GridTesting.cells[x, y].cellContent == CellContent.OPEN_FIELD && !isReadyToSpawn)
            {
                currentTower.SetActive(true);
                currentTower.transform.position = GridTesting.getWorldSpace(x, y) + new Vector3(0.5f, 0.5f, 0);
                currX = x;
                currY = y;
            }
        }

        // if the player touch the area again then spawn the tower, if its the first time then just be ready to spawn
        if (Input.GetMouseButtonUp(0) && GameManager.isSelectTower)
        {
            if (!isReadyToSpawn && isReadyToBuild)
                isReadyToSpawn = true;
            else if (isReadyToBuild)
            {
                // change the color
                SpriteRenderer renderer = currentTower.GetComponent<SpriteRenderer>();
                renderer.color = new Color(1, 1, 1, 1);

                // activate it
                TowerBehaviourTesting tower = currentTower.GetComponent<TowerBehaviourTesting>();
                tower.activate();

                // block the area
                TowerGridBlocker gridBlocker = currentTower.GetComponent<TowerGridBlocker>();
                gridBlocker.changeGridStatus();

                // pay the tower
                GameManager.pay(currentTowerType);
                towerButton.disableButton(GameManager.selectedTower);

                // reset variable
                isReadyToBuild = false;
                isReadyToSpawn = false;
                currX = -1;
                currY = -1;
                GameEventsTesting.current.TowerOrTrapBuild();
                return;
            }
        }
    }

    public void cancel()
    {
        isReadyToBuild = false;
        isReadyToSpawn = false;
        currX = -1;
        currY = -1;

        if(currentTower != null)
            Destroy(currentTower);

        currentTower = null;

        if(GameManager.isSelectTower)
            towerButton.disableButton(GameManager.selectedTower);
    }

}
