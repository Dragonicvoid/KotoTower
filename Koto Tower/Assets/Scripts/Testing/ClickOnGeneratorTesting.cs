﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnGeneratorTesting : MonoBehaviour
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
        clickGeneratorBehave();
    }

    void clickGeneratorBehave()
    {
        // If there is touch(es)
        if (Input.touchCount > 0 && !isAnimating)
            StartCoroutine(touchOnGenerator());

        // If there is scroll wheel input move the screen (for PC debugging)
        if (Input.touchCount == 0 && Input.GetMouseButtonDown(0) && !isAnimating)
            StartCoroutine(mouseClickOnOnGenerator());
    }

    IEnumerator mouseClickOnOnGenerator()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(cam.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 15));

        foreach (RaycastHit2D hit in hits)
        {
            // Get the vector and move camera horizontally if it moves
            if (hit.collider != null && hit.collider.gameObject.tag.Equals("Generator"))
            {
                isAnimating = true;
                if (isClosed)
                {
                    GameEventsTesting.current.ObjectOnScreenEnter(3);
                    isClosed = false;
                }
                else
                {
                    GameEventsTesting.current.ObjectOffScreenEnter(3);
                    isClosed = true;
                }
            }
        }

        yield return new WaitForSeconds(0.60f);
        isAnimating = false;
    }

    IEnumerator touchOnGenerator()
    {
        Touch touch = Input.GetTouch(0);
        RaycastHit2D[] hits = Physics2D.RaycastAll(cam.ScreenToWorldPoint(touch.position), new Vector3(0, 0, 15));

        foreach (RaycastHit2D hit in hits)
        {
            // Get the vector and move camera horizontally if it moves
            if (touch.phase == TouchPhase.Began && hit.collider != null && hit.collider.gameObject.tag.Equals("Generator"))
            {
                isAnimating = true;
                if (isClosed)
                {
                    GameEventsTesting.current.ObjectOnScreenEnter(3);
                    isClosed = false;
                }
                else
                {
                    GameEventsTesting.current.ObjectOffScreenEnter(3);
                    isClosed = true;
                }
            }
        }

        yield return new WaitForSeconds(0.60f);
        isAnimating = false;
    }

    // close the generator
    public IEnumerator closeGenerator()
    {
        isAnimating = true;
        if (!isClosed)
        {
            GameEventsTesting.current.ObjectOffScreenEnter(3);
            isClosed = true;

            yield return new WaitForSeconds(0.60f);
        }
        
        isAnimating = false;
    }

    // open the generator
    public IEnumerator openGenerator()
    {
        isAnimating = true;
        if (isClosed)
        {
            GameEventsTesting.current.ObjectOnScreenEnter(3);
            isClosed = false;

            yield return new WaitForSeconds(0.60f);
        }

        isAnimating = false;
    }

    public bool getisClose()
    {
        return isClosed;
    }
}
