using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crafty.Systems.StateMachine;

public class RoomState : State
{
    RoomInfo roomInfo;
    bool infoCollected;
    public override void EnterState(GuestController guest)
    {




        roomInfo = CheckInComputer.Instance.roomInfo;

                roomInfo.occupied = true;
                //guest.SetWaypoint(roomInfo.GetWayPoints()[0].position);
                //Debug.Log(guest.transform.position);
                guest.agent.SetDestination(roomInfo.transform.position);
                GuestManager.Instance.occupants.Add(guest);
                guest.SubtractMoney(roomInfo.GetRoomCost());
                MoneyManager.Instance.AddToHotelBank(roomInfo.GetRoomCost());
                guest.roomInfo = roomInfo;
                guest.SetScheduledDays(Random.Range(1, guest.roomInfo.GetMaxDays() + 1));
        Debug.Log(guest.GetScheduledDays());
                roomInfo.roomState = RoomEnum.occupied;
        
    
              
           
        
    }

    public override void UpdateState(GuestController guest)
    {
       // Debug.Log(guest.agent.remainingDistance);

        if (guest.agent.remainingDistance <= guest.agent.stoppingDistance && !guest.agent.pathPending)
        {
            guest.isCheckedIn = true;
            //Debug.Log(guest.GetGuestID() + " is Checked In" );
            guest.GetComponent<GuestScheduler>().isDoingActivity = false;

        }
    }

    public override void OnCollisionEnter(GuestController guest)
    {

    }
}
