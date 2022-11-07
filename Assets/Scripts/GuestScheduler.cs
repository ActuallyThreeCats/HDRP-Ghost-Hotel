
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestScheduler : MonoBehaviour
{
    [SerializeField] bool isHoliday;
   private TimeManager timeManager;
    //[SerializeField] private bool isStartingActivity;
    public bool isDoingActivity = false;
    //private bool isResettingActivity;
    private ActivitiesCheck activitiesCheck;
    private HotelActivities hotelActivites;
    private RandomHotelActivities randomHotelActivities;
    private GuestController guestController;

    private int chosenActivity;
    #region Enums
    enum ActivitiesCheck
    {
        HotelActivities = 1,
        RandomHotelActivites = 2,
        TownActivities = 3,
        RandomTownActivities = 4,
        HolidayActivities = 5,
        RandomHolidayActivities = 6,
    }

    enum HotelActivities
    {
        Hiking = 1,
        Breakfast = 2,
        HotelLakeActivites = 3,
    }

    enum HotelLakeActivities
    {
        Walking = 1,
        Swimming = 2,
    }
    enum RandomHotelActivities
    {
        Wander = 1,
        GetIce = 2,
        VendingMachines = 3,
        GiftShop = 4,   
    }

    enum TownActivities
    {
        Cafe = 1,
        FlowerShop = 2,
        Tavern = 3,
        TownLakeActivities = 4,
    }

    enum TownLakeActivities
    {
        Walking = 1,
        Swimming = 2,
    }

    enum RandomTownActivities
    {
        Wandering = 1,
        DumpsterDiving = 2,
        SightSeeing = 3,
    }

    enum HolidayActivities
    {

    }

    enum RandomHolidayActivities
    {

    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        timeManager.OnDateTimeChanged += TimeManager_OnDateTimeChanged;
        guestController = gameObject.GetComponent<GuestController>();
    }

    private void TimeManager_OnDateTimeChanged(object sender, TimeManager.OnDateTimeChangedEventArgs e)
    {
        if (gameObject.GetComponent<GuestController>().isCheckedIn && !isDoingActivity)
        {
            CheckForScheduledActivities(e._hour, e._minute);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForActivity();
        //DoActivity();
    }

    public void CheckForScheduledActivities(int hour, int minute)
    {
        if(hour >= HotelScheduler.Instance.breakfastStartHour)
        {
            if(minute >= HotelScheduler.Instance.breakfastStartMinute)
            {
                int breakfastCheck = Random.Range(1, 4);
                if(breakfastCheck >1)
                {
                    guestController.SwitchState(guestController.breakfastState);
                    isDoingActivity = true;
                }
            }
        }
    }

    public void CheckForActivity()
    {
        int wanderingCheck = Random.Range(1, 5);

   
            if (gameObject.GetComponent<GuestController>().isCheckedIn && !isDoingActivity)
            {
            Debug.Log("WanderCheck: " + wanderingCheck);
                if (wanderingCheck > 3)
                {

                    Debug.Log("Checked for Activity");

                    int enumCount = System.Enum.GetNames(typeof(ActivitiesCheck)).Length;

                    int randomPicker = Random.Range(1, enumCount);

                    activitiesCheck = (ActivitiesCheck)randomPicker;
                    //activitiesCheck = ActivitiesCheck.HotelActivities;
                    //isStartingActivity = true;
                    CheckForSubActivity();
                }
                else
                {
                    Debug.Log("Wandering");
                    isDoingActivity = true;
                    gameObject.GetComponent<GuestController>().SwitchState(gameObject.GetComponent<GuestController>().wanderState);
                }
            }
        
    }

    public void CheckForSubActivity()
    {
        //if (isStartingActivity)
        //{
            Debug.Log("StartingActivity");
            //isStartingActivity = false;
            isDoingActivity = true;
            int enumCount = 0;
            int randomPicker = 0;

            switch (activitiesCheck)
            {

                case ActivitiesCheck.HotelActivities: // Hotel Activities
                    enumCount = System.Enum.GetNames(typeof(HotelActivities)).Length;
                    randomPicker = Random.Range(1, enumCount + 1);
                    DoHotelActivity(randomPicker);
                    // Debug.Log("Doing Hotel Activities: " + (HotelActivities)randomPicker);
                    break;
                case ActivitiesCheck.RandomHotelActivites: // Random Hotel Activities
                    enumCount = System.Enum.GetNames(typeof(RandomHotelActivities)).Length;
                    randomPicker = Random.Range(1, enumCount + 1);
                    //Debug.Log("Doing Random Hotel Activities");
                    DoRandomHotelActivity(randomPicker);

                    break;
                case ActivitiesCheck.TownActivities: // Town Activities
                    enumCount = System.Enum.GetNames(typeof(TownActivities)).Length;
                    randomPicker = Random.Range(1, enumCount + 1);
                    //Debug.Log("Doing Town Activities");

                    break;
                case ActivitiesCheck.RandomTownActivities: // Random Town Activities
                    enumCount = System.Enum.GetNames(typeof(RandomTownActivities)).Length;
                    randomPicker = Random.Range(1, enumCount + 1);
                    //Debug.Log("Doing Random Town Activities");

                    break;
                case ActivitiesCheck.HolidayActivities: // Holiday Activities
                    enumCount = System.Enum.GetNames(typeof(HolidayActivities)).Length;
                    randomPicker = Random.Range(1, enumCount + 1);
                    //Debug.Log("Doing Holiday Activities");

                    if (!isHoliday)
                    {
                        break;
                    }
                    break;
                case ActivitiesCheck.RandomHolidayActivities: // Random Holiday Activities
                    enumCount = System.Enum.GetNames(typeof(RandomHolidayActivities)).Length;
                    randomPicker = Random.Range(1, enumCount + 1);
                    //Debug.Log("Doing Random Holiday Activities");

                    if (!isHoliday)
                    {
                        break;
                    }
                    break;
                default:
                    break;
            }
        
            
        //}
    }


    public void DoActivity()
    {
       
    }


    public void DoHotelActivity(int activity)
    {
        hotelActivites = (HotelActivities)activity;

        switch (hotelActivites)
        {
            case HotelActivities.Hiking:
                Debug.Log("Hiking");
                break;
            case HotelActivities.Breakfast:
                gameObject.GetComponent<GuestController>().SwitchState(gameObject.GetComponent<GuestController>().breakfastState);
                break;
            case HotelActivities.HotelLakeActivites:
                Debug.Log("HotelLakeActivities");
                break;
            default:
                break;
        }
    }

    public void DoRandomHotelActivity(int activity)
    {
        randomHotelActivities = (RandomHotelActivities)activity;

        switch (randomHotelActivities)
        {
            case RandomHotelActivities.Wander:
                break;
            case RandomHotelActivities.GetIce:
                break;
            case RandomHotelActivities.VendingMachines:
                break;
            case RandomHotelActivities.GiftShop:
                break;
            default:
                break;
        }
    }

    IEnumerator WaitForActivity()
    {
        yield return new WaitForSeconds(5f);
        isDoingActivity = false;
        //isStartingActivity = false;
    }


}
