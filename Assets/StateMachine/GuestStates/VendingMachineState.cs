using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crafty.Systems.StateMachine;

public class VendingMachineState : State
{
    float totalTime = 3f;
    float elapsedTime;

    public override void EnterState(GuestController guest)
    {
        Debug.Log("Entered Vending Machine State");

        for (int i = 0; i < GuestManager.Instance.waypoints.Count; i++)
        {
            if (GuestManager.Instance.waypoints[i].name == "VendingMachineWaypoint")
            {
                guest.SetWaypoint(GuestManager.Instance.waypoints[i].transform.position);
                guest.agent.SetDestination(guest.GetWaypoint());
                guest.SubtractMoney(PricesManager.Instance.GetVendingMachinePrice());
                MoneyManager.Instance.AddToHotelBank(PricesManager.Instance.GetVendingMachinePrice());

            }
            if (i == GuestManager.Instance.waypoints.Count && GuestManager.Instance.waypoints[i].name != "VendingMachineWaypoint")
            {
                Debug.LogError("No waypoint titled VendingMachineWay");
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
