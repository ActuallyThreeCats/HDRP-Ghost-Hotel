using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestSpawner : MonoBehaviour
{
    [SerializeField] private GameObject guestPrefab;
    [SerializeField] private Transform waypoint;
    [SerializeField] private int currentGuests;
    [SerializeField] private int waitingGuests;
    [SerializeField] private int maxGuests; //max rooms
    [SerializeField] private int buildingCapacity;
    [SerializeField] private bool respawnAvailable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        maxGuests = VacancyManager.Instance.roomInfo.Count;
        if (currentGuests + waitingGuests >= buildingCapacity && respawnAvailable)
        {
            respawnAvailable = false;
        }else if(currentGuests + waitingGuests < buildingCapacity && respawnAvailable)
        {
            
            StartCoroutine(SpawnTimer());
        }



        if (!respawnAvailable)
        {
            return;
        }
        
    }

    public IEnumerator SpawnTimer()
    {
        respawnAvailable = false;
        int respawnCountdown = Random.Range(3, 10);
        Debug.Log("Respawning in " + respawnCountdown);
        yield return new WaitForSeconds(respawnCountdown);
        Debug.Log("Currently Respawning Guest");
        Instantiate(guestPrefab, waypoint);
        if (currentGuests + waitingGuests < buildingCapacity && !respawnAvailable)
        {
            respawnAvailable = true;
        }
        currentGuests = GuestManager.Instance.occupants.Count;
        waitingGuests = CheckInManager.Instance.guestsInQueue.Count;

    }
}
