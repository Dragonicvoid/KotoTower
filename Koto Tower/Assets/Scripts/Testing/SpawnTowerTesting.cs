using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnTowerTesting : MonoBehaviour
{
    // Assign camera so it doesnt take a long time to find camera
    // since camera.main is basically findGameObjectWithTag
    [SerializeField] Camera mainCamera;
    // variable for spawning tower according the button that player selected
    [SerializeField] GameObject tower;
    [SerializeField] ButtonForTowerTesting towerButton;

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
                GameObject spawnedTower = Instantiate(tower, this.transform);
                spawnedTower.transform.position = touchPosition;
                towerButton.disableToogle(0);
            }
        }
    }
}
