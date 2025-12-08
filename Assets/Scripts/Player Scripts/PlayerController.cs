using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputSystem_Actions inputActions;
    public CharacterController characterController;

    Inventory playerInventory;

    public Camera playerCamera;

    public float speed = 5f;
    public float lookSpeed = .3f;
    float cameraPitch = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var movementInput = inputActions.Player.Move.ReadValue<Vector2>();

        if (movementInput != Vector2.zero)
        {
            Vector3 moveDirection = transform.forward * movementInput.y + transform.right * movementInput.x;
            characterController.Move(moveDirection * Time.deltaTime * speed);
        }

        var lookInput = inputActions.Player.Look.ReadValue<Vector2>();

        if (lookInput != Vector2.zero)
        {
            transform.Rotate(Vector3.up * lookInput.x * lookSpeed);

            cameraPitch -= lookInput.y * lookSpeed;

            cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

            playerCamera.transform.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
        }
    }

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Move.Enable();
        inputActions.Player.Look.Enable();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.Disable();
        inputActions.Player.Look.Disable();
    }
}
