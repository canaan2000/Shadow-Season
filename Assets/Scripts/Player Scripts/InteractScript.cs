using UnityEngine;

public class InteractScript : MonoBehaviour
{
    InputSystem_Actions inputActions;
    Inventory inventory;
    PlayerController playerController;
    CarryBallScript carryBallScript;
    Camera playerCam;


    bool hasInteracted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GetComponent<Inventory>();
        playerController = GetComponent<PlayerController>();
        carryBallScript = GetComponent<CarryBallScript>();

        playerCam = playerController.playerCamera;
    }

    // Update is called once per frame
    void Update()
    {
        var interactInput = inputActions.Player.Interact.ReadValue<float>() > 0;

        if (interactInput && !hasInteracted)
        {
            hasInteracted = true;

            Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3f))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);
                switch (hit.collider.gameObject.tag)
                {
                    case "Pedestal":
                        PedistalScript pedistal = hit.collider.gameObject.GetComponent<PedistalScript>();
                        if (inventory.currentBall != null && !pedistal.isActivated)
                        {
                            GameObject newBall = Instantiate(inventory.currentBall);
                            pedistal.placedBall = newBall;
                            inventory.numberOfBalls--;
                        }
                        break;
                    case "ball1":
                        inventory.numberOfBalls++;

                        carryBallScript.CarryBall(hit.collider.gameObject.transform.parent.gameObject);

                        Destroy(hit.collider.gameObject.transform.parent.gameObject);

                        break;
                }
            }
        }
        if (!interactInput && hasInteracted)
        {
            hasInteracted = false;
        }
    }

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Interact.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.Disable();
    }
}
