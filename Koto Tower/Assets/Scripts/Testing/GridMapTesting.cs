using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMapTesting : MonoBehaviour
{
    [SerializeField] int width = 30, height = 14, offsetX = 9;
    [SerializeField] float cellSize = 1f;
    Camera mainCamera;
    GridTesting grid;

    // find main camera with tag
    private void Awake()
    {
        mainCamera = Camera.main;
        grid = new GridTesting(width, height, cellSize, offsetX);
    }

    // get input touches
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // For debug using mouse click instead touches
            Vector3 touchLocation = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            int x, y;
            GridTesting.getXYFromPosition(touchLocation, out x, out y);
        }
        
    }
}
