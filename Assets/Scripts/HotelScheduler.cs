using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotelScheduler : MonoBehaviour
{

    public static HotelScheduler Instance;
    [SerializeField] private TimeProgressor timeProgressor;
    
    [Header("Check Out Time")]
    [Range(0, 24)]
    public float checkOutTime;
    public float checkOutHour;
    public float checkOutMinute;
    public float checkOutHourPM;
    [Space]

    [Header("Check In Start Time")]
    [Range(0, 24)]
    public float checkInTime;
    public float checkInHour;
    public float checkInMinute;
    public float checkInHourPM;
    [Space]

    [Header("Check In End Time")]
    [Range(0, 24)]
    public float checkInEndTime;
    public float checkInEndHour;
    public float checkInEndMinute;
    public float checkInEndHourPM;
    [Space]

    [Header("Breakfast Start Time")]
    [Range(0, 24)]
    public float breakfastTime;
    public float breakfastHour;
    public float breakfastMinute;
    public float breakfastHourPM;
    [Space]

    [Header("Breakfast End Time")]
    [Range(0, 24)]
    public float breakfastEndTime;
    public float breakfastEndHour;
    public float breakfastEndMinute;
    public float breakfastEndHourPM;






    private void Awake()
    {
        Instance = this;
    }

    private void OnValidate()
    {
        //checkout update
        checkOutHour = (int)Mathf.Floor(checkOutTime / (timeProgressor.dayLength / 24));
        checkOutMinute = (int)Mathf.Floor((checkOutTime / (timeProgressor.dayLength / 1440)) % 60);
        if (checkOutHour > 12)
        {
            checkOutHourPM = checkOutHour - 12;
        }
        if (checkOutHour <= 12)
        {
            checkOutHourPM= checkOutHour;
        }

        //check in update
        checkInHour = (int)Mathf.Floor(checkInTime / (timeProgressor.dayLength / 24));
        checkInMinute = (int)Mathf.Floor((checkInTime / (timeProgressor.dayLength / 1440)) % 60);
        if (checkInHour > 12)
        {
            checkInHourPM = checkInHour - 12;
        }
        if (checkInHour <= 12)
        {
            checkInHourPM = checkInHour;
        }

        //check in End update
        checkInEndHour = (int)Mathf.Floor(checkInEndTime / (timeProgressor.dayLength / 24));
        checkInEndMinute = (int)Mathf.Floor((checkInEndTime / (timeProgressor.dayLength / 1440)) % 60);
        if (checkInEndHour > 12)
        {
            checkInEndHourPM = checkInEndHour - 12;
        }
        if (checkInEndHour <= 12)
        {
            checkInEndHourPM = checkInEndHour;
        }

        //breakfast start update
        breakfastHour = (int)Mathf.Floor(breakfastTime / (timeProgressor.dayLength / 24));
        breakfastMinute = (int)Mathf.Floor((breakfastTime / (timeProgressor.dayLength / 1440)) % 60);
        if (breakfastHour > 12)
        {
            breakfastHourPM = breakfastHour - 12;
        }
        if (breakfastHour <= 12)
        {
            breakfastHourPM = breakfastHour;
        }

        //breakfast end update
        breakfastEndHour = (int)Mathf.Floor(breakfastEndTime / (timeProgressor.dayLength / 24));
        breakfastEndMinute = (int)Mathf.Floor((breakfastEndTime / (timeProgressor.dayLength / 1440)) % 60);
        if (breakfastEndHour > 12)
        {
            breakfastEndHourPM = breakfastEndHour - 12;
        }
        if (breakfastEndHour <= 12)
        {
            breakfastEndHourPM = breakfastEndHour;
        }
    }
    
    private void ValidateTimes(float hour, float minutes)
    {

    }
}
