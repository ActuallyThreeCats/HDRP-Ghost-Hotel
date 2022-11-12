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
    [SerializeField] private InputManager inputManager;
    [SerializeField] private LayerMask mouseColliderLayerMask;
    [SerializeField] private BlueprintModeCameraController blueprintCam;




    private void Start()
    {
        CreateGrid();
        //inputManager.controls.Default.LeftClick.performed += LeftClick_performed;
        inputManager.controls.BlueprintMap.Place.performed += Place_performed;

    }

    private void Place_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (blueprintCam.isCurrentCamera)
        {
            grid.GetXZ(GetMouseWorldPosition(), out int x, out int z);
            if (x >= 0 && z >= 0 && x < grid.GetWidth() && z < grid.GetHeight())
            {
                
            Instantiate(FurnitureIndex.Instance.furniturePrefabs[0], grid.GetWorldPosition(x,z), Quaternion.identity);

            }
        }
    }

    //private void LeftClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    //{
    //    throw new NotImplementedException();
    //}

    private void OnValidate()
    {
        //CreateGrid();
        //gridSprite.EditorRun();
    }

    public void CreateGrid()
    {
        //for (int i = 0; i < lines.Count; i++)
        //{
        //    GameObject.DestroyImmediate(lines[i]);
        //}
        //    lines.Clear();
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, gameObject.transform.position, (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
        Debug.Log(gameObject.transform.position);
        gridSprite.SetGrid(grid);
        FurnitureIndex.Instance.grids.Add(grid);



    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {
            //Debug.Log(raycastHit.point);
            return raycastHit.point;
        }
        else
        {
            Debug.Log("0");
            return Vector3.zero;
        }
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