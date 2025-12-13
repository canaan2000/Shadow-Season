using UnityEngine;

public class ThrowingScript : MonoBehaviour
{
    InputSystem_Actions inputActions;

    InventoryNew inventory;
    PlayerController playerController;


    bool hasThrown = false;

    public float throwSpeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GetComponent<InventoryNew>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        var throwInput = inputActions.Player.Throw.ReadValue<float>() > 0;

        GameObject matchingPrefab = null;

        foreach (GameObject ball in inventory.ballPrefabs)
        {
            if (inventory.currentBall != null && inventory.currentBall.name.Contains(ball.name))
            {
                matchingPrefab = ball;
                break;
            }
        }

        if (throwInput && !hasThrown && inventory.ballInventory[matchingPrefab.tag] > 0)
        {
            GameObject newBall = Instantiate(matchingPrefab, transform.position + playerController.playerCamera.transform.forward * 3, Quaternion.identity);
            newBall.GetComponent<Rigidbody>().AddForce(playerController.playerCamera.transform.forward * throwSpeed);
            inventory.ballInventory[matchingPrefab.tag]--;
            hasThrown = true;
        }
        if (!throwInput && hasThrown)
        {
            hasThrown = false;
        }
    }

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Enable();
        inputActions.Player.Throw.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Throw.Disable();
    }
}
