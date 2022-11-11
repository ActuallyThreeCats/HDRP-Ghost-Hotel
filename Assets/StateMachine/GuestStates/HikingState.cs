using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crafty.Systems.StateMachine;
using PathCreation;
using UnityEngine.AI;

public class HikingState : State
{
    private PathCreator hikingPath;
    private float speed;
    float distanceTravelled;
    bool isHiking;
    public override void EnterState(GuestController guest)
    {
        hikingPath = GameObject.FindGameObjectWithTag("HikingPath").GetComponent<PathCreator>();
        speed = guest.GetComponent<NavMeshAgent>().speed;

        for (int i = 0; i < GuestManager.Instance.waypoints.Count; i++)
        {
            if (GuestManager.Instance.waypoints[i].name == "HikingPathWaypoint")
            {
                guest.SetWaypoint(GuestManager.Instance.waypoints[i].transform.position);
                guest.agent.SetDestination(guest.GetWaypoint());

            }
        }
    }

    public override void UpdateState(GuestController guest)
    {
        if (!isHiking && guest.agent.remainingDistance < 0.2f) 
        {
            isHiking = true;
        }
        if (isHiking)
        {
            distanceTravelled += speed * Time.deltaTime;
            guest.transform.position = hikingPath.path.GetPointAtDistance(distanceTravelled);
            guest.transform.rotation = hikingPath.path.GetRotationAtDistance(distanceTravelled);
            if(guest.targetCollider != null)
            {
                guest.GetComponent<GuestScheduler>().isDoingActivity = false;
                distanceTravelled = 0;
                isHiking = false;
            }
      
            
        }

    }

    public override void OnCollisionEnter(GuestController guest)
    {


    }
}
