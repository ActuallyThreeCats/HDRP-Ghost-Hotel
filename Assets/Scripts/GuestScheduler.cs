
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
    public RandomHotelActivities randomHotelActivities;
    private GuestController guestController;
    public bool isScheduledActivity;
    [SerializeField] private bool hasDoneBreakfast;
    [SerializeField] private bool hasDoneCheckout;
    private int hour;
    private int minute;
    [SerializeField] private bool forceCheckout;


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
    public enum RandomHotelActivities
    {
        Wander = 1,
        GetIce = 2,
        VendingMachines = 3,
        Coffee = 4, 
        Hiking =5,
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

    private void OnDisable()
    {
        timeManager.OnDateTimeChanged -= TimeManager_OnDateTimeChanged;
    }

    private void TimeManager_OnDateTimeChanged(object sender, TimeManager.OnDateTimeChangedEventArgs e)
    {
        if (gameObject.GetComponent<GuestController>().isCheckedIn )
        {
            hour = e._hour;
            minute = e._minute;
        }
        if (hour ==0 && minute == 0)
        {
            hasDoneCheckout = false;
            hasDoneBreakfast = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        
        CheckForScheduledActivities(hour, minute);

        
        if (!isScheduledActivity)
        {
            CheckForActivity();

        }
        //DoActivity();
    }

    public void CheckForFillerActivity()
    {

    }

    public void CheckForScheduledActivities(int hour, int minute)
    {

        //breakfast
        if(hour >= HotelScheduler.Instance.breakfastStartHour && hour < HotelScheduler.Instance.breakfastEndHour && !hasDoneBreakfast)
        {

                isScheduledActivity = true;

                int breakfastCheck = Random.Range(1, 4);
                Debug.Log("Breakfast Check");
                if(breakfastCheck >1 && !isDoingActivity)
                {
                    guestController.SwitchState(guestController.breakfastState);
                    isDoingActivity = true;
                    hasDoneBreakfast = true;
                }

        }
        //Checkout
        else if(hour >= HotelScheduler.Instance.checkOutHour && hour <= HotelScheduler.Instance.checkOutHour + 2 && !hasDoneCheckout)
        {
            if (guestController.roomInfo != null)
            {
                if (guestController.roomInfo.GetDaysRemaining() == 0 || forceCheckout)
                {
                    isScheduledActivity = true;
                    guestController.SwitchState(guestController.checkoutState);
                    isDoingActivity = true;
                    hasDoneCheckout = true;
                    Debug.Log("Ran Checkout State Code");
                }

            }
        }

        //checkout
        
        
        
    }

    public void CheckForActivity()
    {
        int wanderingCheck = Random.Range(1, 6);

   
            if (gameObject.GetComponent<GuestController>().isCheckedIn && !isDoingActivity)
            {
            Debug.Log("WanderCheck: " + wanderingCheck);
                if (wanderingCheck > 1)
                {

                    

                    //Debug.Log("Checked for Activity");

                    //int enumCount = System.Enum.GetNames(typeof(ActivitiesCheck)).Length;


                    //only randoms from filler activities
                    int randomPicker = Random.Range(1, System.Enum.GetNames(typeof(RandomHotelActivities)).Length + 1);


                    randomHotelActivities = (RandomHotelActivities)randomPicker;
                switch (randomHotelActivities)
                {
                    case RandomHotelActivities.Wander:
                        guestController.SwitchState(guestController.wanderState);
                        isDoingActivity = true;
                        Debug.Log("Wander Machine State for " + guestController.GetGuestID());

                        break;
                    case RandomHotelActivities.GetIce:
                        guestController.SwitchState(guestController.iceMachineState);
                        isDoingActivity = true;
                        Debug.Log("Ice  Machine State for " + guestController.GetGuestID());

                        break;
                    case RandomHotelActivities.VendingMachines:
                        guestController.SwitchState(guestController.vendingMachineState);
                        isDoingActivity = true;
                        Debug.Log("Vending Machine State for " + guestController.GetGuestID());

                        break;
                    case RandomHotelActivities.Coffee:
                        guestController.SwitchState(guestController.coffeeState);
                        isDoingActivity = true;
                        Debug.Log("Coffee State for " + guestController.GetGuestID());
                        break;
                    case RandomHotelActivities.Hiking:
                        guestController.SwitchState(guestController.hikingState);
                        isDoingActivity = true;
                        Debug.Log("Hiking State for " + guestController.GetGuestID());

                        break;
                    default:
                        break;
                }


                //activitiesCheck = (ActivitiesCheck)randomPicker;
                ////activitiesCheck = ActivitiesCheck.HotelActivities;
                //isStartingActivity = true;


                //CheckForSubActivity();
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

    }

    #region CheckForSubActivity 
    //public void CheckForSubActivity()
    //{
    //    //if (isStartingActivity)
    //    //{
    //        Debug.Log("StartingActivity");
    //        //isStartingActivity = false;
    //        isDoingActivity = true;
    //        int enumCount = 0;
    //        int randomPicker = 0;

    //        switch (activitiesCheck)
    //        {

    //            case ActivitiesCheck.HotelActivities: // Hotel Activities
    //                enumCount = System.Enum.GetNames(typeof(HotelActivities)).Length;
    //                randomPicker = Random.Range(1, enumCount + 1);
    //                DoHotelActivity(randomPicker);
    //                // Debug.Log("Doing Hotel Activities: " + (HotelActivities)randomPicker);
    //                break;
    //            case ActivitiesCheck.RandomHotelActivites: // Random Hotel Activities
    //                enumCount = System.Enum.GetNames(typeof(RandomHotelActivities)).Length;
    //                randomPicker = Random.Range(1, enumCount + 1);
    //                //Debug.Log("Doing Random Hotel Activities");
    //                DoRandomHotelActivity(randomPicker);

    //                break;
    //            case ActivitiesCheck.TownActivities: // Town Activities
    //                enumCount = System.Enum.GetNames(typeof(TownActivities)).Length;
    //                randomPicker = Random.Range(1, enumCount + 1);
    //                //Debug.Log("Doing Town Activities");

    //                break;
    //            case ActivitiesCheck.RandomTownActivities: // Random Town Activities
    //                enumCount = System.Enum.GetNames(typeof(RandomTownActivities)).Length;
    //                randomPicker = Random.Range(1, enumCount + 1);
    //                //Debug.Log("Doing Random Town Activities");

    //                break;
    //            case ActivitiesCheck.HolidayActivities: // Holiday Activities
    //                enumCount = System.Enum.GetNames(typeof(HolidayActivities)).Length;
    //                randomPicker = Random.Range(1, enumCount + 1);
    //                //Debug.Log("Doing Holiday Activities");

    //                if (!isHoliday)
    //                {
    //                    break;
    //                }
    //                break;
    //            case ActivitiesCheck.RandomHolidayActivities: // Random Holiday Activities
    //                enumCount = System.Enum.GetNames(typeof(RandomHolidayActivities)).Length;
    //                randomPicker = Random.Range(1, enumCount + 1);
    //                //Debug.Log("Doing Random Holiday Activities");

    //                if (!isHoliday)
    //                {
    //                    break;
    //                }
    //                break;
    //            default:
    //                break;
    //        }


    //    //}
    //}
    #endregion

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
            case RandomHotelActivities.Coffee:
                guestController.SwitchState(guestController.coffeeState);
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
