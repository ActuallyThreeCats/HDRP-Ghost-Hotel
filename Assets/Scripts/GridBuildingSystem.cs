using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridBuildingSystem : MonoBehaviour
{
    
    [SerializeField] private GridSprite gridSprite;
    public GridXZ<GridObject> grid;
    private FurnitureScriptableObject.Dir dir = FurnitureScriptableObject.Dir.Down;
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private float cellSize;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private LayerMask mouseColliderLayerMask;
    public BlueprintModeCameraController blueprintCam;

    [SerializeField] private FurnitureGhost furnitureGhost;
    private FurnitureScriptableObject furnitureObject;

    public event EventHandler OnSelectedChanged;

    private void Start()
    {
        CreateGrid();
        //inputManager.controls.Default.LeftClick.performed += LeftClick_performed;
        inputManager.controls.BlueprintMap.Place.performed += Place_performed;
        inputManager.controls.BlueprintMap.RotateItem.performed += RotateItem_performed;
        inputManager.controls.BlueprintMap.DeleteItem.performed += DeleteItem_performed;
        furnitureGhost = gameObject.GetComponent<FurnitureGhost>();
        furnitureObject = FurnitureIndex.Instance.furniturePrefabs[0];

    }

    private void DeleteItem_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        GridObject gridObject = grid.GetGridObject(GetMouseWorldPosition());
        PlacedObject placedObject = gridObject.GetPlacedObject();

        if(placedObject != null)
        {
            placedObject.DestroySelf();
            List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();

            foreach (Vector2Int gridPosition in gridPositionList)
            {
                grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
            }
        }
    }

    private void RotateItem_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(obj.ReadValue<float>() > 0)
        {
            dir = FurnitureScriptableObject.GetNextDirClockwise(dir);
        }else if(obj.ReadValue<float>() < 0)
        {
            dir = FurnitureScriptableObject.GetNextDirCounterClockwise(dir);
        }
    }

    private void Place_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (blueprintCam.isCurrentCamera)
        {
            grid.GetXZ(GetMouseWorldPosition(), out int x, out int z);
            if (x >= 0 && z >= 0 && x < grid.GetWidth() && z < grid.GetHeight())
            {
                List<Vector2Int> gridPositionList = furnitureObject.GetGridPositionList(new Vector2Int(x, z), dir);

                bool canBuild = true;
                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                    {
                        canBuild = false;
                        break;
                    }
                }

                GridObject gridObject = grid.GetGridObject(x, z);

                if (canBuild)
                {
                    Vector2Int rotationOffset = furnitureObject.GetRotationOffset(dir);
                    Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

                    PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(x, z), dir, furnitureObject);
                   // Transform builtTransform = Instantiate(furnitureObject.prefab, placedObjectWorldPosition, Quaternion.Euler(0, furnitureObject.GetRotationAngle(dir),0));

                    foreach (Vector2Int gridPosition in gridPositionList)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                    }

                }
                else
                {
                    Debug.Log("Cannot Build Here");
                }

            }
        }
    }

    private void OnValidate()
    {
        //CreateGrid();
        //gridSprite.EditorRun();
    }

    public void CreateGrid()
    {
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, gameObject.transform.position, (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
        Debug.Log(gameObject.transform.position);
        gridSprite.SetGrid(grid);
        FurnitureIndex.Instance.grids.Add(grid);
    }


    // call when chosen placed object changes
    private void RefreshSelectedObjectType()
    {
        OnSelectedChanged?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMouseWorldSnappedPosition()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        grid.GetXZ(mousePosition, out int x, out int z);

        if (furnitureObject != null)
        {
            Vector2Int rotationOffset = furnitureObject.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
            return placedObjectWorldPosition;
        }
        else
        {
            return mousePosition;
        }
    }

    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {
            //Debug.Log(raycastHit.point);
            return raycastHit.point;
        }
        else
        {
            //Debug.Log("0");
            return Vector3.zero;
        }
    }

    public Quaternion GetPlacedObjectRotation()
    {
        if(furnitureObject != null)
        {
            return Quaternion.Euler(0, furnitureObject.GetRotationAngle(dir), 0);
        }
        else
        {
            return Quaternion.identity;
        }
    }

    public FurnitureScriptableObject GetFurnitureScriptableObject()
    {
        return furnitureObject;
    }
}
public class GridObject
{
    private GridXZ<GridObject> grid;
    private int x;
    private int z;
    private PlacedObject placedObject;
    

    public GridObject(GridXZ<GridObject> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }

    public void SetPlacedObject(PlacedObject placedObject)
    {
        this.placedObject = placedObject;
        grid.TriggerGridObjectChanged(x, z);
    }
    public PlacedObject GetPlacedObject()
    {
        return placedObject;
    }
    public void ClearPlacedObject()
    {
        placedObject = null;
    }

    public bool CanBuild()
    {
        return placedObject == null;
    }



}