using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonForTowerTesting : MonoBehaviour, IPointerClickHandler
{
    public static bool isSelectTower;
    public static bool isPressedButton;
    public static bool isDoneMakingLines;
    GameObject lineParent;
    string colorId = "_Color";
    [SerializeField] List<Toggle> toggles = null;

    // property id for color;
    int colorPropertyId = Shader.PropertyToID("_Color");

    // Default value is not Selected
    private void Awake()
    {
        isSelectTower = false;
        isPressedButton = false;
        foreach (Toggle toggle in toggles)
            toggle.isOn = false;
    }

    // Drawing all possible grid to place tower
    private void Start()
    {
        StartCoroutine(lateDraw(4f));
    }

    // Create all possible grid line
    IEnumerator lateDraw(float waitTime)
    {
        this.lineParent = Instantiate(new GameObject());
        lineParent.transform.position = new Vector3(0f, 0f, 0f);

        for (int i = 0; i < GridTesting.width; i++)
        {
            for (int j = 0; j < GridTesting.height; j++)
            {
                if (GridTesting.cells[i, j].cellContent == CellContent.OPEN_FIELD || GridTesting.cells[i, j].cellContent == CellContent.TOWER)
                {
                    drawLine(GridTesting.getWorldSpace(i, j), GridTesting.getWorldSpace(i + 1, j), Color.green, lineParent.transform);
                    drawLine(GridTesting.getWorldSpace(i, j), GridTesting.getWorldSpace(i, j + 1), Color.green, lineParent.transform);
                    drawLine(GridTesting.getWorldSpace(i + 1, j), GridTesting.getWorldSpace(i + 1, j + 1), Color.green, lineParent.transform);
                    drawLine(GridTesting.getWorldSpace(i, j + 1), GridTesting.getWorldSpace(i + 1, j + 1), Color.green, lineParent.transform);
                }
            }
        }

        this.lineParent.SetActive(false);

        isDoneMakingLines = true;
        yield return new WaitForSeconds(waitTime);
    }

    // Is change the state if it is to spawn tower, or want to move camera (can't be both)
    public void selectTower()
    {
        isSelectTower = !isSelectTower;

        // Show the possible line
        if (isDoneMakingLines && isSelectTower)
            lineParent.SetActive(true);
        else if (isDoneMakingLines && !isSelectTower)
            lineParent.SetActive(false);
    }

    // disable the toggle
    public void disableToogle(int idx)
    {
        Toggle selectedToggle = toggles[idx];
        selectedToggle.isOn = false;
        isSelectTower = false;
    }

    // on the toggle click, this is for the mouse click (debug)
    public void OnPointerClick(PointerEventData eventData)
    {
        isPressedButton = true;
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
        lr.SetVertexCount(2);
        lr.startColor = color;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        myLine.name = "line";
        myLine.transform.parent = parent;
    }
}
