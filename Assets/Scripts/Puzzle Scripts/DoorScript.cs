using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class DoorScript : MonoBehaviour
{
    public List<GameObject> puzzleElements;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int trueCount = 0;

        foreach (GameObject element in puzzleElements)
        {
            PedistalScript pedestal = element.GetComponent<PedistalScript>();
            if (pedestal.isActivated)
            {
                trueCount++;
            }
        }

        if (trueCount == puzzleElements.Count)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        Destroy(gameObject);
    }
}
