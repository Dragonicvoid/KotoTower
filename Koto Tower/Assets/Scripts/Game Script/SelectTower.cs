using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTower : MonoBehaviour
{
    Camera mainCamera;
    Vector3 firstAreaDetection;
    bool isPressOnTower;
    TowerBehaviour currentTower;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = this.gameObject.GetComponent<Camera>();
        firstAreaDetection = new Vector3();
        currentTower = null;
        isPressOnTower = false;
    }

    // only when it is on normal camera mode
    private void Update()
    {
        if (Input.touchCount > 0 && !GameManager.instance.isSelectTower && !GameManager.instance.isSelectTrap && !GameManager.instance.isPaused)
            detectPressOnTower();

        if(Input.touchCount == 0 && !GameManager.instance.isSelectTower && !GameManager.instance.isSelectTrap && !GameManager.instance.isPaused) 
            detectClickOnTower();
    }

    // for tidiness
    void detectPressOnTower()
    {
        Touch touch = Input.GetTouch(0);
        // only see if the finger touch the area and open on the same area
        if (touch.phase == TouchPhase.Began)
        {
            Vector3 touchLocation = mainCamera.ScreenToWorldPoint(touch.position);

            if (isPressOnTower && Vector3.Distance(touchLocation, firstAreaDetection) > 0.3f)
                isPressOnTower = false;

            firstAreaDetection = mainCamera.ScreenToWorldPoint(touch.position);
        }
            
        if (touch.phase == TouchPhase.Ended)
        {
            Vector3 touchLocation = mainCamera.ScreenToWorldPoint(touch.position);
            Ray ray = mainCamera.ScreenPointToRay(touchLocation);
            RaycastHit2D hit = Physics2D.Raycast(touchLocation, new Vector3(0, 0, 15));

            if (Vector3.Distance(touchLocation, firstAreaDetection) <= 0.3f)
            {
                // show the circle if it is tower
                if (hit.collider != null && "Tower".Equals(hit.collider.gameObject.tag) && isPressOnTower)
                {
                    // if we already select a tower
                    if (GameManager.instance.currentStatus == GameStatus.SELECTING_TOWER)
                        currentTower.unshowCircle();

                    currentTower = hit.collider.gameObject.GetComponent<TowerBehaviour>();
                    currentTower.showCircle();
                    GameEvents.current.TowerSelected(currentTower);
                    GameManager.instance.currentStatus = GameStatus.SELECTING_TOWER;
                }
            }

            if (!isPressOnTower && hit.collider != null && "Tower".Equals(hit.collider.gameObject.tag))
                isPressOnTower = true;
        }
    }

    // for tidiness
    void detectClickOnTower()
    {
        // only see if the finger touch the area and open on the same area
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touchLocation = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (isPressOnTower && Vector3.Distance(touchLocation, firstAreaDetection) > 0.3f)
                isPressOnTower = false;

            firstAreaDetection = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 touchLocation = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = mainCamera.ScreenPointToRay(touchLocation);
            RaycastHit2D hit = Physics2D.Raycast(touchLocation, new Vector3(0, 0, 15));

            if (Vector3.Distance(touchLocation, firstAreaDetection) < 0.3f)
            {
                // show the circle if it is tower
                if (hit.collider != null && "Tower".Equals(hit.collider.gameObject.tag) && isPressOnTower)
                {
                    if (GameManager.instance.currentStatus == GameStatus.SELECTING_TOWER)
                        currentTower.unshowCircle();

                    currentTower = hit.collider.gameObject.GetComponent<TowerBehaviour>();
                    currentTower.showCircle();
                    GameEvents.current.TowerSelected(currentTower);
                    GameManager.instance.currentStatus = GameStatus.SELECTING_TOWER;
                }
            }

            if (!isPressOnTower && hit.collider != null && "Tower".Equals(hit.collider.gameObject.tag))
                isPressOnTower = true;
        }
    }

    //unshow the circle
    public void unshowCurrentTower()
    {
        currentTower.unshowCircle();
    }
}
