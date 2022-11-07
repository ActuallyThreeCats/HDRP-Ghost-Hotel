using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crafty.Systems.StateMachine;

public class BreakfastState : State
{
    public override void EnterState(GuestController guest)
    {
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


    }

    public override void OnCollisionEnter(GuestController guest)
    {

    }
}
