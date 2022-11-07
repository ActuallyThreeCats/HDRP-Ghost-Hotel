using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDateManager : MonoBehaviour
{
    private TimeManager timeManager;
    public int holidayID;
    [SerializeField] private List<KeyDateObject> dates = new List<KeyDateObject>();
    
    void Start()
    {
        timeManager = GetComponent<TimeManager>();
        timeManager.OnDateChangeOnly += TimeManager_OnDateChangeOnly;
    }

    private void TimeManager_OnDateChangeOnly(object sender, TimeManager.OnDateTimeChangedEventArgs e)
    {
        Debug.Log("DayChange " + e._month + " " + e._dayInMonth);
        for (int i = 0; i < dates.Count; i++)
        {
            if(e._month == dates[i].month && e._dayInMonth == dates[i].dayInMonthStart)
            {
                holidayID = dates[i].holdayID;

                Debug.Log(dates[i].eventName + "has begun!");
            }
        }
    }
}
