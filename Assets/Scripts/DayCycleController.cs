using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class DayCycleController : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;

    private float timeInMinutes;
    private float minutes;
    private Quaternion currentSunPosition;
    private float sunRotation;
    [SerializeField] private Light sun;
    [SerializeField] private Light moon;
    [SerializeField] private AnimationCurve starsCurve;
    public bool isStartingGame;
    public bool isStartingNight;
    public bool isStartingDay;
    private bool hasStartedDay;
    private bool hasStartedNight;

    [SerializeField] private Volume skyVolume;
    private PhysicallyBasedSky sky;

    private bool isNight;
    // Start is called before the first frame update
    void Start()
    {
        skyVolume.profile.TryGet(out sky);
        isStartingGame = true;
    }


    private void TimeManager_OnDateTimeChanged(object sender, TimeManager.OnDateTimeChangedEventArgs e)
    {
        minutes = timeManager.GetTotalDayMinutes();

        GetSunLocation();
    }

    // Update is called once per frame
    void Update()
    {
        float timeElapsed = timeManager.GetCurrentTickTime();
        float time = timeManager.GetTimeBetweenTicks();
        UpdateTime();

        if (isStartingDay)
        {
            sun.intensity = Mathf.Lerp(0, 15, timeElapsed/time);
            if(sun.intensity == 15)
            {
                isStartingDay = false;
            }
        }
        if (isStartingNight)
        {
            moon.intensity = Mathf.Lerp(0, 2.25f, timeElapsed / time);
            if(moon.intensity == 2.25f)
            {
                isStartingNight = false;
            }
        }

    }
    public void UpdateTime()
    {
        minutes = timeManager.GetTotalDayMinutes();

        if (isStartingGame)
        {
            minutes = timeManager.GetTotalDayMinutes();
            timeManager.OnDateTimeChanged += TimeManager_OnDateTimeChanged;
            GetSunLocation();
            sun.transform.rotation = Quaternion.Euler(new Vector3(sunRotation, 0, 15.4f));
            moon.transform.rotation = Quaternion.Euler(new Vector3(sunRotation - 180, 0, 15.4f));
            //currentSunPosition = sun.transform.rotation;

            isStartingGame = false;
        }
        if (!isStartingGame)
        {
            float timeElapsed = timeManager.GetCurrentTickTime();
            float time = timeManager.GetTimeBetweenTicks();
            //Debug.Log(timeElapsed / time);
            float moonRotation = sunRotation - 180;
            sun.transform.rotation = Quaternion.Slerp(currentSunPosition, Quaternion.Euler(new Vector3(sunRotation, 0, 15.4f)), (timeElapsed / time));
            Vector3 moonPos = new Vector3(sun.transform.rotation.eulerAngles.x - 180, 0, 15.4f);
            moon.transform.rotation = Quaternion.Euler(moonPos);
            sky.spaceEmissionMultiplier.value = starsCurve.Evaluate(timeInMinutes) * 8;
        }
        CheckNightDayTransition();
    }

    public void GetSunLocation()
    {
        timeInMinutes = minutes / 1440;
        currentSunPosition = sun.transform.rotation;
        //Debug.Log(currentSunPosition + "Current Sun Position");
        sunRotation = Mathf.Lerp(-90, 270, timeInMinutes);
    }

    private void CheckNightDayTransition()
    {
        if (isNight)
        {
            if(moon.transform.rotation.eulerAngles.x > 180 && !hasStartedDay)
            {
                StartDay();
                hasStartedDay = true;
                hasStartedNight = false;

            }
        }
        else
        {
            if(sun.transform.rotation.eulerAngles.x > 180 && !hasStartedNight)
            {
                StartNight();
                hasStartedDay = false;
                hasStartedNight = true;
            }
        }
    }

    private void StartDay()
    {
        isNight = false;
        isStartingDay = true;
        sun.shadows = LightShadows.Soft;
        moon.shadows = LightShadows.None;

        
        //moon.enabled = false;
        //sun.enabled = true;
    }

    private void StartNight()
    {
        isNight = true;
        isStartingNight = true;
        sun.shadows = LightShadows.None;
        moon.shadows = LightShadows.Soft;
        //moon.enabled = true;
        //sun.enabled = false;

    }

}
