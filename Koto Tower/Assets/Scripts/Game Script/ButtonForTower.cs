using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonForTower : MonoBehaviour, IPointerClickHandler
{
    // filter and cancel button
    [SerializeField] GameObject filter = null;
    [SerializeField] CancelTrapTower cancelButton = null;
    [SerializeField] GameObject desc = null;

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
    [SerializeField] List<ButtonChangeColor> buttons = null;

    // property id for color;
    int colorPropertyId = Shader.PropertyToID("_Color");

    // Connect to Tower
    ButtonForTrap trapUi;

    // koto tower and generator
    ClickOnGenerator generator;
    ClickOnKotoTower kotoTower;

    // Default value is not Selected
    private void Awake()
    {
        trapUi = this.gameObject.GetComponent<ButtonForTrap>();
        countLine = 0;
        GameManager.instance.resetOnPlay();
    }

    // Drawing all possible grid to place tower
    private void Start()
    {
        kotoTower = GameObject.FindGameObjectWithTag("Koto Tower").GetComponent<ClickOnKotoTower>();
        generator = GameObject.FindGameObjectWithTag("Generator").GetComponent<ClickOnGenerator>();

        foreach (ButtonChangeColor button in buttons)
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

        // Show the possible line
        if (GameManager.instance.isDoneMakingTowerLines && GameManager.instance.isSelectTower)
            lineParent.SetActive(true);
        else if (GameManager.instance.isDoneMakingTowerLines && !GameManager.instance.isSelectTower)
            lineParent.SetActive(false);
    }

    // disable the button
    public void disableButton(int idx)
    {
        ButtonChangeColor selectedButton = buttons[idx];
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
            if(GameManager.instance.gameStart)
            {
                // close the koto tower and generator balloon box
                generator.StartCoroutine(generator.closeGenerator());
                kotoTower.StartCoroutine(kotoTower.closeKotoTower());
            }

            // select the tower
            ButtonChangeColor selectedButton = buttons[idx];
            selectedButton.activate();
            GameManager.instance.isSelectTower = true;
            GameManager.instance.selectedTower = (short)idx;
            DescriptionForBuilding descForBuilding = desc.GetComponent<DescriptionForBuilding>();
            descForBuilding.changeDesc(true, idx);
            desc.gameObject.SetActive(true);
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

        foreach (ButtonChangeColor button in buttons)
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
        for (int i = 1; i < GridCustom.height; i++)
        {
            // get start and end line
            Line line = new Line();
            for (int j = 0; j < GridCustom.width; j++)
            {
                // if it is on open field or tower
                if (GridCustom.cells[j, i].cellContent == CellContent.OPEN_FIELD || GridCustom.cells[j, i].cellContent == CellContent.TOWER)
                {
                    // if the bottom is not open field or tower then just continue, for artistry purpose
                    if (!(GridCustom.cells[j, i - 1].cellContent == CellContent.OPEN_FIELD || GridCustom.cells[j, i - 1].cellContent == CellContent.TOWER))
                    {
                        // if there is a start line then just end it there, looks too ugly
                        if (line.isStart)
                        {
                            line.endVector = GridCustom.getWorldSpace(j, i);
                            drawLine(line.startVector, line.endVector, new Color(0, 1, 0, 100f), lineParent.transform);
                            countLine++;
                            line.reset();
                        }
                        continue;
                    }

                    // if this is start of a line
                    if (!line.isStart)
                    {
                        line.startVector = GridCustom.getWorldSpace(j, i);
                        line.endVector = GridCustom.getWorldSpace(j, i);
                        line.isStart = true;
                    }
                    else
                        line.endVector = GridCustom.getWorldSpace(j, i);
                }
                else // If it is not on the open field or tower
                {
                    if (line.isStart)
                    {
                        line.endVector = GridCustom.getWorldSpace(j, i);
                        drawLine(line.startVector, line.endVector, new Color(0, 1, 0, 100f), lineParent.transform);
                        countLine++;
                        line.reset();
                    }
                }
            }
            // if there is a start line but end of index
            if (line.isStart)
            {
                line.endVector = GridCustom.getWorldSpace(GridCustom.width - 1, i);
                drawLine(line.startVector, line.endVector, new Color(0, 1, 0, 100f), lineParent.transform);
                countLine++;
            }
        }
    }

    // check line horizontally
    void checkVertically()
    {
        // Vertical
        for (int i = 1; i < GridCustom.width; i++)
        {
            // get start and end line
            Line line = new Line();
            for (int j = 0; j < GridCustom.height; j++)
            {
                // if it is on open field or tower
                if (GridCustom.cells[i, j].cellContent == CellContent.OPEN_FIELD || GridCustom.cells[i, j].cellContent == CellContent.TOWER)
                {
                    // if the bottom is not open field or tower then just continue, for artistry purpose
                    if (!(GridCustom.cells[i - 1, j].cellContent == CellContent.OPEN_FIELD || GridCustom.cells[i - 1, j].cellContent == CellContent.TOWER))
                    {
                        // if there is a start line then just end it there, looks too ugly
                        if (line.isStart)
                        {
                            line.endVector = GridCustom.getWorldSpace(i, j);
                            drawLine(line.startVector, line.endVector, new Color(0, 1, 0, 100f), lineParent.transform);
                            countLine++;
                            line.reset();
                        }
                        continue;
                    }

                    // if there is no start line
                    if (!line.isStart)
                    {
                        line.startVector = GridCustom.getWorldSpace(i, j);
                        line.endVector = GridCustom.getWorldSpace(i, j);
                        line.isStart = true;
                    }
                    else
                        line.endVector = GridCustom.getWorldSpace(i, j);
                }
                else // If it is not on the open field or tower
                {
                    if (line.isStart)
                    {
                        line.endVector = GridCustom.getWorldSpace(i, j);
                        drawLine(line.startVector, line.endVector, new Color(0, 1, 0, 100f), lineParent.transform);
                        countLine++;
                        line.reset();
                    }
                }
            }

            // if there is a start line but end of index
            if (line.isStart)
            {
                line.endVector = GridCustom.getWorldSpace(i, GridCustom.height - 1);
                drawLine(line.startVector, line.endVector, new Color(0, 1, 0, 100f), lineParent.transform);
                countLine++;
            }
        }
    }
}
