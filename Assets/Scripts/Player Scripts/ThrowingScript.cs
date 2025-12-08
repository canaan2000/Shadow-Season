using UnityEngine;

public class ThrowingScript : MonoBehaviour
{
    InputSystem_Actions inputActions;

    Inventory inventory;
    PlayerController playerController;


    bool hasThrown = false;

    public float throwSpeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GetComponent<Inventory>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        var throwInput = inputActions.Player.Throw.ReadValue<float>() > 0;

        if (throwInput && !hasThrown && inventory.numberOfBalls > 0)
        {
            GameObject newBall = Instantiate(inventory.currentBall, transform.position + playerController.playerCamera.transform.forward * 3, Quaternion.identity);
            newBall.GetComponent<Rigidbody>().AddForce(playerController.playerCamera.transform.forward * throwSpeed);
            inventory.numberOfBalls--;
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
