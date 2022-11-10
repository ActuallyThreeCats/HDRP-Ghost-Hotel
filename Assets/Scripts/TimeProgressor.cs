using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System;

public class TimeProgressor : MonoBehaviour
{
    [Range(0,24)]
    public float timeOfDay;
    public float hour;
    public float dayLength = 24;
    public int hr;
    public int min;
    public bool twelveHourClock;
    public bool isPM;
    public int hrPM;


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
        
        if (timeOfDay > dayLength)
        {
            // timeOfDay = 0;
            timeOfDay = timeOfDay % dayLength;
            Debug.Log(timeOfDay);
            OnDayChanged?.Invoke(this, new OnTimeChangedEventArgs
            {
                _timeOfDay = timeOfDay
            });
        }
        UpdateTime();
    }

    private void OnValidate()
    {
        skyVolume.profile.TryGet(out sky);

        UpdateTime();
    }

    private void UpdateTime()
    {
        //float alpha = timeOfDay / 24.0f;
        float alpha = timeOfDay / dayLength;
        float sunRotation = Mathf.Lerp(-90, 270, alpha);
        float moonRotation = sunRotation - 180;
        sun.transform.rotation = Quaternion.Euler(sunRotation, 0, 15.4f);
        moon.transform.rotation = Quaternion.Euler(moonRotation, 0, 15.4f);
        CheckNightDayTransition();

        //sun.intensity = ((float)((Mathf.Sin(2 * Mathf.PI * timeOfDay) + (Mathf.Sin(6 * Mathf.PI * timeOfDay) / 9) * 0.56) + 0.5));
        sun.intensity = sunCurve.Evaluate(alpha);

        sky.spaceEmissionMultiplier.value = starsCurve.Evaluate(alpha) * 8;
        hr = (int)Mathf.Floor(timeOfDay / (dayLength / 24));
        min = (int)Mathf.Floor((timeOfDay / (dayLength / 1440)) % 60);
        hour = timeOfDay / (dayLength/24);
        if (twelveHourClock)
        {
            if (hr > 12)
            {
                hrPM = hr - 12;
            }
            if(hr <= 12)
            {
                hrPM = hr;
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
