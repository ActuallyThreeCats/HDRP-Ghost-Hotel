using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RoomInfo : MonoBehaviour
{
    private TimeManager timeManager;
    [SerializeField] private int roomNumber;
    [SerializeField] public bool occupied;
    [SerializeField] private int cost;
    [SerializeField] private List<Transform> Waypoints;
    [SerializeField] private GameObject Patron;
    [SerializeField] private int daysScheduled;
    [SerializeField] private int daysRemaining;
    [SerializeField] private int maxDays = 4;

    private void Start()
    {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        timeManager.OnDateChangeOnly += TimeManager_OnDateChangeOnly;
    }

    private void TimeManager_OnDateChangeOnly(object sender, TimeManager.OnDateTimeChangedEventArgs e)
    {
        daysRemaining--;
 
    }

    public List<Transform> GetWayPoints()
    {
        return Waypoints;
    }

    public int GetRoomCost()
    {
        return cost;
    }
    public int GetMaxDays()
    {
        return maxDays;
    }

    public int GetDaysRemaining()
    {
        return daysRemaining;
    }
    public int GetDaysScheduled()
    {
        return daysScheduled;
    }

    public void SetDaysScheduled(int amt)
    {
        if(amt <= maxDays)
        {
            daysScheduled = amt;
            daysRemaining = amt;
        }
    }

}
