using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonForTowerTesting : MonoBehaviour, IPointerClickHandler
{
    // filter and cancel button
    [SerializeField] GameObject filter;
    [SerializeField] CancelTrapTowerTesting cancelButton;

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
    ButtonForTrapTesting trapUi;

    // koto tower and generator
    ClickOnGeneratorTesting generator;
    ClickOnKotoTowerTesting kotoTower;

    // Default value is not Selected
    private void Awake()
    {
        trapUi = this.gameObject.GetComponent<ButtonForTrapTesting>();
        countLine = 0;
        GameManager.instance.resetOnPlay();
    }

    // Drawing all possible grid to place tower
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
        this.lineParent = new GameObject();
        lineParent.name = "Line Tower Parent";
        lineParent.transform.position = new Vector3(0f, 0f, 0f);
        lineParent.transform.parent = this.gameObject.transform;
        checkVertically();
        checkHorizontally();
        this.lineParent.SetActive(false);
        Debug.Log("Total Tower Line : " + countLine);

        GameManager.instance.isDoneMakingTowerLines = true;
    }

    // Is change the state if it is to spawn tower, or want to move camera (can't be both)
    public void selectTower(int idx)
    {
        if (GameManager.instance.isSelectTrap)
            trapUi.disableButton(GameManager.instance.selectedTrap);

        // all condition selecting button
        if (GameManager.instance.selectedTower == -1)
            enableToogle(idx);
        else if (GameManager.instance.selectedTower == idx)
        {
            GameManager.instance.isSelectTower = false;
            GameManager.instance.selectedTower = -1;
            disableButton(idx);
        }
        else
        {
            clearAllButtons();
            enableToogle(idx);
        }

        // close the koto tower and generator balloon box
        generator.StartCoroutine(generator.closeGenerator());
        kotoTower.StartCoroutine(kotoTower.closeKotoTower());

        // Show the possible line
        if (GameManager.instance.isDoneMakingTowerLines && GameManager.instance.isSelectTower)
            lineParent.SetActive(true);
        else if (GameManager.instance.isDoneMakingTowerLines && !GameManager.instance.isSelectTower)
            lineParent.SetActive(false);
    }

    // disable the button
    public void disableButton(int idx)
    {
        ButtonChangeColorTesting selectedButton = buttons[idx];
        selectedButton.disable();
        GameManager.instance.isSelectTower = false;
        GameManager.instance.selectedTower = -1;
        lineParent.SetActive(false);
    }

    // enable the button
    public void enableToogle(int idx)
    {
        if (GameManager.instance.money >= GameManager.instance.towerPrices[idx].price)
        {
            ButtonChangeColorTesting selectedButton = buttons[idx];
            selectedButton.activate();
            GameManager.instance.isSelectTower = true;
            GameManager.instance.selectedTower = (short)idx;
            filter.SetActive(true);
            cancelButton.readyToSpawn();
        }         
    }

    // on the button click, this is for the mouse click (debug)
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.isPressedButtonTower = true;
    }

    // clearing all button
    void clearAllButtons()
    {
        GameManager.instance.isSelectTower = false;
        GameManager.instance.selectedTower = -1;

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
                if (GridTesting.cells[j, i].cellContent == CellContent.OPEN_FIELD || GridTesting.cells[j, i].cellContent == CellContent.TOWER)
                {
                    // if the bottom is not open field or tower then just continue, for artistry purpose
                    if (!(GridTesting.cells[j, i - 1].cellContent == CellContent.OPEN_FIELD || GridTesting.cells[j, i - 1].cellContent == CellContent.TOWER))
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
                if (GridTesting.cells[i, j].cellContent == CellContent.OPEN_FIELD || GridTesting.cells[i, j].cellContent == CellContent.TOWER)
                {
                    // if the bottom is not open field or tower then just continue, for artistry purpose
                    if (!(GridTesting.cells[i - 1, j].cellContent == CellContent.OPEN_FIELD || GridTesting.cells[i - 1, j].cellContent == CellContent.TOWER))
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
