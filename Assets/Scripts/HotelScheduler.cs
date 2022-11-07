using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotelScheduler : MonoBehaviour
{
    public static HotelScheduler Instance;
    [Header("Check Out Time")]
    [Range(0, 23)]
    public int checkOutHour;
    [Range(0, 59)]
    public int checkOutMinute;
    [Space]

    [Header("Check In Start Time")]
    [Range(0, 23)]
    public int checkInStartHour;
    [Range(0,59)]
    public int checkInStartMinute;
    [Space]

    [Header("Check In End Time")]
    [Range(0, 23)]
    public int checkInEndHour;
    [Range(0, 59)]
    public int checkInEndMinute;
    [Space]

    [Header("Breakfast Start Time")]
    [Range(0, 23)]
    public int breakfastStartHour;
    [Range(0, 59)]
    public int breakfastStartMinute;
    [Space]

    [Header("Breakfast End Time")]
    [Range(0, 23)]
    public int breakfastEndHour;
    [Range(0, 59)]
    public int breakfastEndMinute;





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
