using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycleController : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;

    private float timeInMinutes;
    private float minutes;
    private Quaternion currentSunPosition;
    private float sunRotation;
    [SerializeField] private Light sun;
    public bool isStartingGame;

    // Start is called before the first frame update
    void Start()
    {

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
        if (isStartingGame)
        {
            minutes = timeManager.GetTotalDayMinutes();
            timeManager.OnDateTimeChanged += TimeManager_OnDateTimeChanged;
            GetSunLocation();
            sun.transform.rotation = Quaternion.Euler(new Vector3(sunRotation, 0, 15.4f));
            currentSunPosition = sun.transform.rotation;
            isStartingGame = false;
        }
        if (!isStartingGame)
        {
            float timeElapsed = timeManager.GetCurrentTickTime();
            float time = timeManager.GetTimeBetweenTicks();
            Debug.Log(timeElapsed / time);
            sun.transform.rotation = Quaternion.Slerp(currentSunPosition, Quaternion.Euler(new Vector3(sunRotation, 0, 15.4f)), (timeElapsed / time));
        }

    }

    public void GetSunLocation()
    {
        timeInMinutes = minutes / 1440;
        currentSunPosition = sun.transform.rotation;
        Debug.Log(currentSunPosition + "Current Sun Position");
        sunRotation = Mathf.Lerp(-90, 270, timeInMinutes);
    }

}
