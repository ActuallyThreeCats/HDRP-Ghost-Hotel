using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crafty.Systems.StateMachine;

public class CheckOutState : State
{
    float timeElapsed;
    float waitTime = 2f;
    bool checkingOut;
    public override void EnterState(GuestController guest)
    {
        for (int i = 0; i < GuestManager.Instance.waypoints.Count; i++)
        {
            if (GuestManager.Instance.waypoints[i].name == "CheckOutWaypoint")
            {
                guest.SetWaypoint(GuestManager.Instance.waypoints[i].transform.position);
                guest.agent.SetDestination(guest.GetWaypoint());
                checkingOut = true;
            }
        }

    }

    public override void UpdateState(GuestController guest)
    {
        
        if (guest.agent.remainingDistance < 0.5f && checkingOut)
        {
            if(timeElapsed < waitTime)
            {
                timeElapsed += Time.deltaTime;
                Debug.Log(timeElapsed/waitTime);
            }
            else
            {
                Debug.Log("Testing for waypoint");

                for (int i = 0; i < GuestManager.Instance.waypoints.Count; i++)
                {
                    if (GuestManager.Instance.waypoints[i].name == "ExitWaypoint")
                    {
                        guest.SetWaypoint(GuestManager.Instance.waypoints[i].transform.position);
                        guest.agent.SetDestination(guest.GetWaypoint());
                        checkingOut = false;
                        break;
                    }
                }
            }
            return;
        }

        if(guest.agent.remainingDistance <0.5f && !checkingOut)
        {
            GameObject.Destroy(guest.gameObject);
        }
    }

    public override void OnCollisionEnter(GuestController guest)
    {

    }
}
