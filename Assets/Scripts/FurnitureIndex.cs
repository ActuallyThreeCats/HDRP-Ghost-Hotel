using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureIndex : MonoBehaviour
{
    public static FurnitureIndex Instance;

    public List<FurnitureScriptableObject> furniturePrefabs = new List<FurnitureScriptableObject>();

    [SerializeField]
    public List<GridXZ<GridObject>> grids = new List<GridXZ<GridObject>>();

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
