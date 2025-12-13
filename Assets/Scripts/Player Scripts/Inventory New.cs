using System.Collections.Generic;
using UnityEngine;

public class InventoryNew : MonoBehaviour
{
    InputSystem_Actions inputActions;

    bool hasSwitched = false;

    CarryBallScript carryBallScript;

    public Dictionary<string, int> ballInventory = new Dictionary<string, int>();
    public List<GameObject> ballPrefabs = new List<GameObject>();

    public int ballIndex = 0;

    public GameObject currentBall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        carryBallScript = GetComponent<CarryBallScript>();

        foreach (GameObject ball in ballPrefabs)
        {
            ballInventory.Add(ball.tag, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var nextInput = inputActions.Player.Next.ReadValue<float>() > 0;
        var previousInput = inputActions.Player.Previous.ReadValue<float>() > 0;

        if (nextInput && !hasSwitched)
        {
            ballIndex = (ballIndex + 1) % ballPrefabs.Count;
            if (ballInventory[ballPrefabs[ballIndex].tag] > 0)
            {
                currentBall = ballPrefabs[ballIndex];
                carryBallScript.CarryBall(Instantiate(ballPrefabs[ballIndex]));
            }
            else
            {
                carryBallScript.ChangeBall();
            }
            hasSwitched = true;
        }
        if (previousInput && !hasSwitched)
        {
            ballIndex--;
            if (ballIndex < 0)
            {
                ballIndex = ballPrefabs.Count - 1;
            }
            if (ballInventory[ballPrefabs[ballIndex].tag] > 0)
            {
                currentBall = ballPrefabs[ballIndex];
                carryBallScript.CarryBall(Instantiate(ballPrefabs[ballIndex]));
            }
            else
            {
                carryBallScript.ChangeBall();
            }
                hasSwitched = true;
        }

        if (!nextInput && !previousInput && hasSwitched)
        {
            hasSwitched = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ball1"))
        {
            string ballTag = other.gameObject.transform.parent.gameObject.tag;
            if (ballInventory.ContainsKey(ballTag))
            {
                ballInventory[ballTag]++;
                GameObject newBall = Instantiate(other.gameObject.transform.parent.gameObject);
                ballIndex = ballPrefabs.FindIndex(b => b.tag == ballTag);
                carryBallScript.CarryBall(newBall);
                currentBall = ballPrefabs[ballIndex];
            }
            Destroy(other.gameObject.transform.parent.gameObject);

            Debug.Log("Balls in inventory: " + ballInventory[ballTag]);
        }
    }

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Next.Enable();
        inputActions.Player.Previous.Enable();
    }
}
