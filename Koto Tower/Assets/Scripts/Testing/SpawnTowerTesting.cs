using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnTowerTesting : MonoBehaviour
{
    Camera mainCamera = null;
    // variable for spawning tower according the button that player selected
    [SerializeField] GameObject tower = null;
    [SerializeField] ButtonForTowerTesting towerButton = null;

    // Find camera with tag
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame after normal update
    void Update()
    {
        // check if there is touches, button is selected, and there is no button in front of the touches
        if (Input.touchCount > 0 && ButtonForTowerTesting.isSelectTower)
        {
            Touch touch = Input.GetTouch(0);

            // Spawn the tower at the position that player touch at camera and disable the toggle
            if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
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
                    Debug.Log("Spawn at : " + GridTesting.getWorldSpace(x, y));
                    GameObject spawnedTower = Instantiate(tower, this.transform);
                    spawnedTower.transform.position = GridTesting.getWorldSpace(x, y) + new Vector3(0.5f, 0.5f, 0);
                    spawnedTower.GetComponent<TowerGridBlocker>().changeGridStatus();
                }
                towerButton.disableToogle(0);
            }
        }

        // Detecting mouse click for debug
        // check if there is touches, button is selected, and there is no button in front of the touches
        if (Input.GetMouseButtonDown(0) && ButtonForTowerTesting.isSelectTower)
        {
            Vector3 touchLocation = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // Spawn the tower at the position that player touch at camera and disable the toggle
            if (!ButtonForTowerTesting.isPressedButton)
            {
                // checking if the place is open field
                int x, y;
                GridTesting.getXYFromPosition(touchLocation, out x, out y);

                x += GridTesting.offsetX;
                y += GridTesting.offsetY;

                if (GridTesting.cells[x, y].cellContent == CellContent.OPEN_FIELD)
                {
                    GameObject spawnedTower = Instantiate(tower, this.transform);
                    spawnedTower.transform.position = GridTesting.getWorldSpace(x, y) + new Vector3(0.5f, 0.5f, 0);
                    spawnedTower.GetComponent<TowerGridBlocker>().changeGridStatus();
                }
                towerButton.disableToogle(0);
            }
        }
    }
}
