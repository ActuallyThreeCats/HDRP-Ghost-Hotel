using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private GridSprite gridSprite;
    private GridXZ<GridObject> grid;
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private float cellSize;

    
    private void Start()
    {
        CreateGrid();
    }


    private void OnValidate()
    {
        CreateGrid();
    }

    public void CreateGrid()
    {
        //for (int i = 0; i < lines.Count; i++)
        //{
        //    GameObject.DestroyImmediate(lines[i]);
        //}
        //    lines.Clear();
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, gameObject.transform.position, (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
        gridSprite.SetGrid(grid);



    }

}
public class GridObject
{
    private GridXZ<GridObject> grid;
    private int x;
    private int z;

    private GameObject tile;

    public GridObject(GridXZ<GridObject> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }

}