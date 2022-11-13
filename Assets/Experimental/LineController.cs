using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{

   [SerializeField] private LineRenderer lineRenderer;
    public List<Vector3> points = new List<Vector3>();

    private void Awake()
    {
    }

    public void SetUpLine(List<Vector3> points)
    {
        lineRenderer.positionCount = points.Count;
        this.points = points;
    }

    private void Update()
    {
        UpdateLines();
    }

    private void OnValidate()
    {
        UpdateLines();
    }
    public void UpdateLines()
    {
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }
    }
}
