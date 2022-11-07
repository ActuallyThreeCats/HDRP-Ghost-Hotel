using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RoomInfo : MonoBehaviour
{
    [SerializeField] private int roomNumber;
    [SerializeField] public bool occupied;
    [SerializeField] private int cost;
    [SerializeField] private List<Transform> Waypoints;
    [SerializeField] private GameObject Patron;
    [SerializeField] private int daysScheduled;
    [SerializeField] private int daysRemaining;

    public List<Transform> GetWayPoints()
    {
        return Waypoints;
    }

}
