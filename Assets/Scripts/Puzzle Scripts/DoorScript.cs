using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class DoorScript : MonoBehaviour
{
    public List<GameObject> puzzleElements;

    public Vector3 initialPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPos = transform.position;
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
        else
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        transform.position = new Vector3(initialPos.x, initialPos.y + 5.0f, initialPos.z);
    }

    void CloseDoor()
    {
        transform.position = initialPos;
    }
}
