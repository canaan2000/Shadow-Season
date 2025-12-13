using UnityEngine;

public class CarryBallScript : MonoBehaviour
{
    InventoryNew inventory;

    public Transform carryPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GetComponent<InventoryNew>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject matchingPrefab = null;

        foreach (GameObject ball in inventory.ballPrefabs)
        {
            if (inventory.currentBall != null && inventory.currentBall.name.Contains(ball.name))
            {
                matchingPrefab = ball;
                break;
            }
        }

        if (inventory.currentBall != null)
        {
            if (inventory.ballInventory[matchingPrefab.tag] <= 0)
            {
                ChangeBall();
            }
        }
    }

    public void CarryBall(GameObject ball)
    {
        ChangeBall();

        if (ball != null)
        {
            ball.transform.parent = carryPoint;
            ball.transform.position = carryPoint.position;
            ball.transform.rotation = carryPoint.rotation;
            ball.GetComponent<Rigidbody>().isKinematic = true;
            ball.GetComponent<Collider>().enabled = false;

            inventory.currentBall = ball;
        }
    }

    public void ChangeBall()
    {
        if (carryPoint.childCount == 0)
        {
            return;
        }
        inventory.currentBall = null;
        GameObject carriedBall = carryPoint.GetChild(0).gameObject;
        Destroy(carriedBall);
    }
}
