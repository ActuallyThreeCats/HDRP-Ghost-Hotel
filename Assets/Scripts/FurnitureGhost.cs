using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FurnitureGhost : MonoBehaviour
{
    [SerializeField] private Transform visual;
    private FurnitureScriptableObject furnitureObject;
    [SerializeField] private GridBuildingSystem gridBuildingSystem;
    
    private void Start()
    {
        gridBuildingSystem = gameObject.GetComponentInParent<GridBuildingSystem>();
        gridBuildingSystem.OnSelectedChanged += GridBuildingSystem_OnSelectedChanged;
        
    }

    private void OnDisable()
    {
        gridBuildingSystem.OnSelectedChanged -= GridBuildingSystem_OnSelectedChanged;
    }

    private void GridBuildingSystem_OnSelectedChanged(object sender, EventArgs e)
    {
        RefreshVisual();
    }

    private void Update()
    {
        if (gridBuildingSystem.blueprintCam.isOpeningBlueprintMode)
        {
            RefreshVisual();
        }
        gridBuildingSystem.grid.GetXZ(gridBuildingSystem.GetMouseWorldPosition(), out int x, out int z);
        if (x >= 0 && z >= 0 && x < gridBuildingSystem.grid.GetWidth() && z < gridBuildingSystem.grid.GetHeight() && gridBuildingSystem.blueprintCam.isCurrentCamera)
        {
           if(visual == null)
            {
                RefreshVisual();
            }
        }
        else
        {
            if (visual != null)
            {
                Debug.Log("Destroying Visual");
                Destroy(visual.gameObject);

            }
        }
    }
    private void LateUpdate()
    {


        if (gridBuildingSystem.blueprintCam.isCurrentCamera)
        {
            Vector3 targetPosition = gridBuildingSystem.GetMouseWorldSnappedPosition();
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);
            transform.rotation = Quaternion.Lerp(transform.rotation, gridBuildingSystem.GetPlacedObjectRotation(), Time.deltaTime * 15f);

        }
    }

    private void UpdateRefresh()
    {
        visual.parent = transform;
        visual.localPosition = Vector3.zero;
        visual.localEulerAngles = Vector3.zero;
    }
    public void RefreshVisual()
    {
        if(visual != null)
        {
            Destroy(visual.gameObject);
            visual = null;
        }

        FurnitureScriptableObject furnitureScriptableObject = gridBuildingSystem.GetFurnitureScriptableObject();




        if(furnitureScriptableObject != null)
        {
            //Debug.Log("Test Null");
            gridBuildingSystem.grid.GetXZ(gridBuildingSystem.GetMouseWorldPosition(), out int x, out int z);
            if (x >= 0 && z >= 0 && x < gridBuildingSystem.grid.GetWidth() && z < gridBuildingSystem.grid.GetHeight())
            {
                //Debug.Log("InGrid");
                if(visual == null)
                {
                    visual = Instantiate(furnitureScriptableObject.visual.transform, gridBuildingSystem.GetMouseWorldPosition(), Quaternion.identity);
                    UpdateRefresh();

                }
                else
                {
                    UpdateRefresh();
                }
            }
            //else
            //{
            //    if (visual != null)
            //    {
            //        Debug.Log("Destroying Visual");
            //        Destroy(visual);

            //    }
            //}

        }
        else
        {
            Debug.Log("Null object");
        }
    }
}
