using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Crafty.Systems.StateMachine;
public class GuestController : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] private string guestName;
    [SerializeField] private int guestID;
    [SerializeField] private int money;
    private int maxMoney = 9999;
    [SerializeField] private Vector3 waypoint;
    public bool isCheckedIn;
    [SerializeField] private Transform target;
    public RoomInfo roomInfo;
    public bool isWandering = false;
    public bool isIdle = false;

    private int daysStaying;


    //add geter/seter for this later
    public float timeToWait = 4f;
    public float currentTime;
    public Collider targetCollider;


    State currentState;
    public ArrivalState arrivalState = new ArrivalState();
    public QueueingState queueingState = new QueueingState();
    public CheckInState checkInState = new CheckInState();
    public RoomState roomState = new RoomState();
    public BreakfastState breakfastState = new BreakfastState();
    public WanderState wanderState = new WanderState();
    public CoffeeState coffeeState = new CoffeeState();
    public VendingMachineState vendingMachineState = new VendingMachineState();
    public IceMachineState iceMachineState = new IceMachineState();
    public CheckOutState checkoutState = new CheckOutState();
    public HikingState hikingState = new HikingState();

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = arrivalState;
        agent.stoppingDistance = 0;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        currentState.EnterState(this);
       
    }

    public int GetGuestID()
    {
        return guestID;
    }
   
    public void SetGuestID(int amt)
    {
        if(GuestManager.Instance.totalGuests.Count == 0)
        {
            guestID = amt;
        }
        for (int i = 0; i < GuestManager.Instance.totalGuests.Count; i++)
        {
            if(GuestManager.Instance.totalGuests[i].GetGuestID() == amt)
            {
                arrivalState.GenerateGuestID(this);
                Debug.Log("Generating New ID");
                
            }
            else 
            {
                guestID = amt;
            
            }
        }

        
    }

    public int GetMoney()
    {
        return money;
    }
    public void SubtractMoney(int amt)
    {
        if(amt < 0)
        {
            return;
        }
        if(amt > money)
        {
            return;
        }
        if(amt < money)
        {
            money -= amt;
        }
    }
    public void AddMoney(int amt)
    {
        if(amt + money > maxMoney)
        {
            money = maxMoney;
        }
        else
        {
            money += amt;
        }
    }
    public void SetMoney(int amt)
    {
        if(amt > maxMoney)
        {
            money = maxMoney;
        }
        else
        {
            money = amt;
        }
    }
    public void SetScheduledDays(int amt)
    {
        Debug.Log("scheduled " + amt + " days");
        if(amt < roomInfo.GetMaxDays())
        daysStaying = amt;
        roomInfo.SetDaysScheduled(daysStaying);
    }


    public Vector3 GetWaypoint()
    {
        return waypoint;
    }


    public void SetWaypoint(Vector3 waypoint)
    {
        this.waypoint = waypoint;
    }

    //public List<GameObject> WaypointList()
    //{
    //    return waypoints;
    //}

    private void Update()
    {
        currentState.UpdateState(this);
        if (currentState == checkInState||currentState == queueingState )
        {
            gameObject.transform.LookAt(target);

        }
        if (isIdle)
        {
            currentTime += Time.deltaTime;

        }
        if(currentTime >= timeToWait)
        {
            currentTime = 0;
        }

        //Debug.Log(currentState);
    }

    public void SwitchState(State state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HikingPathExit"))
        {
            targetCollider = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        targetCollider = null;
    }
}
