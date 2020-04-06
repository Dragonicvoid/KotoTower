using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorGridBlocker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector2 position = this.transform.position;

        // get x,y of the tower in the grid
        int x, y;
        GridCustom.getXYFromPosition(position, out x, out y);

        x += GridCustom.offsetX;
        y += GridCustom.offsetY;

        // blocks the inside of tower
        GridCustom.cells[x, y].cellContent = CellContent.GENERATOR;
        Debug.DrawLine(GridCustom.getWorldSpace(x, y), GridCustom.getWorldSpace(x + 1, y), Color.yellow, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x, y), GridCustom.getWorldSpace(x, y + 1), Color.yellow, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x + 1, y), GridCustom.getWorldSpace(x + 1, y + 1), Color.yellow, 100f, false);

        GridCustom.cells[x, y - 1].cellContent = CellContent.GENERATOR;
        Debug.DrawLine(GridCustom.getWorldSpace(x, y - 1), GridCustom.getWorldSpace(x, y), Color.yellow, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x, y - 1), GridCustom.getWorldSpace(x + 1, y - 1), Color.yellow, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x + 1, y - 1), GridCustom.getWorldSpace(x + 1, y), Color.yellow, 100f, false);

        Debug.DrawLine(GridCustom.getWorldSpace(x, y + 1), GridCustom.getWorldSpace(x + 1, y + 1), Color.yellow, 100f, false);

        // blocks behind of the generator
        for (int i = x + 1; i < GridCustom.width; i++)
            for (int j = 0; j < GridCustom.height; j++)
            {
                GridCustom.cells[i, j].cellContent = CellContent.KOTO_TOWER;
                Debug.DrawLine(GridCustom.getWorldSpace(i, j), GridCustom.getWorldSpace(i + 1, j), Color.yellow, 100f, false);
                Debug.DrawLine(GridCustom.getWorldSpace(i, j), GridCustom.getWorldSpace(i, j + 1), Color.yellow, 100f, false);
                Debug.DrawLine(GridCustom.getWorldSpace(i + 1, j), GridCustom.getWorldSpace(i + 1, j + 1), Color.yellow, 100f, false);
                Debug.DrawLine(GridCustom.getWorldSpace(i, j + 1), GridCustom.getWorldSpace(i + 1, j + 1), Color.yellow, 100f, false);
            }
    }
}
