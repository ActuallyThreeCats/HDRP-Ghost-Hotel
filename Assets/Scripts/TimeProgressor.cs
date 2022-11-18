using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System;
using Sirenix.OdinInspector;

public enum Days
{
    Sunday = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6
}

public enum Months
{
    Spring = 0,
    Summer = 1,
    Fall = 2,
    Winter = 3,
}


public class TimeProgressor : MonoBehaviour
{

    [BoxGroup("Controls")]
    [Range(0,24)]
    public float timeOfDay;
    [BoxGroup("Controls")]
    public Days day;
    [BoxGroup("Controls")]
    [EnumToggleButtons]
    public Months month;
 
    [TabGroup("Time")]
    public bool isPM;
    [TabGroup("Time")]
    public int date = 1;
    [TabGroup("Time")]
    public int dayOfWeek;
    [TabGroup("Time")]
    public float hour;
    [TabGroup("Time")]
    public int hrPM;
    [TabGroup("Time")]
    public int hr;
    [TabGroup("Time")]
    public int min;
    [TabGroup("Time")]
    public int year =1;

    [TabGroup("Settings")]
    public int monthLength;
    [TabGroup("Settings")]
    public int daysInWeek = 7;
    [TabGroup("Settings")]
    public float dayLength = 24;
    [TabGroup("Settings")]
    public int monthInt;
    [TabGroup("Settings")]
    public bool twelveHourClock;
    [TabGroup("Settings")]
    public float OrbitSpeed = 1.0f;
    public Light sun;
    public Light moon;
    public Volume skyVolume;
    public AnimationCurve starsCurve;
    public AnimationCurve sunCurve;

    public bool isNight;
    private PhysicallyBasedSky sky;


    public event EventHandler<OnTimeChangedEventArgs> OnTimeChanged;
    public event EventHandler<OnTimeChangedEventArgs> OnDayChanged;

    public class OnTimeChangedEventArgs : EventArgs
    {
        public float _timeOfDay;
    }

    private void Start()
    {
        skyVolume.profile.TryGet(out sky);
    }
    private void Update()
    {
        timeOfDay += Time.deltaTime * OrbitSpeed;
        

        UpdateTime();
    }

    private void OnValidate()
    {
        skyVolume.profile.TryGet(out sky);
        dayOfWeek = (int)day;
        UpdateTime();
    }

    private void UpdateTime()
    {
        if (timeOfDay > dayLength)
        {
            timeOfDay = timeOfDay % dayLength;
            date++;
            dayOfWeek++;
            if (date > monthLength)
            {

                date = 1;
                monthInt++;
                
                if (monthInt >= 4)
                {
                    monthInt = 0;
                    year++;
                }
                month = (Months)monthInt;

            }
            if (dayOfWeek > daysInWeek-1)
            {
                dayOfWeek = 0;
            }
            day = (Days)dayOfWeek;

            OnDayChanged?.Invoke(this, new OnTimeChangedEventArgs
            {
                _timeOfDay = timeOfDay
            });
        }

        
        float alpha = timeOfDay / dayLength;
        float sunRotation = Mathf.Lerp(-90, 270, alpha);
        float moonRotation = sunRotation - 180;
        sun.transform.rotation = Quaternion.Euler(sunRotation, 0, 15.4f);
        moon.transform.rotation = Quaternion.Euler(moonRotation, 0, 15.4f);
        CheckNightDayTransition();

        
        sun.intensity = sunCurve.Evaluate(alpha);

        sky.spaceEmissionMultiplier.value = starsCurve.Evaluate(alpha) * 8;
        hr = (int)Mathf.Floor(timeOfDay / (dayLength / 24));
        min = (int)Mathf.Floor((timeOfDay / (dayLength / 1440)) % 60);
        hour = timeOfDay / (dayLength / 24);
        if (twelveHourClock)
        {
            if (hr > 12)
            {
                hrPM = hr - 12;
            }
            if (hr <= 12)
            {
                hrPM = hr;
            }
            if (hr == 0 && hr < 1)
            {
                hrPM = 12;
            }
        }



        if (hr >= 12)
        {
            isPM = true;

        }
        if (hr < 12)
        {
            isPM = false;
        }

    }

    private void CheckNightDayTransition()
    {
        if (isNight)
        {
            if(moon.transform.rotation.eulerAngles.x > 180)
            {
                StartDay();
            }
        }
        else
        {
            if(sun.transform.rotation.eulerAngles.x > 180)
            {
                StartNight();
            }
        }
    }

    private void StartDay()
    {
        isNight = false;
        sun.shadows = LightShadows.Soft;
        moon.shadows = LightShadows.None;
    }

    private void StartNight()
    {
        isNight = true;
        sun.shadows = LightShadows.None;
        moon.shadows = LightShadows.Soft;
    }

    private void FireEvent()
    {
        OnTimeChanged?.Invoke(this, new OnTimeChangedEventArgs
        {
            _timeOfDay = timeOfDay
        });
    }
}
