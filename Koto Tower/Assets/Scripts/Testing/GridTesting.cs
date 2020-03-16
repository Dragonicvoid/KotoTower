using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTesting
{
    //This class is for all the grid not just a grid
    public static int width;
    public static int height;
    public static CellTesting[,] cells;
    public static int offsetX, offsetY;
    public static float cellSize;

    //contructor for grid
    public GridTesting(int width, int height, float cellSize, int offsetX)
    {
        GridTesting.width = width;
        GridTesting.height = height;
        GridTesting.cellSize = cellSize;
        GridTesting.offsetX = offsetX;
        GridTesting.offsetY = height / 2;

        GridTesting.cells = new CellTesting[width, height];
        initializeValue();

        // Drawing the line for grid (debug purposes)
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                Debug.DrawLine(getWorldSpace(i, j), getWorldSpace(i + 1, j) , Color.green, 100f, false);
                Debug.DrawLine(getWorldSpace(i, j), getWorldSpace(i, j + 1), Color.green, 100f, false);
            }
        }

        Debug.DrawLine(getWorldSpace(0, GridTesting.height), getWorldSpace(GridTesting.width, GridTesting.height), Color.green, 100f, false);
        Debug.DrawLine(getWorldSpace(GridTesting.width, 0), getWorldSpace(GridTesting.width, GridTesting.height), Color.green, 100f, false);
    }

    private void initializeValue()
    {
        for (int i = 0; i < GridTesting.width; i++)
            for (int j = 0; j < GridTesting.height; j++)
                GridTesting.cells[i, j] = new CellTesting();
    }

    // get world space from grid index
    public static Vector3 getWorldSpace(int x, int y)
    {
        return new Vector3(x - GridTesting.offsetX, y - GridTesting.offsetY) * GridTesting.cellSize;
    }

    // from touches position, and return grid position
    public static void getXYFromPosition(Vector3 positionLocation, out int x, out int y)
    {
        x = Mathf.FloorToInt(positionLocation.x / cellSize);
        y = Mathf.FloorToInt(positionLocation.y / cellSize);
    }
}
