using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RoomEnum
{
    occupied, //click to show info
    reserved,
    ghostHaunting,
    unknownThreat,
    goodMatch,
    badMatch
}

[System.Serializable]
public class RoomInfo : MonoBehaviour
{


    [SerializeField] private TimeProgressor timeProgressor;
    public int roomNumber; //make getter/setter for this later
    [SerializeField] public bool occupied;
    [SerializeField] private int cost;
    [SerializeField] private List<Transform> Waypoints;
    public GameObject Patron;
    [SerializeField] private int daysScheduled;
    [SerializeField] private int daysRemaining;
    [SerializeField] private int maxDays = 4;
    public Button checkInButton; // make getter/setter for this later
    public RoomEnum roomState;


    private void Start()
    {
        timeProgressor = GameObject.Find("TimeManager").GetComponent<TimeProgressor>();

        timeProgressor.OnDayChanged += TimeProgressor_OnDayChanged;
    }

    private void TimeProgressor_OnDayChanged(object sender, TimeProgressor.OnTimeChangedEventArgs e)
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
    public void ResetDaysRemaining()
    {
        daysRemaining = daysScheduled;
    }

    public void SetDaysScheduled(int amt)
    {
        if(amt <= maxDays)
        {
            daysScheduled = amt;
            daysRemaining = amt;
        }
        else
        {
            Debug.LogError("Not enough maxdays");
        }
    }

}
