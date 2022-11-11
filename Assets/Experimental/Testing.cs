using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
    float timeElapsed;
    float maxTime = 1f;
    private Grid grid;

    private void Start()
    {
        grid = new Grid(4, 2, 10, gameObject.transform.position);


    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > maxTime)
        {
            timeElapsed = 0;
            int x = Random.Range(0, 4);
            int y = Random.Range(0, 2);
            
            grid.SetValue(x,y, 52);
        }
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        
    }
}
