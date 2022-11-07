using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Days
{
    Sunday = 1,
    Monday = 2,
    Tuesday = 3,
    Wednesday = 4,
    Thursday = 5,
    Friday = 6,
    Saturday = 7
}

public class TimeManager : MonoBehaviour
{
    [Header("Time & Date")]
    [Range(0,99)]
    [SerializeField] private int year;
    [Range(1,12)]
    [SerializeField] private int month;
    [Range(1,28)]
    [SerializeField] private int dayInMonth;
    [Range(1,7)]
    [SerializeField] private int dayInWeek;
    [Range(0,24)]
    [SerializeField] private int hour;
    [Range(1, 60)]
    [SerializeField] private int minute;

    [Header("A.M. & P.M.")]
    [SerializeField] private bool twelveHourClock;
    [SerializeField] private bool isPM;
    [SerializeField] private int displayHour;

    [Header("Advancing Time Control")]
    [SerializeField] private float timeBetweenTicks;
    [SerializeField] private float currentTickTime;
    [SerializeField] private int advanceMinutesBy;
    [SerializeField] private int maxMonths;  
    [SerializeField] private int maxDays = 28;  
    [SerializeField] private int totalDays;
    [SerializeField] private int totalWeeks;  


    public event EventHandler<OnDateTimeChangedEventArgs> OnDateTimeChanged;
    public event EventHandler<OnDateTimeChangedEventArgs> OnDateChangeOnly;
    

    public class OnDateTimeChangedEventArgs : EventArgs
    {
        public int _year, _month, _dayInMonth, _dayInWeek, _hour, _minute, _displayHour;
        public bool _isPM, _twelveHourclock;
    }



    public bool gettingTime;

    // Start is called before the first frame update
    void Start()
    {
        Tick();
    }

    // Update is called once per frame
    void Update()
    {
        currentTickTime += Time.deltaTime;
        if (currentTickTime >= timeBetweenTicks)
        {
            currentTickTime = 0;
            Tick();
        }

        //for debuggin purposes, to remove later
        if (gettingTime)
        {
            GetTime();
            gettingTime = false;
        }
    }

    private void Tick()
    {
        AdvanceTime();
        FireEvent();
    }

    private void FireEvent()
    {
        OnDateTimeChanged?.Invoke(this, new OnDateTimeChangedEventArgs
        {
            _year = year,
            _month = month,
            _dayInMonth = dayInMonth,
            _dayInWeek = dayInWeek,
            _hour = hour,
            _minute = minute,
            _displayHour = displayHour,
            _isPM = isPM,
            _twelveHourclock = twelveHourClock
        }) ;
    }

    private void AdvanceTime()
    {
        minute += advanceMinutesBy;
        if(minute >= 59)
        {
            minute = 0;
            hour++;
            if(twelveHourClock && hour == 1)
            {
                displayHour = 1;
            }
            else 
            { 
                displayHour++; 
            }
           
            
        }
        if(hour >= 24)
        {
            NewDay();
           
        }

        if (hour > 11 && !isPM)
        {
            isPM = true;
        }
        else if (hour < 11 && isPM)
        {
            isPM = false;
        }
        if (twelveHourClock && hour > 12)
        {
            displayHour = hour - 12;
        }
        if (twelveHourClock && hour == 0)
        {
            displayHour = 12;
        }
    }

    public void NewDay()
    {
        hour = 0;
        totalDays++;
        dayInWeek++;
        dayInMonth++;
        displayHour = 0;

        if (dayInWeek > 7)
        {
            dayInWeek = 1;
        }
        if (dayInMonth > maxDays)
        {
            dayInMonth = 1;
            month++;
        }
        if (month > maxMonths)
        {
            month = 1;
            year++;
        }
        

        OnDateChangeOnly?.Invoke(this, new OnDateTimeChangedEventArgs
        {
            _year = year,
            _month = month,
            _dayInMonth = dayInMonth,
            _dayInWeek = dayInWeek,
            _hour = hour,
            _minute = minute,
            _displayHour = displayHour,
            _isPM = isPM,
            _twelveHourclock = twelveHourClock
        });
    }

    public void SetTimeInDays(int days)
    {
        totalDays = days;
        year = settingYear(days);
        month = settingMonth(days);
        dayInMonth = settingDay(days);
        FireEvent();
    }

    int settingYear(int d)
    {
       
        return (int)Mathf.Floor(d / (maxMonths * maxDays)) + 1;
    }

    int settingMonth(int d)
    {
        
        return (int)Mathf.Floor(((d % (maxMonths*maxDays)) / maxDays)) + 1;
    }

    int settingDay(int d)
    {
        return (int)Mathf.Floor(d % maxDays) + 1;

    }

    public int GetYear() {
        return year;
    }
    public int GetMonth()
    {
        return month;
    }
    public int GetDayOfWeek()
    {
        return dayInWeek;
    }
    public int GetDayOfMonth()
    {
        return dayInMonth;
    }
    public int GetMinutes()
    {
        return minute;
    }

    public void GetTime()
    {
        string fullTimeDisplay = (Days)dayInWeek + " the " + dayInMonth + " of month " + month + " in year " + year;
        Debug.Log(fullTimeDisplay);
    }

    public void GetTimeInMinutes()
    {
        GetMinutes();   
    }
}
