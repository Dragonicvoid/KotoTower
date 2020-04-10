using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnKotoTower : MonoBehaviour
{
    // since there will only be one Koto Tower create variable so we can see if there is ongoing animation
    bool isAnimating = false;
    Camera cam;
    bool isClosed;
    // initialization
    private void Start()
    {
        cam = Camera.main;
        isClosed = false;
        isAnimating = false;
    }

    // check every frame if we hit the tower or not
    private void Update()
    {
        if (!GameManager.instance.isPaused)
            clickKotoTowerBehave();
    }

    void clickKotoTowerBehave()
    {
        // If there is touch(es)
        if (Input.touchCount > 0 && !isAnimating)
            StartCoroutine(touchOnKotoTower());

        // If there is scroll wheel input move the screen (for PC debugging)
        if (Input.touchCount == 0 && Input.GetMouseButtonDown(0) && !isAnimating)
            StartCoroutine(mouseClickOnKotoTower());
    }

    IEnumerator mouseClickOnKotoTower()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(cam.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 15));

        foreach(RaycastHit2D hit in hits)
        {
            // get the collider, and if it's Koto Tower and now the truck is sent then you can click the tower
            if (hit.collider != null && hit.collider.gameObject.tag.Equals("Koto Tower") && !GameManager.instance.isSelectTower && !GameManager.instance.isSelectTrap && !isAnimating)
            {
                isAnimating = true;
                if (isClosed)
                {
                    GameEvents.current.OpenKotoTowerBalloonBox();
                    isClosed = false;
                }
                else
                {
                    GameEvents.current.CloseKotoTowerBalloonBox();
                    isClosed = true;
                }
            }
        }

        yield return new WaitForSeconds(0.2f);
        isAnimating = false;
    }

    IEnumerator touchOnKotoTower()
    {
        Touch touch = Input.GetTouch(0);
        RaycastHit2D[] hits = Physics2D.RaycastAll(cam.ScreenToWorldPoint(touch.position), new Vector3(0, 0, 15));

        foreach (RaycastHit2D hit in hits)
        {
            // get the collider, and if it's Koto Tower and now the truck is sent then you can click the tower
            if (touch.phase == TouchPhase.Began && hit.collider != null && hit.collider.gameObject.tag.Equals("Koto Tower") && !GameManager.instance.isSelectTower && !GameManager.instance.isSelectTrap && !isAnimating)
            {
                isAnimating = true;
                if (isClosed)
                {
                    GameEvents.current.OpenKotoTowerBalloonBox();
                    isClosed = false;
                }
                else
                {
                    GameEvents.current.CloseKotoTowerBalloonBox();
                    isClosed = true;
                }
            }
        }

        yield return new WaitForSeconds(0.2f);
        isAnimating = false;
    }

    public IEnumerator closeKotoTower()
    {
        if (!isClosed && !isAnimating)
        {
            isAnimating = true;
            GameEvents.current.CloseKotoTowerBalloonBox();
            isClosed = true;

            yield return new WaitForSeconds(0.2f);
        }
        isAnimating = false;
        yield return null;
    }

    public IEnumerator openKotoTower()
    {
        if (isClosed && !isAnimating)
        {
            isAnimating = true;
            GameEvents.current.OpenKotoTowerBalloonBox();
            isClosed = false;

            yield return new WaitForSeconds(0.2f);
        }
        isAnimating = false;
        yield return null;
    }

    public bool getisClose()
    {
        return isClosed;
    }

    // when on screen make the koto tower button invisible
    private void OnBecameVisible()
    {
        if (GameManager.instance.isNewQuestion)
            GameEvents.current.KotoTowerOnScreenEnter();
    }

    // when on screen make the koto tower button visible
    private void OnBecameInvisible()
    {
        if (GameManager.instance.isNewQuestion)
            GameEvents.current.KotoTowerOffScreenEnter();
    }
}
