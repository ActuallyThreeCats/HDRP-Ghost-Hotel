using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInManager : MonoBehaviour
{
    public static CheckInManager Instance;
    [SerializeField] private GameObject waypoint;
    public bool inUse;
    public bool isTargeted;
    public List<GuestController> guestsInQueue = new List<GuestController>();
    

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
