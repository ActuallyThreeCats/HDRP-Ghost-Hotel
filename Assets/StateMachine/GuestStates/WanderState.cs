using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crafty.Systems.StateMachine;
using UnityEngine.AI;

public class WanderState : State
{
    int wanderSteps = 4;
    int currentSteps = 0;



    public override void EnterState(GuestController guest)
    {
        Vector3 point;
        RandomNavemeshLocation(3f, guest, out point);
        guest.isWandering = false;
        //SetNavmeshLocation(guest);
    }

    public override void UpdateState(GuestController guest)
    {


        if (!guest.isWandering)
        {
            SetNavmeshLocation(guest);
            guest.isWandering = true;
        }
        if(guest.agent.remainingDistance < 0.2f)
        {
            guest.isIdle = true;

        }

        if (guest.isWandering && guest.isIdle)
        {
            if(currentSteps < wanderSteps && guest.currentTime >= guest.timeToWait - 0.5f)
            {
                currentSteps++;
                Debug.Log(currentSteps);
                guest.isWandering = false;
                guest.isIdle = false;
            }
            if(currentSteps >= wanderSteps)
            {
                currentSteps = 0;
                guest.GetComponent<GuestScheduler>().isDoingActivity = false;
            }
        }  
    }

    public override void OnCollisionEnter(GuestController guest)
    {

    }

    public bool RandomNavemeshLocation(float radius, GuestController guest, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = guest.transform.position + Random.insideUnitSphere * radius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public void SetNavmeshLocation(GuestController guest)
    {
        Vector3 result;
        if(RandomNavemeshLocation(10,guest, out result))
        {
            Debug.Log("SetNavmeshLocation");
            Debug.DrawRay(result, Vector3.up, Color.blue, 1.0f);
            guest.SetWaypoint(result);
            guest.agent.SetDestination(guest.GetWaypoint());
            guest.currentTime = 0;
        }

    }

}
