using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crafty.Systems.StateMachine;

public class RoomState : State
{
    public override void EnterState(GuestController guest)
    {

        //Update this to be returnable later
        for (int i = 0; i < VacancyManager.Instance.roomInfo.Count; i++)
        {
            if (!VacancyManager.Instance.roomInfo[i].occupied)
            {

                VacancyManager.Instance.roomInfo[i].occupied = true;
                guest.SetWaypoint(VacancyManager.Instance.roomInfo[i].GetWayPoints()[0].position);
                guest.agent.SetDestination(guest.GetWaypoint());
                GuestManager.Instance.occupants.Add(guest);
                guest.SubtractMoney(VacancyManager.Instance.roomInfo[i].GetRoomCost());
                MoneyManager.Instance.AddToHotelBank(VacancyManager.Instance.roomInfo[i].GetRoomCost());
                guest.roomInfo = VacancyManager.Instance.roomInfo[i];
                guest.SetScheduledDays(Random.Range(1, guest.roomInfo.GetMaxDays() + 1));
                break;
            }
        }
    }

    public override void UpdateState(GuestController guest)
    {
        if (guest.agent.remainingDistance < 0.1f)
        {
            guest.isCheckedIn = true;
            Debug.Log(guest.GetGuestID() + " is Checked In" );
            guest.GetComponent<GuestScheduler>().isDoingActivity = false;

        }
    }

    public override void OnCollisionEnter(GuestController guest)
    {

    }
}
