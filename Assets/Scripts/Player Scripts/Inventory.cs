using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    InputSystem_Actions inputActions;

    CarryBallScript carryBallScript;

    public int numberOfBalls = 0;
    public GameObject[] ballprefabs;

    public Dictionary<GameObject, int> ballInventory = new Dictionary<GameObject, int>();

    public GameObject currentBall;

    bool hasSwitched = false;

    public int ballIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carryBallScript = GetComponent<CarryBallScript>();

        foreach (GameObject ball in ballprefabs)
        {
            ballInventory.Add(ball, 0);
        }

        if (ballprefabs.Length > 0)
        {
            currentBall = GetAvailableBalls()[ballIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        var nextInput = inputActions.Player.Next.ReadValue<float>() > 0;
        var previousInput = inputActions.Player.Previous.ReadValue<float>() > 0;

        if (nextInput && !hasSwitched)
        {
            ballIndex = (ballIndex + 1) % ballprefabs.Length;
            GameObject carriedBall;
            if (numberOfBalls > 0)
            {
                carriedBall = Instantiate(GetAvailableBalls()[ballIndex]);
                carryBallScript.CarryBall(carriedBall);
            }
        }
        if (previousInput && !hasSwitched)
        {
            ballIndex--;
            if (ballIndex < 0)
            {
                ballIndex = ballprefabs.Length - 1;
            }
            GameObject carriedBall;
            if (numberOfBalls > 0)
            {
                carriedBall = Instantiate(GetAvailableBalls()[ballIndex]);
                carryBallScript.CarryBall(carriedBall);
            }
        }
        if (nextInput || previousInput)
        {
            hasSwitched = true;
            
        }
        if (!nextInput && !previousInput && hasSwitched)
        {
            hasSwitched = false;
        }

        currentBall = ballprefabs[ballIndex];

        if (!GetAvailableBalls().Contains(currentBall))
        {
            currentBall = null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SafeZoneScript>() != null)
        {
            return;
        }

        if (other.gameObject.CompareTag("ball1"))
        {
            GameObject newBall = Instantiate(other.gameObject.transform.parent.gameObject);

            GameObject matchingPrefab = null;

            foreach (GameObject ball in ballprefabs)
            {
                if (newBall.CompareTag(ball.tag))
                {
                    matchingPrefab = ball;
                    break;
                }
            }

            carryBallScript.CarryBall(newBall);

            numberOfBalls++;

            ballInventory[matchingPrefab]++;

            Destroy(other.gameObject.transform.parent.gameObject);
            Debug.Log("Balls in inventory: " + numberOfBalls);
        }
    }

    List<GameObject> GetAvailableBalls()
    {
        List<GameObject> availableBalls = new List<GameObject>();
        foreach (var ballType in ballInventory)
        {
            if (ballType.Value > 0)
            {
                availableBalls.Add(ballType.Key);
            }
        }
        return availableBalls;
    }
     
    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Next.Enable();
        inputActions.Player.Previous.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Next.Disable();
        inputActions.Player.Previous.Disable();
    }
}
