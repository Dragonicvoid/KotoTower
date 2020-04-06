using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    [SerializeField] int width = 30, height = 14, offsetX = 9;
    [SerializeField] float cellSize = 1f;
    Camera mainCamera;
    GridCustom grid;

    // find main camera with tag
    private void Awake()
    {
        mainCamera = Camera.main;
        grid = new GridCustom(width, height, cellSize, offsetX);
    }
}
