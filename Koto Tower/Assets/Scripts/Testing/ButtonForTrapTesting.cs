using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonForTrapTesting : MonoBehaviour, IPointerClickHandler
{
    // property
    int countLine;
    class Line
    {
        public Vector3 startVector;
        public Vector3 endVector;
        public bool isStart;

        public Line()
        {
            reset();
        }

        public void reset()
        {
            startVector = new Vector3();
            endVector = new Vector3();
            isStart = false;
        }
    }

    GameObject lineParent;
    [SerializeField] List<ButtonChangeColorTesting> buttons = null;

    // property id for color;
    int colorPropertyId = Shader.PropertyToID("_Color");

    // Connect to Tower
    ButtonForTowerTesting towerUi;

    // koto tower and generator
    ClickOnGeneratorTesting generator;
    ClickOnKotoTowerTesting kotoTower;

    // Default value is not Selected
    private void Awake()
    {
        towerUi = this.gameObject.GetComponent<ButtonForTowerTesting>();
        countLine = 0;
        GameManager.instance.resetOnPlay();
    }

    // Drawing all possible grid to place trap
    private void Start()
    {
        kotoTower = GameObject.FindGameObjectWithTag("Koto Tower").GetComponent<ClickOnKotoTowerTesting>();
        generator = GameObject.FindGameObjectWithTag("Generator").GetComponent<ClickOnGeneratorTesting>();

        foreach (ButtonChangeColorTesting button in buttons)
            button.disable();
        StartCoroutine(lateDraw(0.3f));
    }

    // Create all possible grid line
    IEnumerator lateDraw(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        lineParent = new GameObject();
        lineParent.name = "Line Trap Parent";
        lineParent.transform.position = new Vector3(0f, 0f, 0f);
        lineParent.transform.parent = this.gameObject.transform;
        checkVertically();
        checkHorizontally();
        lineParent.SetActive(false);
        Debug.Log("Total Trap Line : " + countLine);

        GameManager.instance.isDoneMakingTrapLines = true;
    }

    // Is change the state if it is to spawn trap, or want to move camera (can't be both)
    public void selectTrap(int idx)
    {
        if (GameManager.instance.isSelectTower)
            towerUi.disableButton(GameManager.instance.selectedTower);

        // all condition selecting button
        if (GameManager.instance.selectedTrap == -1)
            enableButton(idx);
        else if (GameManager.instance.selectedTrap == idx)
        {
            GameManager.instance.isSelectTrap = false;
            GameManager.instance.selectedTrap = -1;
            disableButton(idx);
        }
        else
        {
            clearAllButtons();
            enableButton(idx);
        }

        // close the koto tower and generator balloon box
        generator.StartCoroutine(generator.closeGenerator());
        kotoTower.StartCoroutine(kotoTower.closeKotoTower());

        // Show the possible line
        if (GameManager.instance.isDoneMakingTrapLines && GameManager.instance.isSelectTrap)
            lineParent.SetActive(true);
        else if (GameManager.instance.isDoneMakingTrapLines && !GameManager.instance.isSelectTrap)
            lineParent.SetActive(false);
    }

    // disable the button
    public void disableButton(int idx)
    {
        ButtonChangeColorTesting selectedButton = buttons[idx];
        selectedButton.disable();
        GameManager.instance.isSelectTrap = false;
        GameManager.instance.selectedTrap = -1;
        lineParent.SetActive(false);
    }

    // enable the button
    public void enableButton(int idx)
    {
        if (GameManager.instance.money >= GameManager.instance.trapPrices[idx].price)
        {
            ButtonChangeColorTesting selectedButton = buttons[idx];
            selectedButton.activate();
            GameManager.instance.isSelectTrap = true;
            GameManager.instance.selectedTrap = (short)idx;
        }          
    }

    // on the button click, this is for the mouse click (debug)
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.isPressedButtonTrap = true;
    }

    // clearing all button
    void clearAllButtons()
    {
        GameManager.instance.isSelectTrap = false;
        GameManager.instance.selectedTrap = -1;

        foreach (ButtonChangeColorTesting button in buttons)
            button.disable();
    }

    //Drawing a line
    void drawLine(Vector3 start, Vector3 end, Color color, Transform parent)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start + new Vector3(0, 0, 1);
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.sortingOrder = 1;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.material.color = color;
        lr.positionCount = 2;
        lr.startColor = color;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        myLine.name = "line";
        myLine.transform.parent = parent;
    }

    // check line vertically
    void checkHorizontally()
    {
        // Vertical
        for (int i = 1; i < GridTesting.height; i++)
        {
            // get start and end line
            Line line = new Line();
            for (int j = 0; j < GridTesting.width; j++)
            {
                // if it is on open field or tower
                if (GridTesting.cells[j, i].cellContent == CellContent.PATH)
                {
                    // if the bottom is not open field or tower then just continue, for artistry purpose
                    if (!(GridTesting.cells[j, i - 1].cellContent == CellContent.PATH))
                    {
                        // if there is a start line then just end it there, looks too ugly
                        if (line.isStart)
                        {
                            line.endVector = GridTesting.getWorldSpace(j, i);
                            drawLine(line.startVector, line.endVector, new Color(0, 1, 0, 100f), lineParent.transform);
                            countLine++;
                            line.reset();
                        }
                        continue;
                    }

                    // if this is start of a line
                    if (!line.isStart)
                    {
                        line.startVector = GridTesting.getWorldSpace(j, i);
                        line.endVector = GridTesting.getWorldSpace(j, i);
                        line.isStart = true;
                    }
                    else
                        line.endVector = GridTesting.getWorldSpace(j, i);
                }
                else // If it is not on the open field or tower
                {
                    if (line.isStart)
                    {
                        line.endVector = GridTesting.getWorldSpace(j, i);
                        drawLine(line.startVector, line.endVector, new Color(0, 1, 0, 100f), lineParent.transform);
                        countLine++;
                        line.reset();
                    }
                }
            }
            // if there is a start line but end of index
            if (line.isStart)
            {
                line.endVector = GridTesting.getWorldSpace(GridTesting.width - 1, i);
                drawLine(line.startVector, line.endVector, new Color(0, 1, 0, 100f), lineParent.transform);
                countLine++;
            }
        }
    }

    // check line horizontally
    void checkVertically()
    {
        // Vertical
        for (int i = 1; i < GridTesting.width; i++)
        {
            // get start and end line
            Line line = new Line();
            for (int j = 0; j < GridTesting.height; j++)
            {
                // if it is on open field or tower
                if (GridTesting.cells[i, j].cellContent == CellContent.PATH)
                {
                    // if the bottom is not open field or tower then just continue, for artistry purpose
                    if (!(GridTesting.cells[i - 1, j].cellContent == CellContent.PATH))
                    {
                        // if there is a start line then just end it there, looks too ugly
                        if (line.isStart)
                        {
                            line.endVector = GridTesting.getWorldSpace(i, j);
                            drawLine(line.startVector, line.endVector, new Color(0, 1, 0, 100f), lineParent.transform);
                            countLine++;
                            line.reset();
                        }
                        continue;
                    }

                    // if there is no start line
                    if (!line.isStart)
                    {
                        line.startVector = GridTesting.getWorldSpace(i, j);
                        line.endVector = GridTesting.getWorldSpace(i, j);
                        line.isStart = true;
                    }
                    else
                        line.endVector = GridTesting.getWorldSpace(i, j);
                }
                else // If it is not on the open field or tower
                {
                    if (line.isStart)
                    {
                        line.endVector = GridTesting.getWorldSpace(i, j);
                        drawLine(line.startVector, line.endVector, new Color(0, 1, 0, 100f), lineParent.transform);
                        countLine++;
                        line.reset();
                    }
                }
            }

            // if there is a start line but end of index
            if (line.isStart)
            {
                line.endVector = GridTesting.getWorldSpace(i, GridTesting.height - 1);
                drawLine(line.startVector, line.endVector, new Color(0, 1, 0, 100f), lineParent.transform);
                countLine++;
            }
        }
    }
}
