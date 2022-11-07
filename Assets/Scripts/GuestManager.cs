using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{

    public static GuestManager Instance;

    public List<GameObject> waypoints = new List<GameObject>();
    //public List<GameObject> queuePoints = new List<GameObject>();


    public List<GuestController> totalGuests = new List<GuestController>();

    public List<GuestController> occupants = new List<GuestController>();


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateWaypoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateWaypoints()
    {
        waypoints.Clear();
        foreach (GameObject seeker in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            waypoints.Add(seeker);
        }
        //foreach (GameObject seeker in GameObject.FindGameObjectsWithTag("QueuePoints"))
        //{
        //    queuePoints.Add(seeker);
        //}
    }
}
