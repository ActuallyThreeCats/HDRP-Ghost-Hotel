using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crafty.Systems.StateMachine;

public class QueueingState : State
{

    

    public override void EnterState(GuestController guest)
    {
        Debug.Log("There are currently " + CheckInManager.Instance.guestsInQueue.Count + " guests in queue.");

        SetQueue(guest);
    }

    public override void UpdateState(GuestController guest)
    {


        if (!CheckInManager.Instance.isTargeted)
        {
            Debug.Log("Is not targeted");
            for (int i = 0; i < CheckInManager.Instance.guestsInQueue.Count; i++)
            {
               if(i == 0)
                {
                    guest.SwitchState(guest.checkInState);
                    
                    
                }

            }
            
            LiveQueue();
        }




    }

    public override void OnCollisionEnter(GuestController guest)
    {

    }

    public static void SetQueue(GuestController guest)
    {
        if (CheckInManager.Instance.guestsInQueue.Count > 1)
        {
            for (int i = 0; i < CheckInManager.Instance.guestsInQueue.Count; i++)
            {
                if (i == CheckInManager.Instance.guestsInQueue.Count - 1)
                {

                    Vector3 distanceVector = guest.transform.position - CheckInManager.Instance.guestsInQueue[i - 1].GetComponent<GuestController>().GetWaypoint();

                    Vector3 distanceVectorNormalized = distanceVector.normalized;

                    Vector3 targetPosition = CheckInManager.Instance.guestsInQueue[i - 1].GetComponent<GuestController>().GetWaypoint() + (distanceVectorNormalized * 2);

                    guest.SetWaypoint(targetPosition);
                    guest.agent.SetDestination(guest.GetWaypoint());
                }
            }
        }
    }


    public static void LiveQueue()
    {
        //Debug.Log("Live queue for " + guest.GetGuestID());
        if (!CheckInManager.Instance.inUse)
        {
            if (CheckInManager.Instance.guestsInQueue.Count > 1)
            {
                for (int i = 0; i < CheckInManager.Instance.guestsInQueue.Count; i++)
                {
                    if (i >= 1)
                    {

                        Debug.Log("Moving Guest #" + i);
                        Vector3 distanceVector = CheckInManager.Instance.guestsInQueue[i].transform.position - CheckInManager.Instance.guestsInQueue[i - 1].GetComponent<GuestController>().GetWaypoint();
                        //Vector3 distanceVector = guest.transform.position - CheckInManager.Instance.guestsInQueue[i - 1].GetComponent<GuestController>().GetWaypoint();

                        Vector3 distanceVectorNormalized = distanceVector.normalized;
                        //Vector3 distanceVectorNormalized = distanceVector.normalized;

                        //Vector3 targetPosition = CheckInManager.Instance.guestsInQueue[i - 1].GetComponent<GuestController>().GetWaypoint() + (distanceVectorNormalized * 2);
                        Vector3 targetPosition = CheckInManager.Instance.guestsInQueue[i - 1].GetComponent<GuestController>().GetWaypoint() + (distanceVectorNormalized * 2);

                        //guest.SetWaypoint(CheckInManager.Instance.guestsInQueue[i - 1].GetWaypoint());
                        CheckInManager.Instance.guestsInQueue[i].SetWaypoint(targetPosition);

                        //guest.agent.SetDestination(guest.GetWaypoint());
                        CheckInManager.Instance.guestsInQueue[i].agent.SetDestination(CheckInManager.Instance.guestsInQueue[i].GetWaypoint());
                    }


                }
            }
        }



    }
}
