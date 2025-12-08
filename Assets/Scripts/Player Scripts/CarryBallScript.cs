using UnityEngine;

public class CarryBallScript : MonoBehaviour
{
    Inventory inventory;

    public Transform carryPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.numberOfBalls <= 0)
        {
            ChangeBall();
        }
    }

    public void CarryBall(GameObject ball)
    {
        ChangeBall();

        if (ball != null && inventory.numberOfBalls > 0)
        {
            ball.transform.parent = carryPoint;
            ball.transform.position = carryPoint.position;
            ball.transform.rotation = carryPoint.rotation;
            ball.GetComponent<Rigidbody>().isKinematic = true;
            ball.GetComponent<Collider>().enabled = false;
        }
    }

    public void ChangeBall()
    {
        if (carryPoint.childCount == 0)
        {
            return;
        }
        GameObject carriedBall = carryPoint.GetChild(0).gameObject;
        Destroy(carriedBall);
    }
}
