using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crafty.Systems.StateMachine;

public class RoomState : State
{
    public override void EnterState(GuestController guest)
    {
        for (int i = 0; i < VacancyManager.Instance.roomInfo.Count; i++)
        {
            if (!VacancyManager.Instance.roomInfo[i].occupied)
            {
                VacancyManager.Instance.roomInfo[i].occupied = true;
                guest.SetWaypoint(VacancyManager.Instance.roomInfo[i].GetWayPoints()[0].position);
                guest.agent.SetDestination(guest.GetWaypoint());
                GuestManager.Instance.occupants.Add(guest);
                break;
            }
        }
    }

    public override void UpdateState(GuestController guest)
    {
        if (guest.agent.remainingDistance < 0.1f)
        {
            guest.isCheckedIn = true;
        }
    }

    public override void OnCollisionEnter(GuestController guest)
    {

    }
}
