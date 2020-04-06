using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCustom
{
    //This class is for all the grid not just a grid
    public static int width;
    public static int height;
    public static Cell[,] cells;
    public static int offsetX, offsetY;
    public static float cellSize;

    //contructor for grid
    public GridCustom(int width, int height, float cellSize, int offsetX)
    {
        GridCustom.width = width;
        GridCustom.height = height;
        GridCustom.cellSize = cellSize;
        GridCustom.offsetX = offsetX;
        GridCustom.offsetY = height / 2;

        GridCustom.cells = new Cell[width, height];
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

        Debug.DrawLine(getWorldSpace(0, GridCustom.height), getWorldSpace(GridCustom.width, GridCustom.height), Color.green, 100f, false);
        Debug.DrawLine(getWorldSpace(GridCustom.width, 0), getWorldSpace(GridCustom.width, GridCustom.height), Color.green, 100f, false);
    }

    private void initializeValue()
    {
        for (int i = 0; i < GridCustom.width; i++)
            for (int j = 0; j < GridCustom.height; j++)
                GridCustom.cells[i, j] = new Cell();
    }

    // get world space from grid index
    public static Vector3 getWorldSpace(int x, int y)
    {
        return new Vector3(x - GridCustom.offsetX, y - GridCustom.offsetY) * GridCustom.cellSize;
    }

    // from touches position, and return grid position
    public static void getXYFromPosition(Vector3 positionLocation, out int x, out int y)
    {
        x = Mathf.FloorToInt(positionLocation.x / cellSize);
        y = Mathf.FloorToInt(positionLocation.y / cellSize);
    }
}
