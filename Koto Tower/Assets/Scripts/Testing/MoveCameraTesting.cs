using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraTesting : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float minX = 0f;
    [SerializeField] float maxX = 0f;
    [SerializeField] float maxTimer = 0.5f;
    bool isMoved;
    bool isTouchedLeft;
    bool isTouchedRight;
    float rightTimer;
    float leftTimer;
    Renderer kotoTowerRenderer, generatorRenderer;
    ClickOnKotoTowerTesting kotoTower;
    ClickOnGeneratorTesting generator;
    Vector2 cameraPosition;
    Camera cam;

    // initialize variables
    private void Awake()
    {
        isMoved = false;
        isTouchedLeft = false;
        isTouchedRight = false;
        rightTimer = 0f;
        leftTimer = 0f;
        cameraPosition = new Vector2(0f, 0f);
        cam = this.GetComponent<Camera>();
        kotoTowerRenderer = GameObject.FindGameObjectWithTag("Koto Tower").GetComponent<SpriteRenderer>();
        generatorRenderer = GameObject.FindGameObjectWithTag("Generator").GetComponent<SpriteRenderer>();
        kotoTower = GameObject.FindGameObjectWithTag("Koto Tower").GetComponent<ClickOnKotoTowerTesting>();
        generator = GameObject.FindGameObjectWithTag("Generator").GetComponent<ClickOnGeneratorTesting>();
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
            if (touch.phase == TouchPhase.Moved
                && (touch.position.y > (Screen.height * 16 / 100) || touch.position.x > (Screen.width * 32 / 100))
                && (touch.position.y > (Screen.height * 16 / 100) || touch.position.x < (Screen.width * 68 / 100)))
            { 
                Vector2 touchDelta = touch.deltaPosition;

                // move the camera if the delta is big enough
                if (Mathf.Abs(touchDelta.x) > 0.05f)
                {
                    this.transform.Translate(-touchDelta.x * speed * Time.deltaTime, 0f, 0f);
                    this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, minX, maxX), 0f, -10f);
                    isMoved = true;
                }
            }
        }

        // If there is scroll wheel input move the screen (for PC debugging)
        if (Input.mouseScrollDelta.y != 0 && !ButtonForTowerTesting.isSelectTower)
        {
            this.transform.Translate(-Input.mouseScrollDelta.y * speed * Time.deltaTime * 15f, 0f, 0f);
            this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, minX, maxX), 0f, -10f);
            isMoved = true;
        }

        if(!ButtonForTowerTesting.isSelectTower)
            checkLeftOrRightClick();

        if (isMoved)
            checkKotoTowerAndGeneratorOnScreen();

        // Look at timer if the screen on left is pressed
        if (isTouchedLeft)
            leftTimer += Time.deltaTime;

        if (leftTimer > maxTimer)
        {
            leftTimer = 0f;
            isTouchedLeft = false;
        }

        // Look at timer if the screen on right is pressed
        if (isTouchedRight)
            rightTimer += Time.deltaTime;

        if (rightTimer > maxTimer)
        {
            rightTimer = 0f;
            isTouchedRight = false;
        }

        isMoved = false;
    }

    // go to minimal horizontal plane
    public void goToMinX()
    {
        this.transform.position = new Vector3(minX, 0f, -10f);
        leftTimer = 0f;
        isTouchedLeft = false;
        isMoved = true;
    }

    // go to maximal horizontal plane
    public void goToMaxX()
    {
        this.transform.position = new Vector3(maxX, 0f, -10f);
        rightTimer = 0f;
        isTouchedRight = false;
        isMoved = true;
    }

    //check left or right click
    private void checkLeftOrRightClick()
    {
        // If there is touch(es)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began
                && ((touch.position.y > (Screen.height * 16 / 100) || touch.position.x < (Screen.width * 68 / 100))
                && touch.position.x >= (Screen.width * 0.5f)))
            {
                if (!isTouchedLeft)
                    isTouchedLeft = true;
                else
                    goToMinX();
            }
            else if (touch.phase == TouchPhase.Began
                && ((touch.position.y > (Screen.height * 16 / 100) || touch.position.x < (Screen.width * 68 / 100))
                && touch.position.x >= (Screen.width * 0.5f)))
            {
                if (!isTouchedRight)
                    isTouchedRight = true;
                else
                    goToMaxX();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;

            if ((mousePosition.y > (Screen.height * 16 / 100) || mousePosition.x > (Screen.width * 32 / 100))
                && mousePosition.x < (Screen.width * 0.5f))
            {
                if (!isTouchedLeft)
                    isTouchedLeft = true;
                else
                    goToMinX();
            }
            else if ((mousePosition.y > (Screen.height * 16 / 100) || mousePosition.x < (Screen.width * 68 / 100))
                && mousePosition.x >= (Screen.width * 0.5f))
            {
                if (!isTouchedRight)
                    isTouchedRight = true;
                else
                    goToMaxX();
            }
        }
    }

    // Calling event if is not visiole
    private void checkKotoTowerAndGeneratorOnScreen()
    {
        if (!OtherMethodTesting.isVisibleFrom(kotoTowerRenderer, cam) && !kotoTower.getisClose())
            kotoTower.StartCoroutine(kotoTower.closeKotoTower());

        if (!OtherMethodTesting.isVisibleFrom(generatorRenderer, cam) && !generator.getisClose())
            generator.StartCoroutine(generator.closeGenerator());
    }
}
