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
        GridTesting.getXYFromPosition(position, out x, out y);

        x += GridTesting.offsetX;
        y += GridTesting.offsetY;

        // blocks the inside of tower
        GridTesting.cells[x, y].cellContent = CellContent.GENERATOR;
        Debug.DrawLine(GridTesting.getWorldSpace(x, y), GridTesting.getWorldSpace(x + 1, y), Color.yellow, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x, y), GridTesting.getWorldSpace(x, y + 1), Color.yellow, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x + 1, y), GridTesting.getWorldSpace(x + 1, y + 1), Color.yellow, 100f, false);

        GridTesting.cells[x, y - 1].cellContent = CellContent.GENERATOR;
        Debug.DrawLine(GridTesting.getWorldSpace(x, y - 1), GridTesting.getWorldSpace(x, y), Color.yellow, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x, y - 1), GridTesting.getWorldSpace(x + 1, y - 1), Color.yellow, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x + 1, y - 1), GridTesting.getWorldSpace(x + 1, y), Color.yellow, 100f, false);

        Debug.DrawLine(GridTesting.getWorldSpace(x, y + 1), GridTesting.getWorldSpace(x + 1, y + 1), Color.yellow, 100f, false);

        // blocks behind of the generator
        for (int i = x + 1; i < GridTesting.width; i++)
            for (int j = 0; j < GridTesting.height; j++)
            {
                GridTesting.cells[i, j].cellContent = CellContent.KOTO_TOWER;
                Debug.DrawLine(GridTesting.getWorldSpace(i, j), GridTesting.getWorldSpace(i + 1, j), Color.yellow, 100f, false);
                Debug.DrawLine(GridTesting.getWorldSpace(i, j), GridTesting.getWorldSpace(i, j + 1), Color.yellow, 100f, false);
                Debug.DrawLine(GridTesting.getWorldSpace(i + 1, j), GridTesting.getWorldSpace(i + 1, j + 1), Color.yellow, 100f, false);
                Debug.DrawLine(GridTesting.getWorldSpace(i, j + 1), GridTesting.getWorldSpace(i + 1, j + 1), Color.yellow, 100f, false);
            }
    }
}
