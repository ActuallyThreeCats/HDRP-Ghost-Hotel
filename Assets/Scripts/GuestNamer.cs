using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestNamer : MonoBehaviour
{
    [SerializeField] private List<string> firstNames = new List<string>();
    [SerializeField] private List<string> lastNames = new List<string>();

    public List<string> GetFirstNames()
    {
        return firstNames;
    }
    public List<string> GetLastNames()
    {
        return lastNames;
    }
    
   
    public void RandomlyGenerateName(out string name)
    {
        string firstName = null;
        string lastName = null;
        name = null;
        int firstNameIndex = Random.Range(0, firstNames.Count + 1);
        int lastNameIndex = Random.Range(0, lastNames.Count + 1);

        for (int i = 0; i < firstNames.Count; i++)
        {
            if(i == firstNameIndex)
            {
                firstName = firstNames[i];
                for (int j = 0; j < lastNames.Count; j++)
                {
                    if (j == lastNameIndex)
                    {
                        lastName = lastNames[j];
                        //Debug.Log(firstName + " " + lastName);
                        name = firstName + " " + lastName;
                        //Debug.Log(name);
                        break;
                    }
                }
            }
        }

        
        
    }
}
