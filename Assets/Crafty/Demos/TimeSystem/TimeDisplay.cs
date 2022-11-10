using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
enum Months
{
    Spring = 1,
    Summer = 2,
    Fall = 3,
    Winter = 4
}

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] private TimeProgressor timeProgressor;
    [SerializeField] private TextMeshProUGUI timeText, pmText, yearText, monthText, dayText;
    // Start is called before the first frame update
    //void Start()
    //{
    //    TimeManager.OnDateTimeChanged += TimeManager_OnDateTimeChanged;
    //}

    //private void TimeManager_OnDateTimeChanged(object sender, TimeManager.OnDateTimeChangedEventArgs e)
    //{
    //    timeText.text = e._displayHour.ToString("D2") + ":" + e._minute.ToString("D2");
    //    if (e._isPM && e._twelveHourclock)
    //    {
    //        pmText.enabled = true;
    //    }
    //    else
    //    {
    //        pmText.enabled = false;
    //    }

    //    yearText.text = "Year: " + e._year.ToString("D2");
    //    Months month = (Months)e._month;
    //    monthText.text = month.ToString()+ ". " + e._dayInMonth.ToString();

    //    Days day = (Days)e._dayInWeek;
    //    dayText.text = day.ToString();
    //}

    // Update is called once per frame

    private void Start()
    {
        
    }
    void Update()
    {
        if (timeProgressor.twelveHourClock)
        {
            timeText.text = timeProgressor.hrPM.ToString("D2") + ":" + timeProgressor.min.ToString("D2");
        }
        else
        { 

            timeText.text = timeProgressor.hour.ToString("D2") + ":" + timeProgressor.min.ToString("D2");
        
        }



        if (timeProgressor.isPM && timeProgressor.twelveHourClock)
        {
            pmText.enabled = true;
        }
        else
        {
            pmText.enabled = false;
        }

        //yearText.text = "Year: " + e._year.ToString("D2");
        //Months month = (Months)e._month;
        //monthText.text = month.ToString() + ". " + e._dayInMonth.ToString();

        //Days day = (Days)e._dayInWeek;
        //dayText.text = day.ToString();
    }
}
