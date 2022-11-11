using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridSprite : MonoBehaviour
{
    private GridXZ<GridObject> grid;
    private Mesh mesh;


    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }
    public void SetGrid(GridXZ<GridObject> grid)
    {
        this.grid = grid;
        UpdateTileVisual();
    }

    private void OnValidate()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        UpdateTileVisual();

    }

    private void UpdateTileVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uvs, out int[] triangles);

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int z = 0; z < grid.GetHeight(); z++)
            {
                int index = x * grid.GetHeight() + z;

                Vector3 quadSize = new Vector3(1,0, 1) * grid.GetCellSize();


                MeshUtils.AddToMeshArrays(vertices, uvs, triangles, index, grid.GetWorldPosition(x, z) + quadSize * .5f, 0, quadSize, new Vector2(0,0), new Vector2(1,1));
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

    }


    public static void CreateEmptyMeshArrays(int quadCount, out Vector3[] vertices, out Vector2[] uvs, out int[] triangles)
    {
        vertices = new Vector3[4 * quadCount];
        uvs = new Vector2[4 * quadCount];
        triangles = new int[6 * quadCount];
    }


}
