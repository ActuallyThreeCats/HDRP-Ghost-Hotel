using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crafty.Systems.StateMachine;

public class BreakfastState : State
{
    float eatingTime = 5f;
    float amountEatenTime;

    public override void EnterState(GuestController guest)
    {
        Debug.Log(guest.GetGuestID() + " has entered breakfast state");
        for (int i = 0; i < GuestManager.Instance.waypoints.Count; i++)
        {
            if (GuestManager.Instance.waypoints[i].name == "Breakfast")
            {
                guest.SetWaypoint(GuestManager.Instance.waypoints[i].transform.position);
                guest.agent.SetDestination(guest.GetWaypoint());
            }
            if (i == GuestManager.Instance.waypoints.Count && GuestManager.Instance.waypoints[i].name != "Breakfast")
            {
                Debug.LogError("No waypoint titled Breakfast");
            }
        }
    }

    public override void UpdateState(GuestController guest)
    {
        if(guest.agent.remainingDistance < 0.5f)
        {
            if (amountEatenTime < eatingTime)
            {
                amountEatenTime += Time.deltaTime;
            }
            if(amountEatenTime >= eatingTime)
            {
                guest.SwitchState(guest.roomState);
            }

        }

    }

    public override void OnCollisionEnter(GuestController guest)
    {

    }
}
