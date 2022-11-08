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

        UpdateTime();
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

}
