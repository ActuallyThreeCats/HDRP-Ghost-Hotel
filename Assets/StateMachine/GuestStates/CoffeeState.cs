using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crafty.Systems.StateMachine;

public class CoffeeState : State
{
    float totalTime = 3f;
    float elapsedTime;

    public override void EnterState(GuestController guest)
    {
        Debug.Log("Entered Coffee Machine State");

        for (int i = 0; i < GuestManager.Instance.waypoints.Count; i++)
        {
            if (GuestManager.Instance.waypoints[i].name == "CoffeeWaypoint")
            {
                Debug.Log("Coffee Waypoint Set");
                guest.SetWaypoint(GuestManager.Instance.waypoints[i].transform.position);
                guest.agent.SetDestination(guest.GetWaypoint());
                
            }
        }
    }

    public override void UpdateState(GuestController guest)
    {
        if (guest.agent.remainingDistance < 0.5f)
        {
            if (elapsedTime < totalTime)
            {
                elapsedTime += Time.deltaTime;
            }
            if (elapsedTime >= totalTime)
            {
                guest.GetComponent<GuestScheduler>().isDoingActivity = false;
            }
        }

    }

    public override void OnCollisionEnter(GuestController guest)
    {

    }
}