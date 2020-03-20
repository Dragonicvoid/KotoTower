using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnTowerTesting : MonoBehaviour
{
    Camera mainCamera = null;
    float timer = 0f;
    float maxTimer = 0.5f;
    bool isTouched = false;
    // variable for spawning tower according the button that player selected
    [SerializeField] GameObject towerMachineGun = null;
    [SerializeField] GameObject towerSniper= null;
    [SerializeField] GameObject towerElectric = null;
    [SerializeField] ButtonForTowerTesting towerButton = null;

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
        if (Input.touchCount > 0 && GameManager.isSelectTower)
        {
            Touch touch = Input.GetTouch(0);

            // Spawn the tower at the position that player touch at camera and disable the toggle
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

                if (GridTesting.cells[x,y].cellContent == CellContent.OPEN_FIELD)
                {
                    GameObject spawnedTower;
                    switch (GameManager.selectedTower)
                    {
                        case 0:
                            spawnedTower = Instantiate(towerMachineGun, this.transform);
                            GameManager.pay(TowerType.MACHINE_GUN);
                            break;
                        case 1:
                            spawnedTower = Instantiate(towerSniper, this.transform);
                            GameManager.pay(TowerType.SNIPER);
                            break;
                        case 2:
                            spawnedTower = Instantiate(towerElectric, this.transform);
                            GameManager.pay(TowerType.ELECTRIC);
                            break;
                        default:
                            spawnedTower = Instantiate(towerMachineGun, this.transform);
                            GameManager.pay(TowerType.MACHINE_GUN);
                            break;
                    }
                    spawnedTower.transform.position = GridTesting.getWorldSpace(x, y) + new Vector3(0.5f, 0.5f, 0);
                    spawnedTower.GetComponent<TowerGridBlocker>().changeGridStatus();
                    isTouched = false;
                    timer = 0f;
                    towerButton.disableButton(GameManager.selectedTower);
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
                towerButton.disableButton(GameManager.selectedTower);
                return;
            }
        }

        // Detecting mouse click for debug
        // check if there is touches, button is selected, and there is no button in front of the touches
        if (Input.GetMouseButtonDown(0) && GameManager.isSelectTower)
        {
            Vector3 touchLocation = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // Spawn the tower at the position that player touch at camera and disable the toggle
            if (!GameManager.isPressedButtonTower)
            {
                // checking if the place is open field
                int x, y;
                GridTesting.getXYFromPosition(touchLocation, out x, out y);

                x += GridTesting.offsetX;
                y += GridTesting.offsetY;

                if (GridTesting.cells[x, y].cellContent == CellContent.OPEN_FIELD 
                    && (Input.mousePosition.y > (Screen.height * 20 / 100) || Input.mousePosition.x > (Screen.width * 32 / 100))
                    && (Input.mousePosition.y > (Screen.height * 20 / 100) || Input.mousePosition.x < (Screen.width * 68 / 100)))
                {
                    GameObject spawnedTower;
                    switch (GameManager.selectedTower)
                    {
                        case 0:
                            spawnedTower = Instantiate(towerMachineGun, this.transform);
                            GameManager.pay(TowerType.MACHINE_GUN);
                            break;
                        case 1:
                            spawnedTower = Instantiate(towerSniper, this.transform);
                            GameManager.pay(TowerType.SNIPER);
                            break;
                        case 2:
                            spawnedTower = Instantiate(towerElectric, this.transform);
                            GameManager.pay(TowerType.ELECTRIC);
                            break;
                        default:
                            spawnedTower = Instantiate(towerMachineGun, this.transform);
                            GameManager.pay(TowerType.MACHINE_GUN);
                            break;
                    }
                    spawnedTower.transform.position = GridTesting.getWorldSpace(x, y) + new Vector3(0.5f, 0.5f, 0);
                    spawnedTower.GetComponent<TowerGridBlocker>().changeGridStatus();
                    isTouched = false;
                    timer = 0f;
                    towerButton.disableButton(GameManager.selectedTower);
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
                towerButton.disableButton(GameManager.selectedTower);
                return;
            }
        }

        if (isTouched)
            timer += Time.deltaTime;

        if(timer > maxTimer)
        {
            timer = 0f;
            isTouched = false;
        }
    }
}
