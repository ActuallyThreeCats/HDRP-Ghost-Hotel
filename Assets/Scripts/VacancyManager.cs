using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacancyManager : MonoBehaviour
{
    public static VacancyManager Instance;


    public List<RoomInfo> roomInfo = new List<RoomInfo>();


    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
