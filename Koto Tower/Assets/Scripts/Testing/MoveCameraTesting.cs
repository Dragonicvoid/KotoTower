using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraTesting : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] Renderer kotoTowerRenderer, generatorRenderer;
    Vector2 cameraPosition;
    Camera cam;

    // initialize variables
    private void Awake()
    {
        cameraPosition = new Vector2(0f, 0f);
        cam = this.GetComponent<Camera>();
    }

    // check if generator or kotoTower is off screen
    private void Start()
    {
        checkKotoTowerAndGeneratorOnScreen();
    }

    // Update is called once per frame
    void Update()
    {
        // If there is touch(es)
        if (Input.touchCount > 0 && !ButtonForTowerTesting.isSelectTower)
        {
            Touch touch = Input.GetTouch(0);

            // Get the vector and move camera horizontally if it moves
            if (touch.phase == TouchPhase.Moved)
            { 
                Vector2 touchDelta = touch.deltaPosition;

                // move the camera if the delta is big enough
                if (Mathf.Abs(touchDelta.x) > 0.5f)
                    this.transform.Translate(-touchDelta.x * speed * Time.deltaTime, 0f, 0f);
            }
        }

        // If there is scroll wheel input move the screen (for PC debugging)
        if (Input.mouseScrollDelta.y != 0)
            this.transform.Translate(-Input.mouseScrollDelta.y * speed * Time.deltaTime * 15f, 0f, 0f);

        // Move and prevent the screen moves more than it needs to
        this.transform.position = new Vector3(
                                    Mathf.Clamp(transform.position.x, minX, maxX),
                                    0f,
                                    -10f);

        checkKotoTowerAndGeneratorOnScreen();
    }

    // go to minimal horizontal plane
    public void goToMinX()
    {
        this.transform.position = new Vector3(minX, 0f, 0f);
    }

    // go to maximal horizontal plane
    public void goToMaxX()
    {
        this.transform.position = new Vector3(maxX, 0f, 0f);
    }

    // Calling event
    private void checkKotoTowerAndGeneratorOnScreen()
    {
        if (OtherMethodTesting.isVisibleFrom(kotoTowerRenderer, cam))
        {
            GameEventsTesting.current.ObjectOnScreenEnter(0);
            GameEventsTesting.current.ObjectOnScreenEnter(2);
        }
        else
        {
            GameEventsTesting.current.ObjectOffScreenEnter(0);
            GameEventsTesting.current.ObjectOffScreenEnter(2);
        }

        if (OtherMethodTesting.isVisibleFrom(generatorRenderer, cam))
        {
            GameEventsTesting.current.ObjectOnScreenEnter(1);
            GameEventsTesting.current.ObjectOnScreenEnter(3);
        }
        else
        {
            GameEventsTesting.current.ObjectOffScreenEnter(1);
            GameEventsTesting.current.ObjectOffScreenEnter(3);
        } 
    }
}
