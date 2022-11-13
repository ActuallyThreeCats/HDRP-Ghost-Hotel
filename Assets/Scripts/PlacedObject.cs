using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, FurnitureScriptableObject.Dir dir, FurnitureScriptableObject furnitureObject)
    {
        Transform placedObjectTransform = Instantiate(furnitureObject.prefab, worldPosition, Quaternion.Euler(0, furnitureObject.GetRotationAngle(dir), 0));

        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();

        placedObject.furnitureObject = furnitureObject;
        placedObject.origin = origin;
        placedObject.dir = dir;

        return placedObject;
    }

    private FurnitureScriptableObject furnitureObject;
    private Vector2Int origin;
    private FurnitureScriptableObject.Dir dir;

    public List<Vector2Int> GetGridPositionList()
    {
        return furnitureObject.GetGridPositionList(origin, dir);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
