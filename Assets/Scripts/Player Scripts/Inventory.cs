using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    InputSystem_Actions inputActions;

    CarryBallScript carryBallScript;

    public int numberOfBalls = 0;
    public GameObject[] ballprefabs;

    public GameObject currentBall;

    bool hasSwitched = false;

    public int ballIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carryBallScript = GetComponent<CarryBallScript>();
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
                carriedBall = Instantiate(ballprefabs[ballIndex]);
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
                carriedBall = Instantiate(ballprefabs[ballIndex]);
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball1"))
        {
            numberOfBalls++;
            Destroy(collision.gameObject);
        }
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
